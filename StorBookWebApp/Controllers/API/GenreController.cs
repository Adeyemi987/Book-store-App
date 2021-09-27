using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StorBookWebApp.Core.API.Interfaces;
using StorBookWebApp.DTOs.GenreDTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StorBookWebApp.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;
        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 60)]
        [HttpCacheValidation(MustRevalidate = false)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IList<GenreDto>>> GetAll()
        {
            var result = await _genreService.GetAll();
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("name")]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 60)]
        [HttpCacheValidation(MustRevalidate = false)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IList<GenreDto>>> Get(string name)
        {
            var result = await _genreService.Get(name);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GenreDto>> Add([FromForm] AddGenreDto model)
        {
            var result = await _genreService.Add(model);
            return StatusCode(result.StatusCode, result);
        }
    }
}
