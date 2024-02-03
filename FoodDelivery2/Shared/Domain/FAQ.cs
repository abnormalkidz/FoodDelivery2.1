using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery2.Shared.Domain
{
    public class FAQ : BaseDomainModel
    {
        [Required]
        public string? Qns { get; set; }
        [Required]
        public string? Ans { get; set; }
    }
}