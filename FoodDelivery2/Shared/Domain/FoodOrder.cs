using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FoodDelivery2.Shared.Domain
{
	public class FoodOrder:BaseDomainModel
	{
		public FoodOrder()
		{
			this.FoodOrder1Order = new HashSet<Order>();
			this.FoodOrder2Order = new HashSet<Order>();
			this.FoodOrder3Order = new HashSet<Order>();
		}
        [Required]
        public int? Qty { get; set; }
		public string? Remarks { get; set; }
		public int? FoodId { get; set; }
		public virtual Food? Food { get; set; }
		public double? FoodPrice { get; set; }
        public int? FoodCalories { get; set; }
        public double? FoodProtein { get; set; }
        public double? FoodFats { get; set; }
        public double? TotalPrice { get; set; }
		public int? TotalCalories { get; set; }
		public double? TotalProtein { get; set; }
		public double? TotalFats { get; set; }
		[InverseProperty("FoodOrder1")]
		public virtual ICollection<Order> FoodOrder1Order { get; set; }
		[InverseProperty("FoodOrder2")]
		public virtual ICollection<Order> FoodOrder2Order { get; set; }
		[InverseProperty("FoodOrder3")]
		public virtual ICollection<Order> FoodOrder3Order { get; set; }

		//public virtual List<Order>? Orders { get; set; }
		//public int OrderId { get; set; }
		//public virtual Order? Order { get; set; }
		public void UpdateTotalPrice()
		{
			TotalPrice = FoodPrice * Qty ?? 0; // Handle nulls appropriately
		}
        public void UpdateTotalCalories()
        {
            TotalCalories = FoodCalories * Qty ?? 0; // Handle nulls appropriately
        }
        public void UpdateTotalProtein()
        {
            TotalProtein = FoodProtein * Qty ?? 0; // Handle nulls appropriately
        }
        public void UpdateTotalFats()
        {
            TotalFats = FoodFats * Qty ?? 0; // Handle nulls appropriately
        }


    }
}