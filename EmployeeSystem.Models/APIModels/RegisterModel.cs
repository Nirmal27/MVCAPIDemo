using System.ComponentModel.DataAnnotations;

namespace EmployeeSystem.Models.APIModels
{
    public class RegisterModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(32, ErrorMessage = "Please enter valid password", MinimumLength = 8)]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
