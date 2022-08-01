using EmployeeSystem.DataAccess.Entities;
using System.Collections.Generic;

namespace EmployeeSystem.Services.Abstraction
{
    public interface IEmployeeHelper
    {        
        Employee GetEmployeeById(int id);
        List<Employee> GetEmployees();
        List<EmployeesDepartments> GetEmployeesDepartments(int id = 0);
        bool AddEmployee(Employee employee);
        bool DeleteEmployee(int id);
        bool UpdateEmployee(EmployeesDepartments employee);
        bool CheckDepartmentExist(int id);
    }
}
