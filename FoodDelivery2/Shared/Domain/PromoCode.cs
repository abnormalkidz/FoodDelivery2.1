using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery2.Shared.Domain
{
    public class PromoCode : BaseDomainModel
    {
        [Required]
        public string? PromoName { get; set; }
        [Required]
        public double? Amount { get; set; }
        public DateTime PCExpiryDate { get; set; }
        public int? CustomerId { get; set; }
        public virtual Customer? Customer { get; set; }
        public virtual List<Payment>? Payments { get; set; }

    }
}