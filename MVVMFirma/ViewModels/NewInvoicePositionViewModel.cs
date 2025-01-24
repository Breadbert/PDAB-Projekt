using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;
using MVVMFirma.Helper;
using MVVMFirma.Models.BusinessLogic;
using MVVMFirma.Models.Entities;
using MVVMFirma.Models.EntitiesForView;

namespace MVVMFirma.ViewModels
{
    public class NewInvoicePositionViewModel : OneViewModel<InvoicePosition>
    {
        #region Contructor
        public NewInvoicePositionViewModel()
            : base("InvoicePosition")
        {
            item = new InvoicePosition();
            Messenger.Default.Register<InvoiceForAllView>(this, getChosenInvoice);
            Messenger.Default.Register<ProductsForAllView>(this, getChosenProduct);
        }
        #endregion

        #region Fields
        public int? InvoiceID
        {
            get
            {
                return item.InvoiceID;
            }
            set
            {
                item.InvoiceID = value;
                OnPropertyChanged(() => InvoiceID);
            }
        }
        public string InvoiceNumber { get; set; }
        public int? ProductID
        {
            get
            {
                return item.ProductID;
            }
            set
            {
                item.ProductID = value;
                OnPropertyChanged(() => ProductID);
            }
        }
        public string ProductName { get; set; }
        public decimal? ProductCost { get; set; }

        public int? Amount
        {
            get
            {
                return item.Amount;
            }
            set
            {
                item.Amount = value;
                OnPropertyChanged(() => Amount);
            }
        }

        #endregion

        #region Command
        private BaseCommand _ShowInvoices;
        private BaseCommand _ShowProducts;
        public ICommand ShowInvoices
        {
            get
            {
                if (_ShowInvoices == null)
                    _ShowInvoices = new BaseCommand(() => showInvoices());
                return _ShowInvoices;
            }
        }
        public ICommand ShowProducts
        {
            get
            {
                if (_ShowProducts == null)
                    _ShowProducts = new BaseCommand(() => showProducts());
                return _ShowProducts;
            }
        }

        private void showInvoices()
        {
            Messenger.Default.Send<string>("InvoicesAll");
        }

        private void showProducts()
        {
            Messenger.Default.Send<string>("ProductsAll");
        }
        #endregion

        #region Helpers
        private void getChosenInvoice(InvoiceForAllView invoice)
        {
            InvoiceID = invoice.InvoiceID;
            InvoiceNumber = invoice.Number;
        }

        private void getChosenProduct(ProductsForAllView product)
        {
            ProductID = product.ProductID;
            ProductName = product.Name;
            ProductCost = product.Cost;
        }
        public override void Save()
        {
            item.Price = ProductCost;
            pdabEntities.InvoicePosition.Add(item);
            pdabEntities.SaveChanges();
        }
        #endregion
    }
}
