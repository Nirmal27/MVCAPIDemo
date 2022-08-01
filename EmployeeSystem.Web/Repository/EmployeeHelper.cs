using EmployeeSystem.Models.Models;
using EmployeeSystem.Web.Abstraction;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeSystem.Web.Repository
{
    public class EmployeeHelper : IEmployeeHelper
    {
        private readonly IApiHelper apiHelper;
        public EmployeeHelper(IApiHelper apiHelper)
        {
            this.apiHelper = apiHelper;
        }

        /// <summary>
        /// Method for Inserting employee API to insert the API to the database
        /// </summary>
        /// <param name="token">JWT Token for authorization</param>
        /// <param name="employee">Object EmployeeViewModel</param>
        /// <returns>Returns true if Employee is inserted else false.</returns>
        public bool AddEmployee(EmployeeViewModel employee)
        {
            var response = apiHelper.GetEmployeesResponse(Method.POST, employee);
            if (response.IsSuccessful)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// For deleting the employee
        /// </summary>
        /// <param name="token">JWT Token for authorization</param>
        /// <param name="id">Id of the employee</param>
        /// <returns>Returns true if employee successful else false</returns>
        public bool DeleteEmployee(int id)
        {
            var response = apiHelper.GetEmployeesResponse(Method.DELETE, null, id);
            if (response.IsSuccessful)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Method for editing the existing employee
        /// </summary>
        /// <param name="token"></param>
        /// <param name="employee"></param>
        /// <returns>Returns true if successful else false</returns>
        public bool EditEmployee(EmployeesDepartmentsViewModel employee)
        {
            var response = apiHelper.GetEmployeesResponse(Method.PUT, employee);
            if (response.IsSuccessful)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Method for getting employee details
        /// </summary>
        /// <param name="token">JWT Token for authorization</param>
        /// <param name="id">Id of the employee</param>
        /// <returns>Returns Employee object</returns>
        public EmployeeViewModel GetEmployee(int id)
        {
            EmployeeViewModel result = new EmployeeViewModel();
            var response = apiHelper.GetEmployeesResponse(Method.GET, null, id);
            if (response.IsSuccessful)
            {
                result = JsonConvert.DeserializeObject<EmployeeViewModel>(response.Content);
            }
            return result;
        }


        /// <summary>
        /// Method for getting all the employees
        /// </summary>
        /// <param name="token">JWT Token for the authorization</param>
        /// <returns>Returns List of Employee object</returns>
        public EmployeesDepartmentsViewModel GetEmployeeDepartment(int id = 0)
        {
            List<EmployeesDepartmentsViewModel> result = new List<EmployeesDepartmentsViewModel>();
            var response = apiHelper.GetEmployeesResponse(Method.GET, null, id);
            if (response.IsSuccessful)
            {
                result = JsonConvert.DeserializeObject<List<EmployeesDepartmentsViewModel>>(response.Content);
            }
            return result.SingleOrDefault();
        }

        /// <summary>
        /// Method for getting the employee data with associated department
        /// </summary>
        /// <returns>Returns list of Employee data</returns>
        public List<EmployeesDepartmentsViewModel> GetEmployeesDepartments()
        {
            List<EmployeesDepartmentsViewModel> result = new List<EmployeesDepartmentsViewModel>();
            var response = apiHelper.GetEmployeesResponse(Method.GET, null);
            if (response.IsSuccessful)
            {
                result = JsonConvert.DeserializeObject<List<EmployeesDepartmentsViewModel>>(response.Content);
            }
            return result;
        }
    }
}
