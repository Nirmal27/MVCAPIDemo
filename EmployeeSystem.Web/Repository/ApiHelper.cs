using EmployeeSystem.Web.Abstraction;
using EmployeeSystem.Web.Models;
using Microsoft.Extensions.Options;
using RestSharp;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace EmployeeSystem.Web.Repository
{
    public class ApiHelper : IApiHelper
    {
        private readonly AppSettings appSettings;
        private readonly IHttpContextAccessor accessor;
        public ApiHelper(IOptions<AppSettings> appSettings, IHttpContextAccessor accessor)
        {
            this.appSettings = appSettings.Value;
            this.accessor = accessor;
        }

        public IRestResponse GetDepartmentResponse(Method method, dynamic model, int id = 0)
        {
            var token = accessor.HttpContext.Session.GetString("Token");
            var client = new RestClient(appSettings.APIUri);
            client.Timeout = -1;
            RestRequest request;
            switch (method)
            {
                case Method.GET:
                    if (id != 0)
                        request = new RestRequest("api/departments/" + id, Method.GET);
                    else
                        request = new RestRequest("api/departments", Method.GET);
                    break;
                default:
                    request = new RestRequest("api/departments", Method.GET);
                    break;
            }
            request.AddHeader("Authorization", "Bearer " + token);
            IRestResponse response = client.Execute(request);
            return response;
        }

        /// <summary>
        /// Method calls Employee API and gets the response
        /// </summary>
        /// <param name="method">Method type of the API (GET, Post, ...)</param>
        /// <param name="model">Employee object value</param>
        /// <param name="id">Id of the employee</param>
        /// <returns>Returns Employee API response</returns>
        public IRestResponse GetEmployeesResponse(Method method, dynamic model, int id = 0)
        {
            var token = accessor.HttpContext.Session.GetString("Token");
            var client = new RestClient(appSettings.APIUri);
            client.Timeout = -1;
            RestRequest request;
            switch (method)
            {
                case Method.GET:
                    if (id != 0)
                    {
                        request = new RestRequest("api/employeesdepartments/" + id, Method.GET);
                        //request = new RestRequest("api/employees/" + id, Method.GET);
                    }
                    else
                    {
                        request = new RestRequest("api/employeesdepartments", Method.GET);
                        //request = new RestRequest("api/employees", Method.GET);
                    }

                    break;
                case Method.POST:
                    request = new RestRequest("api/employees", Method.POST);
                    request.AddHeader("Content-Type", "application/json");
                    request.AddParameter("application/json", JsonConvert.SerializeObject(model), ParameterType.RequestBody);
                    break;
                case Method.PUT:
                    request = new RestRequest("api/employees", Method.PUT);
                    request.AddHeader("Content-Type", "application/json");
                    request.AddParameter("application/json", JsonConvert.SerializeObject(model), ParameterType.RequestBody);
                    break;
                case Method.DELETE:
                    request = new RestRequest("api/employees/" + id, Method.DELETE);
                    break;
                default:
                    request = new RestRequest("api/employees", Method.GET);
                    break;
            }
            request.AddHeader("Authorization", "Bearer " + token);
            IRestResponse response = client.Execute(request);
            return response;
        }
    }
}
