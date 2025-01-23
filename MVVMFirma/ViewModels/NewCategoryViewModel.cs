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
    public class NewCategoryViewModel : WorkspaceViewModel
    {
        #region DB
        private PDABv2Entities pdabEntities;
        #endregion

        #region Item
        private Categories category;
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
        public NewCategoryViewModel()
        {
            base.DisplayName = "New Category";
            pdabEntities = new PDABv2Entities();
            category = new Categories();
        }
        #endregion

        #region Properties

        public string Name
        {
            get { return category.Name; }
            set
            {
                category.Name = value;
                OnPropertyChanged(() => Name);
            }
        }

        public string Description
        {
            get { return category.Description; }
            set
            {
                category.Description = value;
                OnPropertyChanged(() => Description);
            }
        }
        #endregion

        #region Helpers
        public void Save()
        {
            pdabEntities.Categories.Add(category);
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
