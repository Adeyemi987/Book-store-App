using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StorBookWebApp.Core.API.Interfaces;
using StorBookWebApp.Data;
using StorBookWebApp.DTOs.API;
using StorBookWebApp.DTOs.ImageDTOs;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StorBookWebApp.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 60)]
        [HttpCacheValidation(MustRevalidate = false)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IList<UserDto>>> GetUsers([FromQuery] RequestParams requestParams)
        {
            var result = await _userService.GetUsers(requestParams);
            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetUser")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserDto>> GetUser(string id)
        {
            var result = await _userService.GetUserById(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpGet("email")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserDto>> GetUserByEmail(string email)
        {
            var result = await _userService.GetUserByEmail(email);
            return result == null ? NotFound() : Ok(result);

        }

        [HttpGet("search/{name}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IList<UserDto>>> SearchUsers(string name, [FromQuery] RequestParams requestParams)
        {
            var result = await _userService.SearchUsers(name, requestParams);
            return Ok(result);
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpPut("update/{id}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserDto userDTO)
        {

            var response = await _userService.UpdateUser(id, userDTO);
            return StatusCode(response.StatusCode, response);

        }

        [Authorize(Roles = "Regular")]
        [HttpPut("update")]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto userDTO)
        {
            var id = HttpContext.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value;

            var response = await _userService.UpdateUser(id, userDTO);
            return StatusCode(response.StatusCode, response);

        }

        [HttpPatch("photo/id")]
        [Authorize(Roles = "Regular")]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateUserPhoto([FromForm] AddImageDto imageDTO)
        {
            var id = HttpContext.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value;

            var response = await _userService.UploadPhotoUrlAsync(id, imageDTO);

            if (response.Data != true) return BadRequest();

            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "SuperAdmin")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<string>> DeleteUser(string id)
        {
            var response = await _userService.DeleteUser(id);
            return StatusCode(response.StatusCode, response);
        }


    }
}
