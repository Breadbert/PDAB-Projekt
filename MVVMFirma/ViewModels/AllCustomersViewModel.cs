using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using MVVMFirma.Models.EntitiesForView;

namespace MVVMFirma.ViewModels
{
    public class AllCustomersViewModel : AllViewModels<CustomersForAllView>
    {
        #region Constructor
        public AllCustomersViewModel()
            : base("Customers")
        {
        }
        #endregion

        #region Properties
        private CustomersForAllView _ChosenCustomer;
        public CustomersForAllView ChosenCustomer
        {
            get
            {
                return _ChosenCustomer;
            }
            set
            {
                _ChosenCustomer = value;
                Messenger.Default.Send(_ChosenCustomer);
                OnRequestClose();
            }
        }
        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<CustomersForAllView>
                (
                    from customer in pdabEntities.Customers
                    select new CustomersForAllView
                    {
                        CustomerID = customer.CustomerID,
                        FirstName = customer.FirstName,
                        LastName = customer.LastName,
                        Phone = customer.Phone,
                        Country = customer.Address.Country,
                        City = customer.Address.City,
                        Street = customer.Address.Street,
                        PostalCode = customer.Address.PostalCode,
                        HouseNumber = customer.Address.HouseNumber,
                        ApartmentNumber = customer.Address.ApartmentNumber
                    }
                );
        }
        #endregion

        #region Sort and Find
        public override List<string> getComboBoxSortList()
        {
            return new List<string> { "FirstName", "LastName", "LastName and Country"};
        }

        public override void Sort()
        {
            if (SortField == "FirstName")
                List = new ObservableCollection<CustomersForAllView>(List.OrderBy(item => item.FirstName));
            if (SortField == "LastName")
                List = new ObservableCollection<CustomersForAllView>(List.OrderBy(item => item.LastName));
            if (SortField == "LastName and Country")
                List = new ObservableCollection<CustomersForAllView>(List.OrderBy(item => item.LastName).OrderBy(item => item.Country));
        }

        public override List<string> getComboBoxFindList()
        {
            return new List<string>() { "FirstName", "LastName", "FirstName and LastName", "Phone" };
        }

        public override void Find()
        {
            Load();
            if (FindField == "FirstName")
                List = new ObservableCollection<CustomersForAllView>(List.Where(item => item.FirstName != null && item.FirstName.StartsWith(FindTextBox)));
            if (FindField == "LastName")
                List = new ObservableCollection<CustomersForAllView>(List.Where(item => item.LastName != null && item.LastName.StartsWith(FindTextBox)));
            if (FindField == "FirstName and LastName")
                List = new ObservableCollection<CustomersForAllView>(List.Where(item => item.FirstName != null || item.LastName != null && item.FirstName.StartsWith(FindTextBox) && item.LastName.StartsWith(FindTextBox)));
            if (FindField == "Phone")
                List = new ObservableCollection<CustomersForAllView>(List.Where(item => item.Phone != null && item.Phone.StartsWith(FindTextBox)));
        }
        #endregion
    }
}
