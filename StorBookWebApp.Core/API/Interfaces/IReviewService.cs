using StorBookWebApp.DTOs.ReviewDTOs;
using StorBookWebApp.Models;
using StorBookWebApp.Shared.ErrorResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorBookWebApp.Core.API.Interfaces
{
    public interface IReviewService
    {
        Task<ApiResponse<Book>> AddReviewToBook(AddReviewDto addReviewDto);
        Task<ApiResponse<Review>> UpdateReview(AddReviewDto reviewDto);
        Review GetReview(string Id);
    }
}
