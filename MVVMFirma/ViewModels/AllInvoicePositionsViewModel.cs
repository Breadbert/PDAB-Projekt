using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMFirma.Models.EntitiesForView;

namespace MVVMFirma.ViewModels
{
    public class AllInvoicePositionsViewModel : AllViewModels<InvoicePositionsForAllView>
    {
        #region Constructor
        public AllInvoicePositionsViewModel()
            : base("InvoicePosition")
        {
        }
        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<InvoicePositionsForAllView>
                (
                    from invoicePosition in pdabEntities.InvoicePosition
                    select new InvoicePositionsForAllView
                    {
                        InvoicePositionID = invoicePosition.InvoicePositionID,
                        InvoiceID = invoicePosition.Invoice.InvoiceID,
                        InvoiceNumber = invoicePosition.Invoice.Number,
                        ProductID = invoicePosition.Product.ProductID,
                        ProductName = invoicePosition.Product.Name,
                        Price = invoicePosition.Product.Price,
                        Amount = invoicePosition.Amount
                    }
                );
        }
        #endregion

        #region Sort and Find
        public override List<string> getComboBoxSortList()
        {
            return new List<string> { "InvoiceNumber", "ProductName" };
        }

        public override void Sort()
        {
            if (SortField == "InvoiceNumber")
                List = new ObservableCollection<InvoicePositionsForAllView>(List.OrderBy(item => item.InvoiceNumber));
            if (SortField == "ProductName")
                List = new ObservableCollection<InvoicePositionsForAllView>(List.OrderBy(item => item.ProductName));
        }

        public override List<string> getComboBoxFindList()
        {
            return new List<string>() { "InvoiceNumber", "ProductName" };
        }

        public override void Find()
        {
            Load();
            if (FindField == "InvoiceNumber")
                List = new ObservableCollection<InvoicePositionsForAllView>(List.Where(item => item.InvoiceNumber != null && item.InvoiceNumber.StartsWith(FindTextBox)));
            if (FindField == "ContractorNIP")
                List = new ObservableCollection<InvoicePositionsForAllView>(List.Where(item => item.ProductName != null && item.ProductName.StartsWith(FindTextBox)));
        }
        #endregion
    }
}
