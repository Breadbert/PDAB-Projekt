using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MVVMFirma.Models.Entities;
using MVVMFirma.Models.EntitiesForView;
using MVVMFirma.Helper;

namespace MVVMFirma.ViewModels
{
    public class AllOrderItemsViewModel : AllViewModels<OrderItemsForAllView>
    {
        #region Constructor
        public AllOrderItemsViewModel()
            : base("Order Items")
        {
        }
        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<OrderItemsForAllView>(
                from orderItem in pdabEntities.OrderItems
                select new OrderItemsForAllView
                {
                    OrderItemID = orderItem.OrderItemID,
                    OrderID = orderItem.OrderID,
                    ProductID = orderItem.ProductID,
                    ProductName = orderItem.Product.Name,
                    Category = orderItem.Product.Categories.Name,
                    Quantity = orderItem.Quantity,
                    Price = orderItem.Price,
                    DiscountID = orderItem.DiscountID,
                    DiscountPercentage = orderItem.Discounts.Percentage
                }
            );
        }
        #endregion

        #region Sort and Find
        public override List<string> getComboBoxSortList()
        {
            return new List<string> { "ProductName", "Category", "Price", "DiscountPercentage" };
        }

        public override void Sort()
        {
            if (SortField == "ProductName")
                List = new ObservableCollection<OrderItemsForAllView>(List.OrderBy(item => item.ProductName));
            if (SortField == "Category")
                List = new ObservableCollection<OrderItemsForAllView>(List.OrderBy(item => item.Category));
            if (SortField == "Price")
                List = new ObservableCollection<OrderItemsForAllView>(List.OrderBy(item => item.Price));
            if (SortField == "DiscountPercentage")
                List = new ObservableCollection<OrderItemsForAllView>(List.OrderBy(item => item.DiscountPercentage));
        }

        public override List<string> getComboBoxFindList()
        {
            return new List<string> { "ProductName", "Category" };
        }

        public override void Find()
        {
            Load();
            if (FindField == "ProductName")
                List = new ObservableCollection<OrderItemsForAllView>(
                    List.Where(item => item.ProductName != null && item.ProductName.StartsWith(FindTextBox)));
            if (FindField == "Category")
                List = new ObservableCollection<OrderItemsForAllView>(
                    List.Where(item => item.Category != null && item.Category.StartsWith(FindTextBox)));
        }
        #endregion
    }
}
