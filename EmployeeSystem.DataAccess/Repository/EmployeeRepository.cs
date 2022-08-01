using EmployeeSystem.DataAccess.Abstraction;
using EmployeeSystem.DataAccess.DbContext;
using EmployeeSystem.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeSystem.DataAccess.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeDbContext context;
        public EmployeeRepository(EmployeeDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Method for adding employee
        /// </summary>
        /// <param name="employee">Object value of the Employee model</param>
        /// <returns></returns>
        public void AddEmployee(Employee employee)
        {
                context.Employee.Add(employee);
                context.SaveChanges();
        }

        /// <summary>
        /// Method for deleting the employee
        /// </summary>
        /// <param name="employee">Object of the employee that is to be deleted</param>
        /// <returns></returns>
        public void DeleteEmployee(Employee employee)
        {
            context.Employee.Remove(employee);
            context.SaveChanges();
        }

        /// <summary>
        /// Method for getting the department details
        /// </summary>
        /// <param name="id">Id of the department</param>
        /// <returns>returns details of the department</returns>
        public Department GetDepartment(int id)
        {
            return context.Department.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Method for getting employee by id
        /// </summary>
        /// <param name="id">Id of the employee</param>
        /// <returns>returns employee details</returns>
        public Employee GetEmployeeById(int id)
        {
            return context.Employee.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Method for getting employee by name
        /// </summary>
        /// <param name="firstName">Firstname of the employee</param>
        /// <param name="lastName">Lastname of the employee</param>
        /// <returns>returns details of the employee</returns>
        public Employee GetEmployeeByName(string firstName, string lastName)
        {
            return context.Employee.FirstOrDefault(x => x.FirstName == firstName && x.LastName == lastName);
        }

        /// <summary>
        /// Method for getting the list of all the employees
        /// </summary>
        /// <returns>returns list of all the employees</returns>
        public List<Employee> GetEmployees()
        {
            return context.Employee.ToList();
        }

        /// <summary>
        /// Method for geting employee using the stored procedure
        /// </summary>
        /// <returns>returns list of all employees</returns>
        public List<EmployeesDepartments> GetEmployeesDepartments(string id)
        {
            return context.EmployeesDepartments.FromSqlRaw<EmployeesDepartments>("GetEmployee " + id).ToList();
        }

        /// <summary>
        /// Method for updating the employee
        /// </summary>
        /// <param name="employee">Object value of the employee model</param>
        /// <returns></returns>
        public void UpdateEmployee(Employee employee)
        {
                context.Employee.Update(employee);
                context.SaveChanges();
        }
    }
}
