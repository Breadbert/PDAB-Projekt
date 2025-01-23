using System;
using System.Collections.Generic;
using System.Linq;
using MVVMFirma.Models.Entities;

namespace MVVMFirma.Models.BusinessLogic
{
    public class StockB : DatabaseClass
    {
        #region Constructor
        public StockB(PDABv2Entities db) : base(db) { }
        #endregion

        #region Business Functions

        public IEnumerable<ProductStockInfo> GetProductsNotInStock()
        {
            return
            (
                from inventory in db.Inventory
                join product in db.Product
                on inventory.ProductID equals product.ProductID
                where inventory.Quantity == 0
                select new ProductStockInfo
                {
                    ProductID = product.ProductID,
                    CategoryName = product.Categories.Name,
                    ProductName = product.Name
                }
            ).ToList();
        }

        public IEnumerable<ProductStockInfo> GetProductsNotRestockedInAYear()
        {
            var oneYearAgo = DateTime.Now.AddYears(-1);
            return
            (
                from inventory in db.Inventory
                join product in db.Product
                on inventory.ProductID equals product.ProductID
                where inventory.RestockDate == null || inventory.RestockDate < oneYearAgo
                select new ProductStockInfo
                {
                    ProductID = product.ProductID,
                    CategoryName = product.Categories.Name,
                    ProductName = product.Name
                }
            ).ToList();
        }

        public decimal? GetReplenishmentCost()
        {
            return
            (
                from inventory in db.Inventory
                join product in db.Product
                on inventory.ProductID equals product.ProductID
                where inventory.Quantity < inventory.MinimumStockLevel
                select (inventory.MinimumStockLevel - inventory.Quantity) * product.Cost
            ).Sum();
        }

        public IEnumerable<ReplenishmentCostDetail> GetReplenishmentCostDetails()
        {
            return
            (
                from inventory in db.Inventory
                join product in db.Product on inventory.ProductID equals product.ProductID
                where inventory.Quantity < inventory.MinimumStockLevel
                select new ReplenishmentCostDetail
                {
                    ProductName = product.Name,
                    QuantityNeeded = inventory.MinimumStockLevel.Value - inventory.Quantity.Value,
                    Cost = (inventory.MinimumStockLevel.Value - inventory.Quantity.Value) * product.Cost.Value
                }
            ).ToList();
        }

        #endregion
    }

    public class ProductStockInfo
    {
        public int ProductID { get; set; }
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
    }

    public class ReplenishmentCostDetail
    {
        public string ProductName { get; set; }
        public int QuantityNeeded { get; set; }
        public decimal Cost { get; set; }
    }
}
