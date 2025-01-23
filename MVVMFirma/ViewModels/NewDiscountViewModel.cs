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
    public class NewDiscountViewModel : WorkspaceViewModel
    {
        #region DB
        private PDABv2Entities pdabEntities;
        #endregion

        #region Item
        private Discounts discount;
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
        public NewDiscountViewModel()
        {
            base.DisplayName = "New Discount";
            pdabEntities = new PDABv2Entities();
            discount = new Discounts();
        }
        #endregion

        #region Properties

        public string Name
        {
            get { return discount.Name; }
            set
            {
                discount.Name = value;
                OnPropertyChanged(() => Name);
            }
        }

        public decimal? Percentage
        {
            get { return discount.Percentage; }
            set
            {
                discount.Percentage = value;
                OnPropertyChanged(() => Percentage);
            }
        }


        public DateTime? StartDate
        {
            get { return discount.StartDate; }
            set
            {
                discount.StartDate = value;
                OnPropertyChanged(() => StartDate);
            }
        }

        public DateTime? EndDate
        {
            get { return discount.EndDate; }
            set
            {
                discount.EndDate = value;
                OnPropertyChanged(() => EndDate);
            }
        }

        public bool? IsActive
        {
            get { return discount.IsActive; }
            set
            {
                discount.IsActive = value;
                OnPropertyChanged(() => IsActive);
            }
        }
        #endregion

        #region Helpers
        public void Save()
        {
            pdabEntities.Discounts.Add(discount);
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
