using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMFirma.Helper;
using MVVMFirma.Models.Entities;
using System.Windows.Input;
using MVVMFirma.Validators;
using System.ComponentModel;

namespace MVVMFirma.ViewModels
{
    public class NewAddressViewModel : WorkspaceViewModel
    {
        #region DB
        private PDABv2Entities pdabEntities;
        #endregion

        #region Item
        private Address address;
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
        public NewAddressViewModel()
        {
            base.DisplayName = "New Address";
            pdabEntities = new PDABv2Entities();
            address = new Address();
        }
        #endregion

        #region Properties

        public string City
        {
            get { return address.City; }
            set
            {
                address.City = value;
                OnPropertyChanged(() => City);
            }
        }

        public string Street
        {
            get { return address.Street; }
            set
            {
                address.Street = value;
                OnPropertyChanged(() => Street);
            }
        }

        public string Country
        {
            get { return address.Country; }
            set
            {
                address.Country = value;
                OnPropertyChanged(() => Country);
            }
        }

        public string PostalCode
        {
            get { return address.PostalCode; }
            set
            {
                address.PostalCode = value;
                OnPropertyChanged(() => PostalCode);
            }
        }

        public string HouseNumber
        {
            get { return address.HouseNumber; }
            set
            {
                address.HouseNumber = value;
                OnPropertyChanged(() => HouseNumber);
            }
        }

        public string ApartmentNumber
        {
            get { return address.ApartmentNumber; }
            set
            {
                address.ApartmentNumber = value;
                OnPropertyChanged(() => ApartmentNumber);
            }
        }
        #endregion

        #region Helpers
        public void Save()
        {
            pdabEntities.Address.Add(address);
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
