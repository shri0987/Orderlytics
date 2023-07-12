using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#pragma warning disable CS8618

namespace BusinessLayer.Model
{
    [Table("Employees")]
    public class Employee
    {
        [Key]
        [RegularExpression("EMP[0-9]{14}")]
        public string EmployeeId { get; set; }

        [Required]
        public string EmployeeFirstName { get; set; }

        public string EmployeeLastName { get; set; }

        [Required]
        public string Role { get; set; }

        [Required]
        public double Salary { get; set; }

        //[Required]
        //public DateTime DateOfJoining { get; set; }

        //[Required]
        //public DateTime DateOfExit { get; set; } = DateTime.Parse("01/01/2099");

        public string EmployeeEmail { get; set; }

        public long EmployeePhoneNumber { get; set; }

    }
}