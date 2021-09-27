using System.Threading.Tasks;

namespace StorBookWebApp.Core.MVC
{
    public interface IGetAdminRepo
    {
        Task<bool> LoginUser(LoginUserDto user);
        Task<bool> RegisterNewUser(RegisterNewUserDto user);
    }
}