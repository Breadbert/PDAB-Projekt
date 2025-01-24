using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
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

        #region Properties
        private OrdersForAllView _ChosenOrder;
        public OrdersForAllView ChosenOrder
        {
            get
            {
                return _ChosenOrder;
            }
            set
            {
                _ChosenOrder = value;
                Messenger.Default.Send(_ChosenOrder);
                OnRequestClose();
            }
        }
        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<OrdersForAllView>
                (
                    from orders in pdabEntities.Orders
                    select new OrdersForAllView
                    {
                        OrderID = orders.OrderID,
                        CustomerID = orders.CustomerID,
                        CustomerName = orders.Customers.FirstName,
                        CustomerLastName = orders.Customers.LastName,
                        ShippedDate = orders.ShippedDate,
                        PaymentMethod = orders.PaymentMethod.Method
                    }
                );
        }
        #endregion

        #region Sort and Find
        public override List<string> getComboBoxSortList()
        {
            return new List<string> { "ShippedDate", "PaymentMethod" };
        }

        public override void Sort()
        {
            if (SortField == "ShippedDate")
                List = new ObservableCollection<OrdersForAllView>(List.OrderBy(item => item.ShippedDate));
            if (SortField == "PaymentMethod")
                List = new ObservableCollection<OrdersForAllView>(List.OrderBy(item => item.PaymentMethod));
        }

        public override List<string> getComboBoxFindList()
        {
            return new List<string>() { "ShippedDate" };
        }

        public override void Find()
        {
            Load();
            if (FindField == "ShippedDate")
                List = new ObservableCollection<OrdersForAllView>(List.Where(item => item.ShippedDate != null && item.ShippedDate.ToString().StartsWith(FindTextBox)));
        }
        #endregion
    }
}
