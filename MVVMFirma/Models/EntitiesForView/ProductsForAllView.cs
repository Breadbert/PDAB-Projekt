using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.Models.EntitiesForView
{
    public class ProductsForAllView
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public decimal? Cost { get; set; }
        public string Category { get; set; }
    }
}
