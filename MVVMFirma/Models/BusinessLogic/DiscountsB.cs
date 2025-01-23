using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMFirma.Models.Entities;
using MVVMFirma.Models.EntitiesForView;

namespace MVVMFirma.Models.BusinessLogic
{
    public class DiscountsB : DatabaseClass
    {
        #region Constructor
        public DiscountsB(PDABv2Entities db)
            : base(db)
        { }
        #endregion

        #region Business Functions
        public IQueryable<KeyAndValue> GetDiscountsKeyAndValueItems()
        {
            return
                (
                    from discount in db.Discounts
                    select new KeyAndValue
                    {
                        Key = discount.DiscountID,
                        Value = discount.Name
                    }
                ).ToList().AsQueryable();
        }
        #endregion
    }
}
