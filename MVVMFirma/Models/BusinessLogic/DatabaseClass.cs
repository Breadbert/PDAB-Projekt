using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMFirma.Models.Entities;
using MVVMFirma.Models.EntitiesForView;

namespace MVVMFirma.Models.BusinessLogic
{
    public class DatabaseClass
    {
        #region Context
        public PDABv2Entities db { get; set; }
        #endregion

        #region Contructor
        public DatabaseClass(PDABv2Entities db)
        {
            this.db = db; 
        }
        #endregion
    }
}
