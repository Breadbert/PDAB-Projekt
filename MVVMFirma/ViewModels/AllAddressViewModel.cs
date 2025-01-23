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
    public class AllAddressViewModel : AllViewModels<AddressForAllView>
    {
        #region Constructor
        public AllAddressViewModel()
            : base("Addresses")
        {
        }
        #endregion

        #region Properties
        private AddressForAllView _ChosenAddress;
        public AddressForAllView ChosenAddress
        {
            get
            {
                return _ChosenAddress;
            }
            set
            {
                _ChosenAddress = value;
                Messenger.Default.Send(_ChosenAddress);
                OnRequestClose();
            }
        }
        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<AddressForAllView>
                (
                    from address in pdabEntities.Address
                    select new AddressForAllView
                    {
                        AddressID = address.AddressID,
                        City = address.City,
                        Street = address.Street,
                        Country = address.Country,
                        PostalCode = address.PostalCode,
                        HouseNumber = address.HouseNumber,
                        ApartmentNumber = address.ApartmentNumber
                    }
                );
        }
        #endregion

        #region Sort and Find
        public override List<string> getComboBoxSortList()
        {
            return new List<string> { "City", "Street", "Country", "PostalCode" };
        }

        public override void Sort()
        {
            if (SortField == "Name")
                List = new ObservableCollection<AddressForAllView>(List.OrderBy(item => item.City));
            if (SortField == "Street")
                List = new ObservableCollection<AddressForAllView>(List.OrderBy(item => item.Street));
            if (SortField == "Country")
                List = new ObservableCollection<AddressForAllView>(List.OrderBy(item => item.Country));
            if (SortField == "PostalCode")
                List = new ObservableCollection<AddressForAllView>(List.OrderBy(item => item.PostalCode));

        }

        public override List<string> getComboBoxFindList()
        {
            return new List<string>() { "City", "Street", "Country", "PostalCode" };
        }

        public override void Find()
        {
            Load();
            if (FindField == "City")
                List = new ObservableCollection<AddressForAllView>(List.Where(item => item.City != null && item.City.StartsWith(FindTextBox)));
            if (FindField == "Street")
                List = new ObservableCollection<AddressForAllView>(List.Where(item => item.Street != null && item.Street.StartsWith(FindTextBox)));
            if (FindField == "Country")
                List = new ObservableCollection<AddressForAllView>(List.Where(item => item.Country != null && item.Country.StartsWith(FindTextBox)));
            if (FindField == "PostalCode")
                List = new ObservableCollection<AddressForAllView>(List.Where(item => item.PostalCode != null && item.PostalCode.StartsWith(FindTextBox)));
        }
        #endregion
    }
}
