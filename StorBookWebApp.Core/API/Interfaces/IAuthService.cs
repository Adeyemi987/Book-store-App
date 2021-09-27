using StorBookWebApp.DTOs.API;
using System.Threading.Tasks;

namespace StorBookWebApp.Core.API.Interfaces
{
    public interface IAuthService
    {
        Task<bool> ValidateUser(LoginUserDto userDto);
        Task<string> CreateToken();
    }
}
