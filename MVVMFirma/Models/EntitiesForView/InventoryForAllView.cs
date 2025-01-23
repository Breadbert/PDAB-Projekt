using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.Models.EntitiesForView
{
    public class InventoryForAllView
    {
        public int InventoryID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int? Quantity { get; set; }
        public int? MinimumStockLevel { get; set; }
        public DateTime? RestockDate { get; set; }
    }
}
