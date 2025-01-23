using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMFirma.Helper;
using MVVMFirma.Models.Entities;
using System.Windows.Input;

namespace MVVMFirma.ViewModels
{
    public class NewPaymentMethodViewModel : WorkspaceViewModel
    {
        #region DB
        private PDABv2Entities pdabEntities;
        #endregion

        #region Item
        private PaymentMethod paymentMethod;
        #endregion

        #region Command
        private BaseCommand _SaveCommand;
        public ICommand SaveCommand
        {
            get
            {
                if (_SaveCommand == null)
                    _SaveCommand = new BaseCommand(() => SaveAndClose());
                return _SaveCommand;
            }
        }
        #endregion

        #region Constructor
        public NewPaymentMethodViewModel()
        {
            base.DisplayName = "New Payment Method";
            pdabEntities = new PDABv2Entities();
            paymentMethod = new PaymentMethod();
        }
        #endregion

        #region Properties

        public string Method
        {
            get { return paymentMethod.Method; }
            set
            {
                paymentMethod.Method = value;
                OnPropertyChanged(() => Method);
            }
        }

        public string Description
        {
            get { return paymentMethod.Description; }
            set
            {
                paymentMethod.Description = value;
                OnPropertyChanged(() => Description);
            }
        }
        #endregion

        #region Helpers
        public void Save()
        {
            pdabEntities.PaymentMethod.Add(paymentMethod);
            pdabEntities.SaveChanges();
        }

        public void SaveAndClose()
        {
            Save();
            OnRequestClose();
        }
        #endregion
    }
}
