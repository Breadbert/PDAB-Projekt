using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMFirma.Models.Entities;
using MVVMFirma.Models.EntitiesForView;

namespace MVVMFirma.Models.BusinessLogic
{
    public class PaymentMethodB : DatabaseClass
    {
        #region Constructor
        public PaymentMethodB(PDABv2Entities db)
            : base(db)
        { }
        #endregion

        #region Business Functions
        public IQueryable<KeyAndValue> GetPaymentMethodKeyAndValueItems()
        {
            return
                (
                    from paymentmethod in db.PaymentMethod
                    select new KeyAndValue
                    {
                        Key = paymentmethod.PaymentMethodID,
                        Value = paymentmethod.Method
                    }
                ).ToList().AsQueryable();
        }
        #endregion
    }
}
