using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMFirma.Helper;
using System.Windows.Input;
using MVVMFirma.Models.Entities;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Messaging;
using MVVMFirma.Models.EntitiesForView;

namespace MVVMFirma.ViewModels
{
    public abstract class AllViewModels<T> : WorkspaceViewModel
    {
        #region DB
        protected readonly PDABv2Entities pdabEntities;
        #endregion

        #region Command
        private BaseCommand _LoadCommand;
        public ICommand LoadCommand
        {
            get
            {
                if (_LoadCommand == null)
                    _LoadCommand = new BaseCommand(() => Load());
                return _LoadCommand;
            }
        }

        private BaseCommand _AddCommand;
        public ICommand AddCommand
        {
            get
            {
                if (_AddCommand == null)
                    _AddCommand = new BaseCommand(() => Add());
                return _AddCommand;
            }
        }
        #endregion

        #region List
        private ObservableCollection<T> _List;

        public ObservableCollection<T> List
        {
            get
            {
                if (_List == null)
                    Load();
                return _List;
            }
            set
            {
                _List = value;
                OnPropertyChanged(() => List);
            }
        }
        #endregion

        #region Constructor
        public AllViewModels(String displayName)
        {
            pdabEntities = new PDABv2Entities();
            base.DisplayName = displayName;
        }
        #endregion

        #region Sort and Filter
        // === SORT ==
        public string SortField { get; set; }
        private BaseCommand _SortCommand;
        public List<string> SortComboBoxItems 
        {
            get
            {
                return getComboBoxSortList();
            }
        }
        public virtual List<string> getComboBoxSortList()
        {
            return new List<string>();
        }

        public ICommand SortCommand
        {
            get
            {
                if (_SortCommand == null)
                    _SortCommand = new BaseCommand(() => Sort());
                return _SortCommand;
            }
        }

        public virtual void Sort()
        {
            if (SortField == "")
                List = new ObservableCollection<T>(List.OrderBy(item => item));
        }

        // === FIND ===
        public string FindField { get; set; }
        public string FindTextBox { get; set; }
        private BaseCommand _FindCommand;
        public List<string> FindComboBoxItems
        {
            get
            {
                return getComboBoxFindList();
            }
        }
        public virtual List<string> getComboBoxFindList()
        {
            return new List<string>();
        }

        public ICommand FindCommand
        {
            get
            {
                if (_FindCommand == null)
                    _FindCommand = new BaseCommand(() => Find());
                return _FindCommand;
            }
        }

        public virtual void Find()
        {
            if (FindField == "Name")
                List = new ObservableCollection<T>();
        }
        #endregion

        #region Helpers
        public abstract void Load();
        private void Add()
        {
            Messenger.Default.Send(DisplayName + "Add");
        }
        #endregion
    }
}
