using System;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;
using MVVMFirma.Helper;
using MVVMFirma.Models.BusinessLogic;
using MVVMFirma.Models.Entities;
using MVVMFirma.Models.EntitiesForView;

namespace MVVMFirma.ViewModels
{
    public class NewOrderItemViewModel : OneViewModel<OrderItems>
    {
        #region Constructor
        public NewOrderItemViewModel()
            : base("OrderItem")
        {
            item = new OrderItems();
            Messenger.Default.Register<ProductsForAllView>(this, getChosenProduct);
            Messenger.Default.Register<OrdersForAllView>(this, getChosenOrder);
        }
        #endregion

        #region Fields
        public int OrderID
        {
            get { return item.OrderID; }
            set
            {
                item.OrderID = value;
                OnPropertyChanged(() => OrderID);
            }
        }

        public int? ProductID
        {
            get { return item.ProductID; }
            set
            {
                item.ProductID = value;
                OnPropertyChanged(() => ProductID);
            }
        }

        public string ProductName { get; set; }
        public decimal? Price
        {
            get { return item.Price; }
            set
            {
                item.Price = value;
                OnPropertyChanged(() => Price);
            }
        }

        public int? Quantity
        {
            get { return item.Quantity; }
            set
            {
                item.Quantity = value;
                OnPropertyChanged(() => Quantity);
            }
        }

        public int? DiscountID
        {
            get { return item.DiscountID; }
            set
            {
                item.DiscountID = value;
                OnPropertyChanged(() => DiscountID);
            }
        }

        public decimal? DiscountPercentage { get; set; }

        #endregion

        #region Command
        private BaseCommand _ShowOrders;
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

        public ICommand ShowOrders
        {
            get
            {
                if (_ShowOrders == null)
                    _ShowOrders = new BaseCommand(() => showOrders());
                return _ShowOrders;
            }
        }

        private void showProducts()
        {
            Messenger.Default.Send<string>("ProductsAll");
        }

        private void showOrders()
        {
            Messenger.Default.Send<string>("OrdersAll");
        }
        #endregion

        #region Properties for ComboBox
        public IQueryable<KeyAndValue> DiscountsItems
        {
            get
            {
                return new DiscountsB(pdabEntities).GetDiscountsKeyAndValueItems();
            }
        }
        #endregion

        #region Helpers
        private void getChosenProduct(ProductsForAllView product)
        {
            ProductID = product.ProductID;
            ProductName = product.Name;
            Price = product.Price;
        }

        private void getChosenOrder(OrdersForAllView order)
        {
            OrderID = order.OrderID;
        }

        public override void Save()
        {
            pdabEntities.OrderItems.Add(item);
            pdabEntities.SaveChanges();
        }
        #endregion
    }
}
