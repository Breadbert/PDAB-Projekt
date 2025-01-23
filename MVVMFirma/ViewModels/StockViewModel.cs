using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using MVVMFirma.Helper;
using MVVMFirma.Models.BusinessLogic;
using MVVMFirma.Models.Entities;

namespace MVVMFirma.ViewModels
{
    public class StockViewModel : WorkspaceViewModel
    {
        #region DB
        private PDABv2Entities db;
        private StockB _stockB;
        #endregion

        #region Constructor
        public StockViewModel()
        {
            base.DisplayName = "Stock Management";
            db = new PDABv2Entities();
            _stockB = new StockB(db);

            ProductsNotInStock = new List<ProductStockInfo>();
            ProductsNotRestockedInAYear = new List<ProductStockInfo>();
            ReplenishmentCostDetails = new List<ReplenishmentCostDetail>();
            ReplenishmentCost = 0;
        }
        #endregion

        #region Fields

        private IEnumerable<ProductStockInfo> _ProductsNotInStock;
        public IEnumerable<ProductStockInfo> ProductsNotInStock
        {
            get { return _ProductsNotInStock; }
            set
            {
                if (_ProductsNotInStock != value)
                {
                    _ProductsNotInStock = value;
                    OnPropertyChanged(() => ProductsNotInStock);
                }
            }
        }

        private IEnumerable<ProductStockInfo> _ProductsNotRestockedInAYear;
        public IEnumerable<ProductStockInfo> ProductsNotRestockedInAYear
        {
            get { return _ProductsNotRestockedInAYear; }
            set
            {
                if (_ProductsNotRestockedInAYear != value)
                {
                    _ProductsNotRestockedInAYear = value;
                    OnPropertyChanged(() => ProductsNotRestockedInAYear);
                }
            }
        }

        private IEnumerable<ReplenishmentCostDetail> _ReplenishmentCostDetails;
        public IEnumerable<ReplenishmentCostDetail> ReplenishmentCostDetails
        {
            get { return _ReplenishmentCostDetails; }
            set
            {
                if (_ReplenishmentCostDetails != value)
                {
                    _ReplenishmentCostDetails = value;
                    OnPropertyChanged(() => ReplenishmentCostDetails);
                }
            }
        }

        private decimal? _ReplenishmentCost;
        public decimal? ReplenishmentCost
        {
            get { return _ReplenishmentCost; }
            set
            {
                if (_ReplenishmentCost != value)
                {
                    _ReplenishmentCost = value;
                    OnPropertyChanged(() => ReplenishmentCost);
                }
            }
        }

        #endregion

        #region Commands

        private BaseCommand _CalculateCommand;
        public ICommand CalculateCommand
        {
            get
            {
                if (_CalculateCommand == null)
                    _CalculateCommand = new BaseCommand(() => PerformCalculations());
                return _CalculateCommand;
            }
        }

        private void PerformCalculations()
        {
            ProductsNotInStock = _stockB.GetProductsNotInStock();
            ProductsNotRestockedInAYear = _stockB.GetProductsNotRestockedInAYear();

            var detailsFromBusinessLogic = _stockB.GetReplenishmentCostDetails();
            ReplenishmentCostDetails = detailsFromBusinessLogic.Select(detail => new ReplenishmentCostDetail
            {
                ProductName = detail.ProductName,
                QuantityNeeded = detail.QuantityNeeded,
                Cost = detail.Cost
            }).ToList();

            ReplenishmentCost = ReplenishmentCostDetails.Sum(detail => detail.Cost);

            OnPropertyChanged(() => ProductsNotInStock);
            OnPropertyChanged(() => ProductsNotRestockedInAYear);
            OnPropertyChanged(() => ReplenishmentCostDetails);
            OnPropertyChanged(() => ReplenishmentCost);
        }

        #endregion
    }
}
