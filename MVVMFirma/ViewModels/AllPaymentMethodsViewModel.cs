using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMFirma.Models.Entities;

namespace MVVMFirma.ViewModels
{
    public class AllPaymentMethodsViewModel : AllViewModels<PaymentMethod>
    {
        #region Constructor
        public AllPaymentMethodsViewModel()
            : base("PaymentMethods")
        {
        }
        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<PaymentMethod>
                (
                    pdabEntities.PaymentMethod.ToList()
                );
        }
        #endregion
    }
}
