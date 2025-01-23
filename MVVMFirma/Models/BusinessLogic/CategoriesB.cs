using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMFirma.Models.Entities;
using MVVMFirma.Models.EntitiesForView;

namespace MVVMFirma.Models.BusinessLogic
{
    public class CategoriesB : DatabaseClass
    {
        #region Constructor
        public CategoriesB(PDABv2Entities db)
            :base(db)
        {}
        #endregion

        #region Business Functions
        public IQueryable<KeyAndValue> GetCategoriesKeyAndValueItems()
        {
            return
                (
                    from category in db.Categories
                    select new KeyAndValue
                    {
                        Key = category.CategoryID,
                        Value = category.Name
                    }
                ).ToList().AsQueryable();
        }
        #endregion
    }
}
