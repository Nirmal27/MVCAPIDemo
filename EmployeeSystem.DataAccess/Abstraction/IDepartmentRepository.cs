using EmployeeSystem.DataAccess.Entities;
using System.Collections.Generic;

namespace EmployeeSystem.DataAccess.Abstraction
{
    public interface IDepartmentRepository
    {
        List<Department> GetDepartments();
        Department GetDepartmentById(int id);
    }
}
