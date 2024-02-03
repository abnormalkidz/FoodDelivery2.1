using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery2.Shared.Domain
{
    public class Menu : BaseDomainModel
    {
        [Required]
        public string? Cuisine { get; set; }
        public string? CuisinePhoto { get; set; }
        public int? FoodId { get; set; }
        public virtual Food? Food { get; set; }
        public int? RestaurantId { get; set; }
        public virtual Restaurant? Restaurant { get; set; }



    }
}