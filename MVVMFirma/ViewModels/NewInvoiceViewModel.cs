using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMFirma.Helper;
using System.Windows.Input;
using MVVMFirma.Models.BusinessLogic;
using MVVMFirma.Models.Entities;
using MVVMFirma.Models.EntitiesForView;
using GalaSoft.MvvmLight.Messaging;
using MVVMFirma.Validators;
using System.Xml.Linq;
using System.ComponentModel;

namespace MVVMFirma.ViewModels
{
    public class NewInvoiceViewModel : OneViewModel<Invoice>, IDataErrorInfo
    {
        #region Contructor
        public NewInvoiceViewModel()
            : base("Invoice")
        {
            item = new Invoice();
            Messenger.Default.Register<ContractorsForAllView>(this, getChosenContractor);
        }
        #endregion

        #region Command
        private BaseCommand _ShowContractors;
        public ICommand ShowContractors
        {
            get
            {
                if (_ShowContractors == null)
                    _ShowContractors = new BaseCommand(() => showContractors());
                return _ShowContractors;
            }
        }

        private void showContractors()
        {
            Messenger.Default.Send<string>("ContractorsAll");
        }
        #endregion

        #region Fields
        public int? ContractorID
        {
            get
            {
                return item.ContractorID;
            }
            set
            {
                item.ContractorID = value;
                OnPropertyChanged(() => ContractorID);
            }
        }
        public string ContractorName { get; set; }
        public string ContractorNIP { get; set; }

        public bool? IsApproved
        {
            get
            {
                return item.IsApproved;
            }
            set
            {
                item.IsApproved = value;
                OnPropertyChanged(() => IsApproved);
            }
        }

        public string Number
        {
            get
            { return item.Number; }
            set
            {
                item.Number = value;
                OnPropertyChanged(() => Number);
            }
        }

        public DateTime? IssueDate
        {
            get
            { return item.IssueDate; }
            set
            {
                item.IssueDate = value;
                OnPropertyChanged(() => IssueDate);
            }
        }

        public DateTime? DueDate
        {
            get
            { return item.DueDate; }
            set
            {
                item.DueDate = value;
                OnPropertyChanged(() => DueDate);
            }
        }

        public int? PaymentMethodID
        {
            get
            { return item.PaymentMethodID; }
            set
            {
                item.PaymentMethodID = value;
                OnPropertyChanged(() => PaymentMethodID);
            }
        }

        #endregion

        #region Properties for ComboBox
        public IQueryable<KeyAndValue> PaymentMethodItems
        {
            get
            {
                return new PaymentMethodB(pdabEntities).GetPaymentMethodKeyAndValueItems();
            }
        }
        #endregion

        #region Helpers
        private void getChosenContractor(ContractorsForAllView contractor)
        {
            ContractorID = contractor.ContractorID;
            ContractorName = contractor.Name;
            ContractorNIP = contractor.NIP;
        }
        public override void Save()
        {
            pdabEntities.Invoice.Add(item);
            pdabEntities.SaveChanges();
        }
        #endregion

        #region Validation
        private string _validationMessage = string.Empty;
        public string this[string propertiesName]
        {
            get
            {
                if (propertiesName == nameof(Number))
                {
                    return NewInvoiceValidator.ValidateInvoiceNumber(Number);
                }
                return string.Empty;
            }
        }
        public string Error => string.Empty;
        protected override bool IsValid()
        {
            return string.IsNullOrEmpty(_validationMessage);
        }
        #endregion
    }
}
