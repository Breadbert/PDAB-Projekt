using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using MVVMFirma.Models.Entities;
using MVVMFirma.Helper;
using System.Windows.Input;
using MVVMFirma.Models.EntitiesForView;
using GalaSoft.MvvmLight.Messaging;

namespace MVVMFirma.ViewModels
{
    public class AllProductsViewModel : AllViewModels<ProductsForAllView>
    {

        #region Constructor
        public AllProductsViewModel()
            : base("Products")
        {
        }
        #endregion

        #region Properties
        private ProductsForAllView _ChosenProduct;
        public ProductsForAllView ChosenProduct
        {
            get
            {
                return _ChosenProduct;
            }
            set
            {
                _ChosenProduct = value;
                Messenger.Default.Send(_ChosenProduct);
                OnRequestClose();
            }
        }
        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<ProductsForAllView>
                (
                    from product in pdabEntities.Product
                    select new ProductsForAllView
                    {
                        ProductID = product.ProductID,
                        Name = product.Name,
                        Price = product.Price,
                        Cost = product.Cost,
                        Category = product.Categories.Name
                    }
                );
        }
        #endregion

        #region Sort and Find
        public override List<string> getComboBoxSortList()
        {
            return new List<string> { "Name", "Price", "Cost", "Category" };
        }

        public override void Sort()
        {
            if (SortField == "Name")
                List = new ObservableCollection<ProductsForAllView>(List.OrderBy(item => item.Name));
            if (SortField == "Price")
                List = new ObservableCollection<ProductsForAllView>(List.OrderBy(item => item.Price));
            if (SortField == "Cost")
                List = new ObservableCollection<ProductsForAllView>(List.OrderBy(item => item.Cost));
            if (SortField == "Category")
                List = new ObservableCollection<ProductsForAllView>(List.OrderBy(item => item.Category));
        }

        public override List<string> getComboBoxFindList()
        {
            return new List<string>() { "Name", "OrderDate", "ShippedDate" };
        }

        public override void Find()
        {
            Load();
            if (FindField == "Name")
                List = new ObservableCollection<ProductsForAllView>(List.Where(item => item.Name != null && item.Name.StartsWith(FindTextBox)));
        }
        #endregion
    }
}