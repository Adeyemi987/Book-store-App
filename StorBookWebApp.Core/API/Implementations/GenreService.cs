using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StorBookWebApp.Core.API.Interfaces;
using StorBookWebApp.Data.Implementation;
using StorBookWebApp.DTOs.GenreDTOs;
using StorBookWebApp.Models;
using StorBookWebApp.Shared.ErrorResponses;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace StorBookWebApp.Core.API.Implementations
{
    public class GenreService : IGenreService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GenreService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;

        }

        public async Task<ApiResponse<IList<GenreDto>>> GetAll()
        {
            var genres = await _unitOfWork.Genres.GetAll().ToListAsync();
            var result = _mapper.Map<IList<GenreDto>>(genres);
            var response = new ApiResponse<IList<GenreDto>>();

            if (!result.Any())
            {
                response.Data = default;
                response.Succeeded = false;
                response.StatusCode = (int)HttpStatusCode.NotFound;
                response.Message = "No record found";
                return response;
            }
            response.Data = result;
            response.StatusCode = (int)HttpStatusCode.OK;
            response.Succeeded = true;
            return response;
        }

        public async Task<ApiResponse<GenreDto>> Add(AddGenreDto model)
        {
            ApiResponse<GenreDto> response = new();
            var message = string.Empty;

            if (!Find(model.Name))
            {
                var genre = _mapper.Map<Genre>(model);
                await _unitOfWork.Genres.Add(genre);

                response.StatusCode = (int)HttpStatusCode.Created;
                response.Data = _mapper.Map<GenreDto>(genre);
                ApiResponse<GenreDto>.Success(response.Data);
                return response;
            }           

            response.StatusCode = (int)HttpStatusCode.BadRequest;
            message += $"{model.Name} already exist in record";
            response.Message = message;
            ApiResponse<GenreDto>.Fail(response.Message);
            return response;
      
        }

        public async Task<ApiResponse<GenreDto>> Get(string name)
        {
            var genre = await _unitOfWork.Genres.Get(g => g.Name.ToLower() == name.ToLower()).SingleOrDefaultAsync();

            var message = string.Empty;
            ApiResponse<GenreDto> response = new();

            if(genre == null)
            {
                response.StatusCode = (int)HttpStatusCode.NotFound;
                message = $"{name} does not exist in record";
                ApiResponse<Genre>.Fail(message, response.StatusCode);
                return response;
            }
            response.StatusCode = (int)HttpStatusCode.OK;
            var result = _mapper.Map<GenreDto>(genre);
            response.Data = result;
            response.Succeeded = true;
            return ApiResponse<GenreDto>.Success(result, response.StatusCode);
        }

        private bool Find(string name)
        {
            var genre =  _unitOfWork.Genres.Get(g => g.Name.ToLower() == name.ToLower()).SingleOrDefault();
            return genre != null;
        }
    }
}
