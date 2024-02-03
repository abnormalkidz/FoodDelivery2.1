using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery2.Shared.Domain
{
    public class Driver : BaseDomainModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "Name is too long.")]
        public string? DriverName { get; set; }
        [Required]
        public string? ModeOfTransport { get; set; }

        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email Address inputed is not a valid email.")]
        [EmailAddress]
        public string? DriverEmail { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"(6|8|9)\d{7}", ErrorMessage = "Contact number inputed is not a valid phone number.")]
        public string? DriverPhoneNo { get; set; }
        [Required]
        public string? OrderStatus { get; set; }

        public string? DriverPhoto { get; set; }

        //public int? ReviewId { get; set; }
        //public virtual Review? Review { get; set; }
    }
}