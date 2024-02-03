using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery2.Shared.Domain
{
    public class Restaurant : BaseDomainModel
    {
        [Required]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Last name does not meet the length requirements.")]
        public string? RestoName { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Only alphabets are allowed.")]
        public string? Location { get; set; }
        public string? RestaurantPhoto { get; set; }
        public virtual List<Menu>? Menus { get; set; }
        public int? ReviewId { get; set; }
        public virtual Review? Review { get; set; }
    }
}