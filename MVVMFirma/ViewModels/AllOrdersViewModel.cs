using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMFirma.Models.Entities;
using MVVMFirma.Models.EntitiesForView;

namespace MVVMFirma.ViewModels
{
    public class AllOrdersViewModel : AllViewModels<OrdersForAllView>
    {
        #region Constructor
        public AllOrdersViewModel()
            : base("Orders")
        {
        }
        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<OrdersForAllView>
                (
                    from orders in pdabEntities.OrderItems
                    select new OrdersForAllView
                    {
                        OrderID = orders.OrderID,
                        ProductID = orders.Product.ProductID,
                        ProductName = orders.Product.Name,
                        Quantity = orders.Quantity,
                        DiscountPercentage = orders.Discounts.Percentage,
                        OrderDate = orders.Orders.OrderDate,
                        ShippedDate = orders.Orders.ShippedDate,
                        PaymentMethod = orders.Orders.PaymentMethod.Method
                    }
                );
        }
        #endregion

        #region Sort and Find
        public override List<string> getComboBoxSortList()
        {
            return new List<string> { "ProductName", "OrderDate", "ShippedDate", "PaymentMethod" };
        }

        public override void Sort()
        {
            if (SortField == "ProductName")
                List = new ObservableCollection<OrdersForAllView>(List.OrderBy(item => item.ProductName));
            if (SortField == "OrderDate")
                List = new ObservableCollection<OrdersForAllView>(List.OrderBy(item => item.OrderDate));
            if (SortField == "ShippedDate")
                List = new ObservableCollection<OrdersForAllView>(List.OrderBy(item => item.ShippedDate));
            if (SortField == "PaymentMethod")
                List = new ObservableCollection<OrdersForAllView>(List.OrderBy(item => item.PaymentMethod));
        }

        public override List<string> getComboBoxFindList()
        {
            return new List<string>() { "ProductName", "OrderDate", "ShippedDate" };
        }

        public override void Find()
        {
            Load();
            if (FindField == "ProductName")
                List = new ObservableCollection<OrdersForAllView>(List.Where(item => item.ProductName != null && item.ProductName.StartsWith(FindTextBox)));
            if (FindField == "OrderDate")
                List = new ObservableCollection<OrdersForAllView>(List.Where(item => item.OrderDate != null && item.OrderDate.ToString().StartsWith(FindTextBox)));
            if (FindField == "ShippedDate")
                List = new ObservableCollection<OrdersForAllView>(List.Where(item => item.ShippedDate != null && item.ShippedDate.ToString().StartsWith(FindTextBox)));
        }
        #endregion
    }
}
