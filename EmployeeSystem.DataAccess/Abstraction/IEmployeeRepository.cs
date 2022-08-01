using EmployeeSystem.DataAccess.Entities;
using EmployeeSystem.Models.APIModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeSystem.DataAccess.Abstraction
{
    public interface IEmployeeRepository
    {
        Employee GetEmployeeById(int id);
        Employee GetEmployeeByName(string firstName, string lastName);
        List<Employee> GetEmployees();
        List<EmployeesDepartments> GetEmployeesDepartments(string id);
        void AddEmployee(Employee employee);
        void DeleteEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
        Department GetDepartment(int id);
    }
}
