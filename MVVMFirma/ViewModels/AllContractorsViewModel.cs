using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMFirma.Helper;
using System.Windows.Input;
using MVVMFirma.Models.EntitiesForView;
using MVVMFirma.Models.Entities;
using GalaSoft.MvvmLight.Messaging;

namespace MVVMFirma.ViewModels
{
    public class AllContractorsViewModel : AllViewModels<ContractorsForAllView>
    {
        #region Constructor
        public AllContractorsViewModel()
            : base("Contractors")
        {
        }
        #endregion

        #region Properties
        private ContractorsForAllView _ChosenContractor;
        public ContractorsForAllView ChosenContractor
        {
            get 
            { 
                return _ChosenContractor; 
            } 
            set { 
                _ChosenContractor = value;
                Messenger.Default.Send(_ChosenContractor);
                OnRequestClose();
            }
        }
        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<ContractorsForAllView>
                (
                    from contractor in pdabEntities.Contractor
                    select new ContractorsForAllView
                    {
                        ContractorID = contractor.ContractorID,
                        Code = contractor.Code,
                        NIP = contractor.NIP,
                        Name = contractor.Name,
                        Type = contractor.Type.Name,
                        Status = contractor.Status.Name,
                        Country = contractor.Address.Country,
                        City = contractor.Address.City,
                        Street = contractor.Address.Street,
                        PostalCode = contractor.Address.PostalCode,
                        HouseNumber = contractor.Address.HouseNumber,
                        ApartmentNumber = contractor.Address.ApartmentNumber
                    }
                );
        }
        #endregion

        #region Sort and Find
        public override List<string> getComboBoxSortList()
        {
            return new List<string> { "Code", "NIP", "Name", "Name and Type" };
        }

        public override void Sort()
        {
            if (SortField == "Code")
                List = new ObservableCollection<ContractorsForAllView>(List.OrderBy(item => item.Code));
            if (SortField == "NIP")
                List = new ObservableCollection<ContractorsForAllView>(List.OrderBy(item => item.NIP));
            if (SortField == "Name")
                List = new ObservableCollection<ContractorsForAllView>(List.OrderBy(item => item.Name));
            if (SortField == "Type")
                List = new ObservableCollection<ContractorsForAllView>(List.OrderBy(item => item.Type));
            if (SortField == "Name and Type")
                List = new ObservableCollection<ContractorsForAllView>(List.OrderBy(item => item.Name).OrderBy(item => item.Type));
        }

        public override List<string> getComboBoxFindList()
        {
            return new List<string>() { "Code", "NIP", "Name" };
        }

        public override void Find()
        {
            Load();
            if (FindField == "Name")
                List = new ObservableCollection<ContractorsForAllView>(List.Where(item => item.Name != null && item.Name.StartsWith(FindTextBox)));
            if (FindField == "NIP")
                List = new ObservableCollection<ContractorsForAllView>(List.Where(item => item.NIP != null && item.NIP.StartsWith(FindTextBox)));
            if (FindField == "Code")
                List = new ObservableCollection<ContractorsForAllView>(List.Where(item => item.Code != null && item.Code.StartsWith(FindTextBox)));
        }
        #endregion
    }
}
