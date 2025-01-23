using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMFirma.Models.Entities;
using System.Windows.Documents;

namespace MVVMFirma.ViewModels
{
    internal class AllTypesViewModel : AllViewModels<Models.Entities.Type>
    {
        #region Constructor
        public AllTypesViewModel()
            : base("Types")
        {
        }
        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<Models.Entities.Type>
                (
                    pdabEntities.Type.ToList()
                );
        }
        #endregion
    }
}
