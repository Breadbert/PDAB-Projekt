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
using System.ComponentModel;
using MVVMFirma.Validators;
using System.Diagnostics;
using System.Xml.Linq;

namespace MVVMFirma.ViewModels
{
    public class NewCustomerViewModel : OneViewModel<Customers>, IDataErrorInfo
    {
        #region Contructor
        public NewCustomerViewModel()
            : base("Customer")
        {
            item = new Customers();
            Messenger.Default.Register<AddressForAllView>(this, getChosenAddress);
        }
        #endregion

        #region Command
        private BaseCommand _ShowAddresses;
        public ICommand ShowAddresses
        {
            get
            {
                if (_ShowAddresses == null)
                    _ShowAddresses = new BaseCommand(() => showAddresses());
                return _ShowAddresses;
            }
        }

        private void showAddresses()
        {
            Messenger.Default.Send<string>("AddressesAll");
        }
        #endregion

        #region Fields
        public string Country { get; set; }
        public string City { get; set; }
        public string FirstName
        {
            get
            {
                return item.FirstName;
            }
            set
            {
                item.FirstName = value;
                OnPropertyChanged(() => FirstName);
            }
        }

        public string LastName
        {
            get
            { return item.LastName; }
            set
            {
                item.LastName = value;
                OnPropertyChanged(() => LastName);
            }
        }

        public string Phone
        {
            get
            { return item.Phone; }
            set
            {
                item.Phone = value;
                OnPropertyChanged(() => Phone);
            }
        }

        public int? AddressID
        {
            get
            { return item.AddressID; }
            set
            {
                item.AddressID = value;
                OnPropertyChanged(() => AddressID);
            }
        }
        #endregion

        #region Helpers
        private void getChosenAddress(AddressForAllView address)
        {
            AddressID = address.AddressID;
            Country = address.Country;
            City = address.City;
        }

        public override void Save()
        {
            pdabEntities.Customers.Add(item);
            pdabEntities.SaveChanges();
        }
        #endregion

        #region Validation
        private string _validationMessage = string.Empty;
        public string this[string propertiesName]
        {
            get
            {
                if (propertiesName == nameof(FirstName))
                {
                    return NewCustomerValidator.ValidateFirstName(FirstName);
                }

                if (propertiesName == nameof(LastName))
                {
                    return NewCustomerValidator.ValidateLastName(LastName);
                }

                if (propertiesName == nameof(Phone))
                {
                    return NewCustomerValidator.ValidatePhone(Phone);
                }
                return string.Empty;
            }
        }
        public string Error => string.Empty;
        protected override bool IsValid()
        {
            return string.IsNullOrEmpty(_validationMessage);
        }
        #endregion
    }
}
