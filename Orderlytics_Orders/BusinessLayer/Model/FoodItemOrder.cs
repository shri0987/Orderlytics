using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Model
{
    [Table("FoodItemOrders")]
    public class FoodItemOrder
    {
        public string FoodId { get; set; }
        public string OrderId { get; set; }
        public int Quantity { get; set; }

    }
}
