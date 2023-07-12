using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#pragma warning disable CS8618

namespace BusinessLayer.Model
{
    [Table("Customers")]
    public class Customer
    {
        [Key]
        [RegularExpression(@"CUST[0-9]{14}")]
        public string CustomerId { get; set; }
        [Required]
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        [EmailAddress]
        public string CustomerEmail { get; set; }
        [Required]
        public long CustomerPhoneNumber { get; set; }

    }
}