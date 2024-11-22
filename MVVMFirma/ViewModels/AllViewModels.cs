using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMFirma.Helper;
using System.Windows.Input;
using MVVMFirma.Models.Entities;
using System.Collections.ObjectModel;

namespace MVVMFirma.ViewModels
{
    public abstract class AllViewModels<T> : WorkspaceViewModel
    {
        #region DB
        protected readonly PDABEntities pdabEntities;
        #endregion

        #region LoadCommand
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
            pdabEntities = new PDABEntities();
            base.DisplayName = displayName;
        }
        #endregion

        #region Helpers
        public abstract void Load();
        #endregion
    }
}
