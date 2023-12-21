using System.ComponentModel.DataAnnotations;

namespace Employee_Management_MVC_Application.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Employee Name required!!!")]
        [MaxLength(150)]
        [MinLength(3)]
        public string? Name { get; set; }
        [Required(ErrorMessage = "City Name required!!!")]
        [MaxLength(50)]
        [MinLength(3)]
        public string? City { get; set; }
        [Required(ErrorMessage = "Employee Address required!!!")]
        [MaxLength(150)]
        [MinLength(3)]
        public string? Address { get; set; }
    }
}
