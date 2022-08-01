using EmployeeSystem.DataAccess.Entities;
using EmployeeSystem.DataAccess.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmployeeSystem.DataAccess.DbContext
{
    public partial class EmployeeDbContext : IdentityDbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options)
            : base(options)
        {
        }
        public new DbSet<ApplicationUser> Users { get; set; }
        public new DbSet<Role> Roles { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<EmployeesDepartments> EmployeesDepartments { get; set; }
    }
}
