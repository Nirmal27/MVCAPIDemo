using EmployeeSystem.DataAccess.Abstraction;
using EmployeeSystem.DataAccess.Entities;
using EmployeeSystem.Models.APIModels;
using EmployeeSystem.Services.Abstraction;
using System;
using System.Collections.Generic;

namespace EmployeeSystem.Services.Repository
{
    public class EmployeeHelper : IEmployeeHelper
    {
        private readonly IEmployeeRepository employeeRepository;
        public EmployeeHelper(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        /// <summary>
        /// Method for adding the employee
        /// </summary>
        /// <param name="employee">object value of employee</param>
        /// <returns>Returns true if employee created else false</returns>
        public bool AddEmployee(Employee employee)
        {
            var employeeData = employeeRepository.GetEmployeeByName(employee.FirstName, employee.LastName);
            if (employeeData == null)
            {
                employeeRepository.AddEmployee(employee);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Method for checking the department exist or not
        /// </summary>
        /// <param name="id">Id of the department</param>
        /// <returns>returns true if department exist else false</returns>
        public bool CheckDepartmentExist(int id)
        {
            var department = employeeRepository.GetDepartment(id);
            if (department == null)
                return false;
            return true;
        }

        /// <summary>
        /// Method for deleting the employee data
        /// </summary>
        /// <param name="id">Id of the employee</param>
        /// <returns>returns if the employee is deleted else false</returns>
        public bool DeleteEmployee(int id)
        {
            var employee = employeeRepository.GetEmployeeById(id);
            if (employee != null)
            {
                employeeRepository.DeleteEmployee(employee);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Method for getting the employee details
        /// </summary>
        /// <param name="id">Id of the employee</param>
        /// <returns>returns the details of the employee</returns>
        public Employee GetEmployeeById(int id)
        {
            return employeeRepository.GetEmployeeById(id);
        }

        /// <summary>
        /// Method for getting the list of all the employees
        /// </summary>
        /// <returns>Returns list of all the employees</returns>
        public List<Employee> GetEmployees()
        {
            return employeeRepository.GetEmployees();
        }

        /// <summary>
        /// Method for getting all the employees using stored procedure
        /// </summary>
        /// <returns>returns list of all the employees</returns>
        public List<EmployeesDepartments> GetEmployeesDepartments(int id = 0)
        {
            if (id == 0)
                return employeeRepository.GetEmployeesDepartments(string.Empty);
            else
                return employeeRepository.GetEmployeesDepartments(id.ToString());
        }

        /// <summary>
        /// Method for updating the employee
        /// </summary>
        /// <param name="employee">Object value of the employee model</param>
        /// <returns>returns true if employee is updated else false</returns>
        public bool UpdateEmployee(EmployeesDepartments employee)
        {
            var employeeData = employeeRepository.GetEmployeeById(employee.Id);
            if (employeeData != null)
            {
                employeeData.FirstName = employee.FirstName;
                employeeData.LastName = employee.LastName;
                employeeData.BirthDate = employee.BirthDate;
                employeeData.Gender = employee.Gender;
                employeeData.DepartmentId = employee.DepartmentId;
                employeeRepository.UpdateEmployee(employeeData);
                return true;
            }
            return false;
        }
    }
}
