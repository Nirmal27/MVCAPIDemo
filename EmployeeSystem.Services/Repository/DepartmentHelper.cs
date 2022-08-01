using EmployeeSystem.DataAccess.Abstraction;
using EmployeeSystem.DataAccess.Entities;
using EmployeeSystem.Services.Abstraction;
using System.Collections.Generic;

namespace EmployeeSystem.Services.Repository
{
    public class DepartmentHelper : IDepartmentHelper
    {
        private readonly IDepartmentRepository departmentRepository;
        public DepartmentHelper(IDepartmentRepository departmentRepository)
        {
            this.departmentRepository = departmentRepository;
        }
        public List<Department> GetDepartment()
        {
            return departmentRepository.GetDepartments();
        }

        public Department GetDepartmentById(int id)
        {
            return departmentRepository.GetDepartmentById(id);
        }
    }
}
