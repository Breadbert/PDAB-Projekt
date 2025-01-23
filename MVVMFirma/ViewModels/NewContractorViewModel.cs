using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using MVVMFirma.Helper;
using System.Windows.Input;
using MVVMFirma.Models.BusinessLogic;
using MVVMFirma.Models.Entities;
using MVVMFirma.Models.EntitiesForView;
using MVVMFirma.Validators;
using System.Diagnostics;
using System.ComponentModel;

namespace MVVMFirma.ViewModels
{
    public class NewContractorViewModel : OneViewModel<Contractor>, IDataErrorInfo
    {
        #region Constructor
        public NewContractorViewModel()
            : base("Contractor")
        {
            item = new Contractor();
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
        public int? AddressID
        {
            get
            {
                return item.AddressID;
            }
            set
            {
                item.AddressID = value;
                OnPropertyChanged(() => AddressID);
            }
        }
        public int? StatusID
        {
            get
            {
                return item.StatusID;
            }
            set
            {
                item.StatusID = value;
                OnPropertyChanged(() => StatusID);
            }
        }
        public int? TypeID
        {
            get
            {
                return item.TypeID;
            }
            set
            {
                item.TypeID = value;
                OnPropertyChanged(() => TypeID);
            }
        }
        public string City { get; set; }
        public string Country { get; set; }
        public string Code
        {
            get
            {
                return item.Code;
            }
            set
            {
                item.Code = value;
                OnPropertyChanged(() => Code);
            }
        }
        public string NIP
        {
            get
            {
                return item.NIP;
            }
            set
            {
                item.NIP = value;
                OnPropertyChanged(() => NIP);
            }
        }

        public string Name
        {
            get
            {
                return item.Name;
            }
            set
            {
                item.Name = value;
                OnPropertyChanged(() => Name);
            }
        }
        #endregion

        #region Properties for ComboBox
        public IQueryable<KeyAndValue> TypesItems
        {
            get
            {
                return new TypesB(pdabEntities).GetTypesKeyAndValueItems();
            }
        }

        public IQueryable<KeyAndValue> StatusItems
        {
            get
            {
                return new StatusB(pdabEntities).GetStatusKeyAndValueItems();
            }
        }
        #endregion

        #region Helpers
        private void getChosenAddress(AddressForAllView address)
        {
            AddressID = address.AddressID;
            City = address.City;
            Country = address.Country;
        }
        public override void Save()
        {
            pdabEntities.Contractor.Add(item);
            pdabEntities.SaveChanges();
        }
        #endregion

        #region Validation
        private string _validationMessage = string.Empty;
        public string this[string propertiesName]
        {
            get
            {
                if (propertiesName == nameof(Code))
                {
                    return NewContractorValidator.ValidateContractorCode(Code);
                }

                if (propertiesName == nameof(Name))
                {
                    return NewContractorValidator.ValidateContractorName(Name);
                }

                if (propertiesName == nameof(NIP))
                {
                    return NewContractorValidator.ValidateContractorNIP(NIP);
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
