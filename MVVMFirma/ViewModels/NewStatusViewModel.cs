using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMFirma.Helper;
using MVVMFirma.Models.Entities;
using System.Windows.Input;

namespace MVVMFirma.ViewModels
{
    public class NewStatusViewModel : WorkspaceViewModel
    {
        #region DB
        private PDABv2Entities pdabEntities;
        #endregion

        #region Item
        private Status status;
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
        public NewStatusViewModel()
        {
            base.DisplayName = "New Status";
            pdabEntities = new PDABv2Entities();
            status = new Status();
        }
        #endregion

        #region Properties

        public string Method
        {
            get { return status.Name; }
            set
            {
                status.Name = value;
                OnPropertyChanged(() => Method);
            }
        }

        public string Description
        {
            get { return status.Description; }
            set
            {
                status.Description = value;
                OnPropertyChanged(() => Description);
            }
        }
        #endregion

        #region Helpers
        public void Save()
        {
            pdabEntities.Status.Add(status);
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
