using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StorBookWebApp.Core.API.Interfaces;
using StorBookWebApp.DTOs.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StorBookWebApp.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministrationController : ControllerBase
    {
        private readonly IAdministrationService _adminService;

        public AdministrationController(IAdministrationService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("get-roles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<UserRolesDto>>> GetRoles(string userId)
        {
            var result = await _adminService.ManageUserRole(userId);

            if (result != null)
                return result;

            return BadRequest(result);
        }

        [HttpPost("manage-user-roles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<UserRolesDto>>> ManageUserRoles(List<UserRolesDto> model, string userId)
        {
            var result = await _adminService.ManageUserRolesAsync(model, userId);

            if (!result.Succeeded)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("create-roles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<UserRolesDto>>> CreateRole([FromBody] CreateUserRoleDto model)
        {
            var result = await _adminService.CreateRole(model);

            if (!result.Succeeded)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
