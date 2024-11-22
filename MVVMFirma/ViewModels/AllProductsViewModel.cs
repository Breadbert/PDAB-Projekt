using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using MVVMFirma.Models.Entities;
using MVVMFirma.Helper;
using System.Windows.Input;

namespace MVVMFirma.ViewModels
{
    public class AllProductsViewModel : AllViewModels<Products>
    {

        #region Constructor
        public AllProductsViewModel()
            : base("Products")
        {
        }
        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<Products>
                (
                    pdabEntities.Products.ToList()
                );
        }
        #endregion
    }
}