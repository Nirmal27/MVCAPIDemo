using EmployeeSystem.DataAccess.Identity;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace EmployeeSystem.DataAccess.Abstraction
{
    public interface IAccountRepository
    {
        Task<string> CheckExistsUserName(string UserName);
        Task<bool> CreateUser(ApplicationUser model, string password);
        ApplicationUser GetUser(string userName);
        Task<SignInResult> Login(string UserName, string Password, bool IsPersistent, bool lockoutOnFailure);
    }
}
