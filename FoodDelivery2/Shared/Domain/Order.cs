using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery2.Shared.Domain
{
    public class Order : BaseDomainModel
    {
        public int? TotalCalories { get; set; }
        public string? CaloriesIndicator { get; set; }
        public double? TotalProtein { get; set; }
        public string? ProteinIndicator { get; set; }
        public double? TotalFats { get; set; }
        public string? FatsIndicator { get; set; }
        public double? TotalPrice { get; set; }
        public int? CustomerId { get; set; }
        public virtual Customer? Customer { get; set; }

        [ForeignKey("FoodOrder1")]
        public int? FoodOrderId1 { get; set; }
        [ForeignKey("FoodOrder2")]
        public int? FoodOrderId2 { get; set; }
        [ForeignKey("FoodOrder3")]
        public int? FoodOrderId3 { get; set; }
        public virtual FoodOrder? FoodOrder1 { get; set; }
        public virtual FoodOrder? FoodOrder2 { get; set; }
        public virtual FoodOrder? FoodOrder3 { get; set; }
        public int? DriverId { get; set; }
        public virtual Driver? Driver { get; set; }
        public virtual List<Payment>? Payments { get; set; }

        //public virtual List<int>? FoodOrderId { get; set; }
        //public int? FoodOrderId { get; set; }

        //public virtual FoodOrder? FoodOrder { get; set; }

    }
}