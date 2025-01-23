using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using MVVMFirma.Helper;
using System.Text.RegularExpressions;
using MVVMFirma.Models.BusinessLogic;
using MVVMFirma.Models.Entities;
using MVVMFirma.Models.EntitiesForView;
using MVVMFirma.Validators;

namespace MVVMFirma.ViewModels
{
    public class NewProductViewModel : OneViewModel<Product>, IDataErrorInfo
    {
        #region Contructor
        public NewProductViewModel() 
            : base("Product") 
        { 
            item = new Product();
        }
        #endregion

        #region Fields
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

        public decimal? Price
        {
            get
            { return item.Price; }
            set
            {
                item.Price = value;
                OnPropertyChanged(() => Price);
            }
        }

        public decimal? Cost
        {
            get
            { return item.Cost; }
            set
            {
                item.Cost = value;
                OnPropertyChanged(() => Cost);
            }
        }

        public int? CategoryID
        {
            get
            { return item.CategoryID; }
            set
            {
                item.CategoryID = value;
                OnPropertyChanged(() => CategoryID);
            }
        }
        #endregion

        #region Properties for ComboBox
        public IQueryable<KeyAndValue> CategoriesItems
        {
            get
            {
                return new CategoriesB(pdabEntities).GetCategoriesKeyAndValueItems();
            }
        }
        #endregion

        #region Helpers
        public override void Save()
        {
            pdabEntities.Product.Add(item);
            pdabEntities.SaveChanges();
        }
        #endregion

        #region Validation
        private string _validationMessage = string.Empty;
        public string this[string propertiesName]
        {
            get
            {
                if (propertiesName == nameof(Name))
                {
                    return NewProductValidator.ValidateProductName(Name);
                }

                if (propertiesName == nameof(Price))
                {
                    return NewProductValidator.ValidateProductPrice(Price, Cost);
                }

                if (propertiesName == nameof(Cost))
                {
                    return NewProductValidator.ValidateProductCost(Cost);
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
