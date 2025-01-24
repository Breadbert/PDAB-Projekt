using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.Models.EntitiesForView
{
    public class OrderItemsForAllView
    {
        public int OrderItemID { get; set; }
        public int OrderID { get; set; }
        public int? ProductID { get; set; }
        public string ProductName { get; set; }
        public string Category {  get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public int? DiscountID { get; set; }
        public decimal? DiscountPercentage { get; set; }
    }
}
