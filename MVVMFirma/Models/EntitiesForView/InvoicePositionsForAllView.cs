using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.Models.EntitiesForView
{
    public class InvoicePositionsForAllView
    {
        public int? InvoicePositionID { get; set; }
        public int? InvoiceID { get; set; }
        public string InvoiceNumber { get; set; }
        public int? ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal? Price { get; set; }
        public int? Amount { get; set; }
    }
}
