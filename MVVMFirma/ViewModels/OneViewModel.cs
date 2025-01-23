using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMFirma.Helper;
using System.Windows.Input;
using MVVMFirma.Models.Entities;

namespace MVVMFirma.ViewModels
{
    public abstract class OneViewModel<T> : WorkspaceViewModel
    {
        #region DB
        protected PDABv2Entities pdabEntities;
        #endregion

        #region Item
        protected T item;
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

        #region Konstruktor
        public OneViewModel(string displayName)
        {
            base.DisplayName = displayName;
            pdabEntities = new PDABv2Entities();
        }
        #endregion

        #region Helpers
        public abstract void Save();
        public void SaveAndClose()
        {
            if (IsValid())
            {
                Save();
                base.OnRequestClose();
            } else
            {
                ShowMessageBox("Edit data in the form.");
            }
            
        }

        protected virtual bool IsValid() => true;
        #endregion
    }
}
