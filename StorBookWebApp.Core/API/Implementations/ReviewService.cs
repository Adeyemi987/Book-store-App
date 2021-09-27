using AutoMapper;
using GenericServices;
using Microsoft.EntityFrameworkCore;
using StorBookWebApp.Core.API.Interfaces;
using StorBookWebApp.Data;
using StorBookWebApp.DTOs.BookDTOs;
using StorBookWebApp.DTOs.ReviewDTOs;
using StorBookWebApp.Models;
using StorBookWebApp.Shared.ErrorResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StorBookWebApp.Core.API.Implementations
{
    public class ReviewService : IReviewService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public string BookTitle { get; private set; }
        public ReviewService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;   
        }
        public Review GetBlankReview(string id)
        {
            BookTitle = _context.Books.Where(p => p.Id == id)
                            .Select(p => p.Title)
                            .Single();
            return new Review
            {
                Id = id
            };
        }

        public async Task<ApiResponse<Book>> AddReviewToBook(AddReviewDto addReviewDto)
        {
            var book = _context.Books
                .Include(r => r.Reviews)
                .Single(k => k.Id == addReviewDto.BookId);
            var response = new ApiResponse<Book>();
            if(book == null)
            {
                response.Data = default;
                response.Succeeded = false;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = $"Book with id {addReviewDto.BookId} not found!";
            }

            var review = _mapper.Map<Review>(addReviewDto);
            book.Reviews.Add(review);
            await _context.SaveChangesAsync();

            response.Data = book;
            response.Succeeded = true;
            response.StatusCode = (int)HttpStatusCode.Created;
            return response;
        }

        public async Task<ApiResponse<Review>> UpdateReview(AddReviewDto reviewDto)
        {
            var review = GetReview(reviewDto.Id);

            review.NumStars = reviewDto.NumStars;
            review.Comment = reviewDto.Comment;
            _context.Reviews.Update(review);
            var result = await _context.SaveChangesAsync();
            var response = new ApiResponse<Review>();
            if(result > 0)
            {
                response.Data = default;
                response.StatusCode = (int)HttpStatusCode.NoContent;
                response.Succeeded = true;
                return response;
            }
            response.Data = default;
            response.Message = "Something went wrong";
            response.Succeeded = false;
            response.StatusCode = (int)HttpStatusCode.BadRequest;
            return response;
        }

        public BookListDto RemoveReview(ReviewDto reviewDto)
        {
            var review = GetReview(reviewDto.Id);
            _context.Reviews.Remove(review);

            var book = _context.Books.SingleOrDefault(x => x.Id == reviewDto.BookId);

            var result = _mapper.Map<BookListDto>(book);

            result.ReviesAverageCount = book.Reviews.Any() ? book.Reviews.Average(x => x.NumStars) : (double?)null;

            return result;
        }

        public Review GetReview(string Id)
        {
            return _context.Reviews.SingleOrDefault(r => r.Id == Id);
        }
    }
}
