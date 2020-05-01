using Licenta.Dtos;
using Licenta.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Licenta.API.Services
{
    public interface IAuthService
    {
        User MapRegisterInformations(UserForRegisterDto userForRegisterDto);
        Task<IdentityResult> RegisterUser(User user, string password);
        UserForDetailedDto MapRegisteredUser(User user);
        User FindUser(string username);
        Task<SignInResult> SignInUser(User user, string password);
        UserForListDto MapUserForLogin(User user);
        Task<string> GenerateJwtToken(User user);
        void AddUserToDivisions(UserForRegisterDto userForRegister, User user, UserForDetailedDto userForDetailed);
    }
}
