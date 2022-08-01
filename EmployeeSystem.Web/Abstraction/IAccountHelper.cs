using EmployeeSystem.Models.ViewModels;
using EmployeeSystem.Web.Models.Response;

namespace EmployeeSystem.Web.Abstraction
{
    public interface IAccountHelper
    {
        LoginResponse Login(LoginViewModel loginViewModel);
        bool Register(RegisterViewModel registerViewModel);
    }
}
