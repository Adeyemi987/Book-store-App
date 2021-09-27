using Microsoft.AspNetCore.Identity;
using StorBookWebApp.DTOs.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StorBookWebApp.Core.API.Interfaces
{
    public interface IAdministrationService
    {
        Task<IdentityResult> CreateRole(CreateUserRoleDto model);
        Task<List<UserRolesDto>> ManageUserRole(string userId);
        Task<IdentityResult> ManageUserRolesAsync(List<UserRolesDto> model, string userId);
    }
}
