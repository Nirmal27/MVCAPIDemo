using EmployeeSystem.DataAccess.Abstraction;
using EmployeeSystem.DataAccess.DbContext;
using EmployeeSystem.DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeSystem.DataAccess.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly EmployeeDbContext context;
        public DepartmentRepository(EmployeeDbContext context)
        {
            this.context = context;
        }

        public List<Department> GetDepartments()
        {
            return context.Department.ToList();
        }

        public Department GetDepartmentById(int id)
        {
            return context.Department.FirstOrDefault(x => x.Id == id);
        }
    }
}
