using EmployeeSystem.DataAccess.Entities;
using System.Collections.Generic;

namespace EmployeeSystem.Services.Abstraction
{
    public interface IDepartmentHelper
    {
        Department GetDepartmentById(int id);
        List<Department> GetDepartment();
    }
}
