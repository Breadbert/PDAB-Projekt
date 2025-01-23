using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMFirma.Models.Entities;
using MVVMFirma.Models.EntitiesForView;

namespace MVVMFirma.Models.BusinessLogic
{
    public class ProductB : DatabaseClass
    {
        #region Constructor
        public ProductB(PDABv2Entities db)
            : base(db)
        { }
        #endregion

        #region Business Functions
        public IQueryable<KeyAndValue> GetProductsKeyAndValueItems()
        {
            return
                (
                    from product in db.Product
                    select new KeyAndValue
                    {
                        Key = product.ProductID,
                        Value = product.Name
                    }
                ).ToList().AsQueryable();
        }
        #endregion
    }
}
