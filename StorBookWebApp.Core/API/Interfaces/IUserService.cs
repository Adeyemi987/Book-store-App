using Microsoft.AspNetCore.Identity;
using StorBookWebApp.Data;
using StorBookWebApp.DTOs.API;
using StorBookWebApp.DTOs.ImageDTOs;
using StorBookWebApp.Shared.ErrorResponses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StorBookWebApp.Core.API.Interfaces
{
    public interface IUserService
    {
        Task<ApiResponse<UserDto>> AddUser(RegisterUserDto model);
        Task<UserDto> GetUserById(string id);
        Task<UserDto> GetUserByEmail(string email);
        Task<IList<UserDto>> GetUsers(RequestParams requestParams);
        Task<IList<UserDto>> SearchUsers(string name, RequestParams param);
        Task<ApiResponse<IdentityResult>> UpdateUser(string id, UpdateUserDto userDto);
        Task<ApiResponse<bool>> UploadPhotoUrlAsync(string userId, AddImageDto imageDto);
        Task<ApiResponse<string>> DeleteUser(string id);
    }
}
