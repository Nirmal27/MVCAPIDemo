using EmployeeSystem.DataAccess.Identity;
using EmployeeSystem.Models.APIModels;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace EmployeeSystem.Services.Abstraction
{
    public interface IAccountHelper
    {
        string GenerateToken(ApplicationUser user);
        Task<string> CheckExistsUserName(string UserName);
        Task<bool> CreateUser(RegisterModel model);
        ApplicationUser GetUser(string EmailAddress);
        Task<SignInResult> Login(string UserName, string Password, bool IsPersistent, bool lockoutOnFailure);
    }
}