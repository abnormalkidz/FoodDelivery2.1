using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery2.Shared.Domain
{
    public class Food : BaseDomainModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "Food name is too long.")]
        public string? FoodItem { get; set; }
        [Required]
        public string? Size { get; set; }
        [Required]
        public double? Price { get; set; }
        [Required]
        public bool Halal { get; set; }
        [Required]
        public int? Calories { get; set; }
        [Required]
        public double? Protein { get; set; }
        [Required]
        public double? Fats { get; set; }
        public string? Allergies { get; set; }

        public string? Remarks { get; set; }
        public virtual List<Menu>? Menus { get; set; }

    }
}