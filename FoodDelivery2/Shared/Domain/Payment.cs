using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery2.Shared.Domain
{
    public class Payment : BaseDomainModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "Name is too long.")]
        public string? CreditName { get; set; }
        [Required]
        [RegularExpression(@"\d{16}", ErrorMessage = "Credit Card Number inputed is not a valid Credit Card Nmber.")]
        public string? CreditCardNo { get; set; }
        [Required]
        public string? Bank { get; set; }
        [Required]
        [RegularExpression(@"\d{3}", ErrorMessage = "CVC inputed is not a valid CVC.")]
        public string? Cvc { get; set; }
        [Required]
        public string? CardExpiryDate { get; set; }
        public double? DeliveryFee { get; set; }
        public Payment()
        {
            DeliveryFee = 5;
        }
        public double? FinalPrice { get; set; }
        public int? OrderId { get; set; }
        public virtual Order? Order { get; set; }
        public int? FoodOrderId { get; set; }
        public virtual FoodOrder? FoodOrder { get; set; }
        public int? PromoCodeId { get; set; }
        public virtual PromoCode? PromoCode { get; set; }
        public void UpdateFinalPrice()
        {
            FinalPrice = FoodOrder.TotalPrice + DeliveryFee - PromoCode.Amount ?? 0; // Handle nulls appropriately
        }
    }
}