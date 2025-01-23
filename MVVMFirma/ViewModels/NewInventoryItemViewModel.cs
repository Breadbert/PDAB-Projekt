using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using MVVMFirma.Helper;
using MVVMFirma.Models.BusinessLogic;
using System.Windows.Input;
using MVVMFirma.Models.Entities;
using MVVMFirma.Models.EntitiesForView;
using System.Windows.Controls;

namespace MVVMFirma.ViewModels
{
    public class NewInventoryItemViewModel : OneViewModel<Inventory>
    {
        #region Contructor
        public NewInventoryItemViewModel()
            : base("Inventory")
        {
            item = new Inventory();
            Messenger.Default.Register<ProductsForAllView>(this, getChosenProduct);
        }
        #endregion

        #region Command
        private BaseCommand _ShowProducts;
        public ICommand ShowProducts
        {
            get
            {
                if (_ShowProducts == null)
                    _ShowProducts = new BaseCommand(() => showProducts());
                return _ShowProducts;
            }
        }

        private void showProducts()
        {
            Messenger.Default.Send<string>("ProductsAll");
        }
        #endregion

        #region Fields
        public int? ProductID
        {
            get
            { return item.ProductID; }
            set
            {
                item.ProductID = value;
                OnPropertyChanged(() => ProductID);
            }
        }

        public string Name { get; set; }
        public int? Quantity
        {
            get
            { return item.Quantity; }
            set
            {
                item.Quantity = value;
                OnPropertyChanged(() => Quantity);
            }
        }

        public int? MinimumStockLevel
        {
            get
            { return item.MinimumStockLevel; }
            set
            {
                item.MinimumStockLevel = value;
                OnPropertyChanged(() => MinimumStockLevel);
            }
        }

        public DateTime? RestockDate
        {
            get
            { return item.RestockDate; }
            set
            {
                item.RestockDate = value;
                OnPropertyChanged(() => RestockDate);
            }
        }

        #endregion
        #region Helpers
        private void getChosenProduct(ProductsForAllView product)
        {
            ProductID = product.ProductID;
            Name = product.Name;
        }
        public override void Save()
        {
            pdabEntities.Inventory.Add(item);
            pdabEntities.SaveChanges();
        }
        #endregion
    }
}
