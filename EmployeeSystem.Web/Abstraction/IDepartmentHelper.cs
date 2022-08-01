using EmployeeSystem.Models.ViewModels;
using System.Collections.Generic;

namespace EmployeeSystem.Web.Abstraction
{
    public interface IDepartmentHelper
    {
        DepartmentViewModel GetDepartmentById(int id = 0);
        List<DepartmentViewModel> GetDepartments();
    }
}
