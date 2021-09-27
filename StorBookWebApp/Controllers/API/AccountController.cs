using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StorBookWebApp.Core.API.Interfaces;
using StorBookWebApp.DTOs.API;
using StorBookWebApp.Models;
using StorBookWebApp.Shared.ErrorResponses;
using System.Linq;
using System.Threading.Tasks;

namespace StorBookWebApp.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<AccountController> _logger;
        private readonly IAuthService _authService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        public AccountController(IUserService userService, 
            RoleManager<IdentityRole> roleManager, IMapper mapper,
            ILogger<AccountController> logger, IAuthService authService, 
            UserManager<AppUser> userManager)
        {
            _userService = userService;
            _logger = logger;
            _authService = authService;
            _roleManager = roleManager;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<UserDto>> Register([FromBody] RegisterUserDto userDTO)
        {
            _logger.LogInformation($"Registration Attempt for {userDTO.Email}");

            var result = await _userService.AddUser(userDTO);
            return Created("", result);
        }

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<string>> LoginAdmin([FromBody] LoginUserDto userDTO)
        {
            _logger.LogInformation($"Login Attempt for {userDTO.Email}");

            if (!await _authService.ValidateUser(userDTO))
            {
                return Unauthorized();
            }

            var user = await _userManager.FindByEmailAsync(userDTO.Email);
            
            var result = _mapper.Map<UserDto>(user);

            var roles = _userManager.GetRolesAsync(user);


            if(roles != null)
                result.Roles = roles.Result;

            var token = await _authService.CreateToken();
            result.token = token.ToString();

            if (result.Roles.FirstOrDefault() != "Admin")
            {
                if (result.Roles.FirstOrDefault() != "SuperAdmin")
                {
                    return Unauthorized();
                }
            }

            return Accepted(result);
        }


        [HttpPost]
        [Route("loginUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<string>> LoginUser([FromBody] LoginUserDto userDto)
        {
            _logger.LogInformation($"Login Attempt for {userDto.Email}");

            if (!await _authService.ValidateUser(userDto))
            {
                return Unauthorized();
            }

            var user = await _userManager.FindByEmailAsync(userDto.Email);

            var result = _mapper.Map<UserDto>(user);

            var roles = _userManager.GetRolesAsync(user);


            if (roles != null)
                result.Roles = roles.Result;

            var token = await _authService.CreateToken();
            result.token = token.ToString();

            if (result.Roles.FirstOrDefault() != "Regular")
            {
                return Unauthorized();
            }

            return Accepted(result);
        }


    }
}
