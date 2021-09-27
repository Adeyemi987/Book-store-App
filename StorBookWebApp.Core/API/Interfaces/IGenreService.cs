using StorBookWebApp.DTOs.GenreDTOs;
using StorBookWebApp.Models;
using StorBookWebApp.Shared.ErrorResponses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StorBookWebApp.Core.API.Interfaces
{
    public interface IGenreService
    {
        Task<ApiResponse<IList<GenreDto>>> GetAll();
        Task<ApiResponse<GenreDto>> Get(string name);
        Task<ApiResponse<GenreDto>> Add(AddGenreDto model);
    }
}
