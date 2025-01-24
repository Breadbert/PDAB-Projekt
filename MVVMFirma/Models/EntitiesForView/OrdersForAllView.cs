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
        public int? CustomerID {  get; set; }
        public string CustomerName { get; set; }
        public string CustomerLastName { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public string PaymentMethod { get; set; }
    }
}
