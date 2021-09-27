using Microsoft.AspNetCore.Identity;
using StorBookWebApp.Core.API.Interfaces;
using StorBookWebApp.DTOs.User;
using StorBookWebApp.Models;
using StorBookWebApp.Shared.ErrorResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StorBookWebApp.Core.API.Implementations
{
    public class AdministrationService : IAdministrationService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdministrationService(UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> CreateRole(CreateUserRoleDto model)
        {
            IdentityRole role = new()
            {
                Name = model.RoleName,
                NormalizedName = model.RoleName.ToUpper()
            };

            IdentityResult result = await _roleManager.CreateAsync(role);

            ApiResponse<IdentityResult> response = new();

            if (result.Succeeded)
            {
                response.Data = result;
                return result;
            }

            response.Data = result;
            foreach (var err in result.Errors)
            {
                response.Message += err.Description + ", ";
            }

            throw new MissingFieldException(response.Message);

        }

        public async Task<List<UserRolesDto>> ManageUserRole(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                throw new ArgumentNullException($"User with id = {userId} cannot be found");

            var model = new List<UserRolesDto>();

            foreach (var role in _roleManager.Roles)
            {
                var userRoleModel = new UserRolesDto()
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };

                if (await _userManager.IsInRoleAsync(user, role.Name))
                    userRoleModel.IsInRole = true;
                else
                    userRoleModel.IsInRole = false;

                model.Add(userRoleModel);
            }

            return model;

        }

        public async Task<IdentityResult> ManageUserRolesAsync(List<UserRolesDto> model, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                throw new ArgumentNullException($"User with id = {userId} cannot be found");

            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);

            if (!result.Succeeded)
            {
                throw new ArgumentException("cannot remove user from role");
            }

            result = await _userManager.AddToRolesAsync(user, model.Where(x => x.IsInRole).Select(x => x.RoleName));

            if (!result.Succeeded)
            {
                throw new ArgumentException("cannot add selected role to user");
            }

            return result;
        }
    }
}
