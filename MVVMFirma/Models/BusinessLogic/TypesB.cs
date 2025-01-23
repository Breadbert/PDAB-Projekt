using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMFirma.Models.Entities;
using MVVMFirma.Models.EntitiesForView;

namespace MVVMFirma.Models.BusinessLogic
{
    public class TypesB : DatabaseClass
    {
        #region Constructor
        public TypesB(PDABv2Entities db)
            : base(db)
        { }
        #endregion

        #region Business Functions
        public IQueryable<KeyAndValue> GetTypesKeyAndValueItems()
        {
            return
                (
                    from types in db.Type
                    select new KeyAndValue
                    {
                        Key = types.TypeID,
                        Value = types.Name
                    }
                ).ToList().AsQueryable();
        }
        #endregion
    }
}
