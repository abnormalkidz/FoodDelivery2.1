using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery2.Shared.Domain
{
    public class Customer : BaseDomainModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "Name is too long.")]
        public string? CustName { get; set; }

        [Required]
        public string? CustAddress { get; set; }

        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email Address inputed is not a valid email.")]
        [EmailAddress]
        public string? CustEmail { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"(6|8|9)\d{7}", ErrorMessage = "Contact number inputed is not a valid phone number.")]
        public string? CustPhoneNo { get; set; }

        [Required]
        public int? TargetCalories { get; set; }

        [Required]
        public double? TargetProtein { get; set; }
        [Required]
        public double? TargetFats { get; set; }
        public int? ReviewId { get; set; }
        public virtual Review? Review { get; set; }
        public virtual List<PromoCode>? PromoCodes { get; set; }
        public virtual List<Order>? Orders { get; set; }
        public virtual List<Customer>? Customers { get; set; }


    }
}