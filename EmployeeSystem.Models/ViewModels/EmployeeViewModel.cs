using System.ComponentModel.DataAnnotations;

namespace EmployeeSystem.Models.Models
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public string BirthDate { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public int DepartmentID { get; set; }
    }

    public class EmployeesDepartmentsViewModel
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public string BirthDate { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
    }
}
