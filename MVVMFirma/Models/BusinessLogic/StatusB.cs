using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMFirma.Models.Entities;
using MVVMFirma.Models.EntitiesForView;

namespace MVVMFirma.Models.BusinessLogic
{
    public class StatusB : DatabaseClass
    {
        #region Constructor
        public StatusB(PDABv2Entities db)
            : base(db)
        { }
        #endregion

        #region Business Functions
        public IQueryable<KeyAndValue> GetStatusKeyAndValueItems()
        {
            return
                (
                    from status in db.Status
                    select new KeyAndValue
                    {
                        Key = status.StatusID,
                        Value = status.Name
                    }
                ).ToList().AsQueryable();
        }
        #endregion
    }
}
