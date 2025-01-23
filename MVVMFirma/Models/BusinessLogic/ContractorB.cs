using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMFirma.Models.Entities;
using MVVMFirma.Models.EntitiesForView;

namespace MVVMFirma.Models.BusinessLogic
{
    public class ContractorB : DatabaseClass
    {
        #region Konstruktor
        public ContractorB(PDABv2Entities db)
            : base(db) { }
        #endregion
        #region Business Functions
        public IQueryable<KeyAndValue> GetKontrahenciKeyAndValueItems()
        {
            return
                (
                    from contractor in db.Contractor
                    select new KeyAndValue
                    {
                        Key = contractor.ContractorID,
                        Value = contractor.Name + " " + contractor.NIP
                    }
                ).ToList().AsQueryable();
        }
        #endregion
    }
}
