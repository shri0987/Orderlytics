using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Model
{
    [Table("FoodItems")]
    public class FoodItem
    {
        [Key]
        [Required]
        [RegularExpression("ITEM[0-9]{3}")]
        public string FoodId { get; set; }

        [Required]
        public string ItemName { get; set; }

        public string ItemCategory { get; set; }

        public double ItemPrice { get; set; }

        public string ItemDescription { get; set; }

        [Required]
        public bool IsAvailable { get; set; }

    }
}
