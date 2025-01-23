using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMFirma.Models.Entities;
using System.Windows.Documents;
using MVVMFirma.Models.EntitiesForView;
using MVVMFirma.Helper;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;

namespace MVVMFirma.ViewModels
{
    public class AllInvoicesViewModel : AllViewModels<InvoiceForAllView>
    {

        #region Constructor
        public AllInvoicesViewModel()
            : base("Invoices")
        {
        }
        #endregion

        #region Properties
        private InvoiceForAllView _ChosenInvoice;
        public InvoiceForAllView ChosenInvoice
        {
            get
            {
                return _ChosenInvoice;
            }
            set
            {
                _ChosenInvoice = value;
                Messenger.Default.Send(_ChosenInvoice);
                OnRequestClose();
            }
        }
        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<InvoiceForAllView>
                (
                    from invoice in pdabEntities.Invoice
                    select new InvoiceForAllView
                    {
                        InvoiceID = invoice.InvoiceID,
                        Number = invoice.Number,
                        IsApproved = invoice.IsApproved,
                        IssueDate = invoice.IssueDate,
                        DueDate = invoice.DueDate,
                        ContractorNIP = invoice.Contractor.NIP,
                        ContractorName = invoice.Contractor.Name,
                        PaymentMethod = invoice.PaymentMethod.Method,
                    }
                );
        }

        #region Sort and Find
        public override List<string> getComboBoxSortList()
        {
            return new List<string> { "IsApproved", "IssueDate", "DueDate", "ContractorName", "IsApproved and PaymentMethod" };
        }

        public override void Sort()
        {
            if (SortField == "IsApproved")
                List = new ObservableCollection<InvoiceForAllView>(List.OrderBy(item => item.IsApproved));
            if (SortField == "IssueDate")
                List = new ObservableCollection<InvoiceForAllView>(List.OrderBy(item => item.IssueDate));
            if (SortField == "DueDate")
                List = new ObservableCollection<InvoiceForAllView>(List.OrderBy(item => item.DueDate));
            if (SortField == "ContractorName")
                List = new ObservableCollection<InvoiceForAllView>(List.OrderBy(item => item.ContractorName));
            if (SortField == "IsApproved and PaymentMethod")
                List = new ObservableCollection<InvoiceForAllView>(List.OrderBy(item => item.IsApproved).OrderBy(item => item.PaymentMethod));
        }

        public override List<string> getComboBoxFindList()
        {
            return new List<string>() { "DueDate", "ContractorNIP", "ContractorName" };
        }

        public override void Find()
        {
            Load();
            if (FindField == "DueDate")
                List = new ObservableCollection<InvoiceForAllView>(List.Where(item => item.DueDate != null && item.DueDate.ToString().StartsWith(FindTextBox)));
            if (FindField == "ContractorNIP")
                List = new ObservableCollection<InvoiceForAllView>(List.Where(item => item.ContractorNIP != null && item.ContractorNIP.StartsWith(FindTextBox)));
            if (FindField == "ContractorName")
                List = new ObservableCollection<InvoiceForAllView>(List.Where(item => item.ContractorName != null && item.ContractorName.StartsWith(FindTextBox)));
        }
        #endregion
    }
    #endregion
}