using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMFirma.Models.Entities;
using MVVMFirma.Models.EntitiesForView;
using System.Windows.Documents;

namespace MVVMFirma.ViewModels
{
    public class AllCategoriesViewModel : AllViewModels<Categories>
    {
        #region Constructor
        public AllCategoriesViewModel()
            : base("Categories")
        {
        }
        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<Categories>
                (
                    pdabEntities.Categories.ToList()
                );
        }
        #endregion
    }
}
