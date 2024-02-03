using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery2.Shared.Domain
{
    public class Review : BaseDomainModel
    {
        //[Required]
        //public string? DriverOrOrder { get; set; }
        [Required]
        public int? Rating { get; set; }
        public string? Remarks { get; set; }
        public int? OrderID { get; set; }
        public virtual Order? Order { get; set; }
        public virtual List<Customer>? Customers { get; set; }
        public virtual Driver? Driver { get; set; }
        public virtual List<Restaurant>? Restaurant { get; set; }
        public int? DriverID { get; set; }
    }
}