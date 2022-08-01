using EmployeeSystem.Models.Models;
using System.Collections.Generic;

namespace EmployeeSystem.Web.Abstraction
{
    public interface IEmployeeHelper
    {
        EmployeesDepartmentsViewModel GetEmployeeDepartment(int id = 0);
        List<EmployeesDepartmentsViewModel> GetEmployeesDepartments();
        bool AddEmployee(EmployeeViewModel employee);
        bool EditEmployee(EmployeesDepartmentsViewModel employee);
        bool DeleteEmployee(int id);
    }
}
