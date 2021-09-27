using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StorBookWebApp.Core.API.Interfaces;
using StorBookWebApp.Data;
using StorBookWebApp.DTOs.BookDTOs;
using StorBookWebApp.DTOs.ImageDTOs;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StorBookWebApp.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 60)]
        [HttpCacheValidation(MustRevalidate = false)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IList<BookDto>> GetBooks([FromQuery] RequestParams requestParams)
        {
            var result = _bookService.GetBooks(requestParams);
            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetBook")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<BookDto> GetBook(string id)
        {
            var result = _bookService.GetBookById(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpGet("isbn", Name = "GetBookByIsbn")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<BookDto> GetBookByIsbn(string isbn)
        {
            var result = _bookService.GetBookByIsbn(isbn);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpGet("genre", Name = "GetBooksByGenre")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BookDto>> GetBooksByGenre(string genre, [FromQuery] RequestParams requestParams)
        {
            var result = await _bookService.GetBooksByGenre(genre, requestParams);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("search/{term}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IList<BookDto>>> Search(string term, [FromQuery] RequestParams requestParams)
        {
            var result = await _bookService.SearchBooks(term, requestParams);
            return Ok(result);
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPut("update/{id}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateBookDto bookDTO)
        {

            var response = await _bookService.UpdateBook(id, bookDTO);

            return response != true ? BadRequest("User Not Found") : NoContent();

        }

        [HttpPatch("photo/id")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateUserPhoto([FromForm] AddImageDto imageDto)
        {
            var id = HttpContext.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value;

            var response = await _bookService.UploadCoverUrlAsync(id, imageDto);

            return response != true ? BadRequest() : NoContent();
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpDelete("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<string>> Delete(string id)
        {
            var response = await _bookService.DeleteBook(id);
            return response != true ? BadRequest() : NoContent();
        }
    }
}
