using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessLayer.Model
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        [RegularExpression(@"ORD[0-9]{14}")]
        public string OrderId { get; set; }

        [RegularExpression(@"CUST[0-9]{14}")]
        public string CustomerId { get; set; }
        public DateTime OrderDateTime { get; set; }
        public double Amount { get; set; }

    }
}