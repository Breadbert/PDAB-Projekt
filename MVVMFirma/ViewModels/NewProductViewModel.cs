using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using MVVMFirma.Helper;
using MVVMFirma.Models.Entities;

namespace MVVMFirma.ViewModels
{
    public class NewProductViewModel : WorkspaceViewModel
    {
        #region DB
        private PDABEntities pdabEntities;
        #endregion

        #region Item
        private Products product;
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
        public NewProductViewModel()
        {
            base.DisplayName = "Product";
            pdabEntities = new PDABEntities();
            product = new Products();
        }
        #endregion

        #region Properties
/*        public int ID
        {
            get { return product.ProductID; }
            set 
            {
                product.ProductID = value;
                OnPropertyChanged(() => ProductID);
            }
        }*/

        public string ProductName
        {
            get { return product.ProductName; }
            set
            {
                product.ProductName = value;
                OnPropertyChanged(() => ProductName);
            }
        }

        public decimal? Price
        {
            get { return product.Price; }
            set
            {
                product.Price = value;
                OnPropertyChanged(() => Price);
            }
        }

        public int? StockQuantity
        {
            get { return product.StockQuantity; }
            set
            {
                product.StockQuantity = value;
                OnPropertyChanged(() => StockQuantity);
            }
        }

        public int? CategoryID
        {
            get { return product.CategoryID; }
            set
            {
                product.CategoryID = value;
                OnPropertyChanged(() => CategoryID);
            }
        }
        #endregion

        #region Helpers
        public void Save()
        {
            pdabEntities.Products.Add(product);
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
