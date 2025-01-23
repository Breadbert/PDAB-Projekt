using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMFirma.Models.Entities;

namespace MVVMFirma.Models.EntitiesForView
{
    public class OrdersForAllView
    {
        public int OrderID { get; set; }
        public string ProductName {  get; set; }
        public int? ProductID { get; set; }
        public int? Quantity { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public string PaymentMethod { get; set; }
    }
}
