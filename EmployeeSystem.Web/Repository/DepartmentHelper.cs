using EmployeeSystem.Models.ViewModels;
using EmployeeSystem.Web.Abstraction;
using EmployeeSystem.Web.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;

namespace EmployeeSystem.Web.Repository
{
    public class DepartmentHelper : IDepartmentHelper
    {
        private readonly IApiHelper apiHelper;
        public DepartmentHelper(IApiHelper apiHelper)
        {
            this.apiHelper = apiHelper;
        }

        /// <summary>
        /// Get department by Id
        /// </summary>
        /// <param name="id">Id of the department</param>
        /// <returns>Returns department details of the Id</returns>
        public DepartmentViewModel GetDepartmentById(int id = 0)
        {
            DepartmentViewModel result = new DepartmentViewModel();
            var response = apiHelper.GetEmployeesResponse(Method.GET, null, id);
            if (response.IsSuccessful)
            {
                result = JsonConvert.DeserializeObject<DepartmentViewModel>(response.Content);
            }
            return result;
        }

        /// <summary>
        /// Get all department
        /// </summary>
        /// <returns>Returns List of the department</returns>
        public List<DepartmentViewModel> GetDepartments()
        {
            List<DepartmentViewModel> result = new List<DepartmentViewModel>();
            var response = apiHelper.GetDepartmentResponse(Method.GET, null);
            if (response.IsSuccessful)
            {
                result = JsonConvert.DeserializeObject<List<DepartmentViewModel>>(response.Content);
            }
            return result;
        }
    }
}
