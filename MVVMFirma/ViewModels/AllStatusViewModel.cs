using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMFirma.Models.Entities;

namespace MVVMFirma.ViewModels
{
    public class AllStatusViewModel : AllViewModels<Status>
    {
        #region Constructor
        public AllStatusViewModel()
            : base("Statuses")
        {
        }
        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<Status>
                (
                    pdabEntities.Status.ToList()
                );
        }
        #endregion
    }
}
