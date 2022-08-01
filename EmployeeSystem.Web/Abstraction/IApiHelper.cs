using RestSharp;

namespace EmployeeSystem.Web.Abstraction
{
    public interface IApiHelper
    {
        IRestResponse GetEmployeesResponse(Method method, dynamic model, int id = 0);
        IRestResponse GetDepartmentResponse(Method method, dynamic model, int id = 0);
    }
}
