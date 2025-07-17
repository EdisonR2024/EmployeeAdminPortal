using System.ComponentModel.DataAnnotations;

namespace EmployeeAdminPortal.Models
{
    public class AddEmployeeDto
    {
        public required string FirstName { get; set; }
        
        [Required(ErrorMessage = "El apellido es requerido")]
        public string LastName { get; set; } = string.Empty;

        public required string Email { get; set; }

        public string? Phone { get; set; }

        public decimal Salary { get; set; }

    }
}
