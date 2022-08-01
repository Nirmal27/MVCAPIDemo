using EmployeeSystem.Models.ViewModels;
using EmployeeSystem.Web.Abstraction;
using EmployeeSystem.Web.Models;
using EmployeeSystem.Web.Models.Response;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;

namespace EmployeeSystem.Web.Repository
{
    public class AccountHelper : IAccountHelper
    {
        private readonly AppSettings appSettings;
        public AccountHelper(IOptions<AppSettings> appSettings)
        {
            this.appSettings = appSettings.Value;
        }

        /// <summary>
        /// Method logs into the system and gets the JWT Token
        /// </summary>
        /// <param name="loginViewModel">Object of the Login model</param>
        /// <returns>returns login response</returns>
        public LoginResponse Login(LoginViewModel loginViewModel)
        {
            LoginResponse result = new LoginResponse();
            var client = new RestClient(appSettings.APIUri);
            client.Timeout = -1;
            var request = new RestRequest("api/login", Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", JsonConvert.SerializeObject(loginViewModel), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful)
            {
                result = JsonConvert.DeserializeObject<LoginResponse>(response.Content);
            }
            return result;
        }

        /// <summary>
        /// Method for registring new user
        /// </summary>
        /// <param name="registerViewModel">Object of RegisterViewModel</param>
        /// <returns>Returns true if successful and false if not</returns>
        public bool Register(RegisterViewModel registerViewModel)
        {
            var client = new RestClient(appSettings.APIUri);
            client.Timeout = -1;
            var request = new RestRequest("api/register/", Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", JsonConvert.SerializeObject(registerViewModel), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful)
            {
                return true;
            }
            return false;
        }
    }
}
