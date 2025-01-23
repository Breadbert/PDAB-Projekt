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
    public class AllInventoryViewModel : AllViewModels<InventoryForAllView>
    {
        #region Constructor
        public AllInventoryViewModel()
            : base("Inventory")
        {
        }
        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<InventoryForAllView>
                (
                    from inventory in pdabEntities.Inventory
                    select new InventoryForAllView
                    {
                        ProductID = inventory.Product.ProductID,
                        ProductName = inventory.Product.Name,
                        Quantity = inventory.Quantity,
                        MinimumStockLevel = inventory.MinimumStockLevel,
                        RestockDate = inventory.RestockDate
                    }
                );
        }
        #endregion

        #region Sort and Find
        public override List<string> getComboBoxSortList()
        {
            return new List<string> { "Name", "RestockDate", "Name and RestockDate" };
        }

        public override void Sort()
        {
            if (SortField == "Name")
                List = new ObservableCollection<InventoryForAllView>(List.OrderBy(item => item.ProductName));
            if (SortField == "RestockDate")
                List = new ObservableCollection<InventoryForAllView>(List.OrderBy(item => item.RestockDate));
            if (SortField == "Name and RestockDate")
                List = new ObservableCollection<InventoryForAllView>(List.OrderBy(item => item.ProductName).OrderBy(item => item.RestockDate));
        }

        public override List<string> getComboBoxFindList()
        {
            return new List<string>() { "Name", "RestockDate" };
        }

        public override void Find()
        {
            Load();
            if (FindField == "Name")
                List = new ObservableCollection<InventoryForAllView>(List.Where(item => item.ProductName != null && item.ProductName.StartsWith(FindTextBox)));
            if (FindField == "RestockDate")
                List = new ObservableCollection<InventoryForAllView>(List.Where(item => item.RestockDate != null && item.RestockDate.ToString().Contains(FindTextBox)));
        }
        #endregion
    }
}
