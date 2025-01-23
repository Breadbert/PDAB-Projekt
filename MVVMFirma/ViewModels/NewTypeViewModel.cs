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
    public class NewTypeViewModel : WorkspaceViewModel
    {
        #region DB
        private PDABv2Entities pdabEntities;
        #endregion

        #region Item
        private Models.Entities.Type contractorType;
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
        public NewTypeViewModel()
        {
            base.DisplayName = "New Type";
            pdabEntities = new PDABv2Entities();
            contractorType = new MVVMFirma.Models.Entities.Type();
        }
        #endregion

        #region Properties

        public string Name
        {
            get { return contractorType.Name; }
            set
            {
                contractorType.Name = value;
                OnPropertyChanged(() => Name);
            }
        }

        public string Description
        {
            get { return contractorType.Description; }
            set
            {
                contractorType.Description = value;
                OnPropertyChanged(() => Description);
            }
        }
        #endregion

        #region Helpers
        public void Save()
        {
            pdabEntities.Type.Add(contractorType);
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
