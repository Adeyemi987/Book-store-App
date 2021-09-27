using AutoMapper;
using StorBookWebApp.Core.API.Interfaces;
using StorBookWebApp.Data;
using StorBookWebApp.Data.Implementation;
using StorBookWebApp.Data.Interfaces;
using StorBookWebApp.DTOs.AuthorDTOs;
using StorBookWebApp.DTOs.BookDTOs;
using StorBookWebApp.DTOs.GenreDTOs;
using StorBookWebApp.DTOs.ImageDTOs;
using StorBookWebApp.Models;
using StorBookWebApp.Shared.ErrorResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using X.PagedList;

namespace StorBookWebApp.Core.API.Implementations
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;
        private readonly IBookRepository _bookRepo;

        public BookService(IUnitOfWork unitOfWork, IMapper mapper,
            IImageService imageService, IBookRepository bookRepo)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _imageService = imageService;
            _bookRepo = bookRepo;
        }

        public async Task<BookListDto> AddBook(AddBookDto addBookDto, List<AddAuthorDto> authorsDtos, List<AddGenreDto> genreDtos)
        {
            var authors = new List<Author>();
            var genres = new List<Genre>();

            foreach (var author in authorsDtos)
            {
                if (_unitOfWork.Authors.Get1(a => a.Name == author.Name) != null)
                {
                    var foundAuthor = _mapper.Map<Author>(author);
                    authors.Add(foundAuthor);
                }
                else
                    throw new BadRequestException($"Author with name: {author.Name} not found.\nPlease add Author to record and try again");
            }

            foreach (var genre in genreDtos)
            {
                if (_unitOfWork.Authors.Get1(g => g.Name.ToLower() == genre.Name.ToLower()) != null)
                {
                    var foundGenre = _mapper.Map<Genre>(genre);
                    genres.Add(foundGenre);
                }
                else
                    throw new BadRequestException($"Genre with name: {genre.Name} not found.\nPlease add {genre.Name} to record and try again");
            }

            var book = _mapper.Map<Book>(addBookDto);

            foreach (var author in authors)
            {
                book.BookAuthors = new List<BookAuthor>
                {
                    new BookAuthor
                    {
                        Book = book,
                        Author = author
                    }
                };
            }

            foreach (var genre in genres)
            {
                book.BookGenres = new List<BookGenre>
                {
                    new BookGenre
                    {
                        Book = book,
                        Genre = genre
                    }
                };
            }

            await _unitOfWork.Books.Add(book);
            await _unitOfWork.Save();

            return new BookListDto
            {
                Id = book.Id,
                Isbn = book.Isbn,
                Title = book.Title,
                PublishedOn = book.Publisher,
                Description = book.Description,
                Thumbnail = book.CoverUrl,
                AuthorsOrdered = string.Join(", ", book.BookAuthors
                                    .OrderBy(ba => ba.Author.Name)
                                    .Select(ba => ba.Author.Name)),
                ReviewsCount = book.Reviews.Count,
                ReviesAverageCount = book.Reviews.Select(review =>
                                (double?)review.NumStars).Average(),
                Created = book.Created,
                Updated = book.Updated
            };

        }
        public async Task<bool> DeleteBook(string id)
        {
            var book =  _unitOfWork.Books.Get(q => q.Id == id);
            if (book == null)
                throw new ArgumentNullException($"No book with id = {id} in record");

            await _unitOfWork.Books.Delete(id);
            await _unitOfWork.Save();

            return true;
        }
        public BookListDto GetBookById(string id)
        {
            var book = _unitOfWork.Books.Get(q => q.Id == id);
            var result = _bookRepo.MapBookToDto(book).FirstOrDefault();

            if (result == null)
            {
                throw new BadRequestException("Record does not exist!");
            }
            return result;
        }
        public BookListDto GetBookByIsbn(string isbn)
        {
            var book = _unitOfWork.Books.Get(q => q.Isbn == isbn);
            var result = _bookRepo.MapBookToDto(book).FirstOrDefault();

            if (result == null)
            {
                throw new BadRequestException("Record does not exist!");
            }
            return result;
        }
        public IPagedList<BookListDto> GetBooks(RequestParams requestParams)
        {
            var books = _bookRepo.GetAll();
            var result = _bookRepo.MapBookToDto(books).
                ToPagedListAsync(requestParams.PageNumber, requestParams.PageSize).Result;
            return result;
        }
        public async Task<ApiResponse<IPagedList<BookListDto>>> GetBooksByGenre(string genre, RequestParams requestParams)
        {
            var books = _bookRepo.GetBooksByGenre(genre);
            var response = new ApiResponse<IPagedList<BookListDto>>();
            if(books == null)
            {
                response.Data = default;
                response.Succeeded = false;
                response.Message = $"{genre} does not exist in records";
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                return response;
            }
            var result = await books.ToPagedListAsync(requestParams.PageNumber, requestParams.PageSize);
            response.Data = result;
            response.Succeeded = true;
            response.StatusCode = (int)HttpStatusCode.OK;
            return response;
        }
        public async Task<IList<BookDto>> SearchBooks(string term, RequestParams param)
        {
            var books = await _unitOfWork.Books.GetAll(param,
                q => q.Title.Contains(term) 
                || q.Publisher.Contains(term)
                || q.Isbn.Contains(term));
            var result = _mapper.Map<IList<BookDto>>(books);
            return result;
        }
        public async Task<bool> UpdateBook(string id, UpdateBookDto bookDto)
        {
            var book =  _unitOfWork.Books.Get(q => q.Id == id).SingleOrDefault();
            if(book == null)
                throw new BadRequestException("Record does not exist");

            _mapper.Map(bookDto, book);
            _unitOfWork.Books.Update(book);
            await _unitOfWork.Save();

            return true;
        }
        public async Task<bool> UploadCoverUrlAsync(string bookId, AddImageDto imageDto)
        {
            var book =  _unitOfWork.Books.Get(q => q.Id == bookId).FirstOrDefault();

            if (book == null)
                throw new ArgumentNullException("User not found!");

            var result = await _imageService.UploadImageAsync(imageDto.Image);
            var imageProp = new ImageAddedDTO()
            {
                PublicId = result.PublicId,
                URL = result.Url.ToString()
            };

            book.CoverUrl = string.IsNullOrWhiteSpace(imageProp.URL) ? "default.jpg" : imageProp.URL;

            _unitOfWork.Books.Update(book);
            await _unitOfWork.Save();

            return true;
        }
       
    }
}
