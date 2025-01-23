using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMFirma.Helper;
using MVVMFirma.Models.BusinessLogic;
using System.Windows.Input;
using MVVMFirma.Models.Entities;

namespace MVVMFirma.ViewModels
{
    public class SalesReportViewModel : WorkspaceViewModel
    {
        #region DB
        PDABv2Entities db;
        #endregion

        #region Constructor
        public SalesReportViewModel()
        {
            base.DisplayName = "Sales Report";
            db = new PDABv2Entities();

            DateFrom = DateTime.Now;
            DateTo = DateTime.Now;
            Revenue = 0;
        }
        #endregion

        #region Fields

        private DateTime _DateFrom;
        public DateTime DateFrom
        {
            get { return _DateFrom; }

            set
            {
                if (_DateFrom != value)
                {
                    _DateFrom = value;
                    OnPropertyChanged(() => DateFrom);
                }
            }
        }

        private DateTime _DateTo;
        public DateTime DateTo
        {
            get { return _DateTo; }

            set
            {
                if (_DateTo != value)
                {
                    _DateTo = value;
                    OnPropertyChanged(() => DateTo);
                }
            }
        }

        private decimal? _Revenue;
        public decimal? Revenue
        {
            get { return _Revenue; }

            set
            {
                if (_Revenue != value)
                {
                    _Revenue = value;
                    OnPropertyChanged(() => Revenue);
                }
            }
        }

        private decimal? _TotalCost;
        public decimal? TotalCost
        {
            get { return _TotalCost; }

            set
            {
                if (_TotalCost != value)
                {
                    _TotalCost = value;
                    OnPropertyChanged(() => TotalCost);
                }
            }
        }

        private decimal? _NETProfit;
        public decimal? NETProfit
        {
            get { return _NETProfit; }

            set
            {
                if (_NETProfit != value)
                {
                    _NETProfit = value;
                    OnPropertyChanged(() => NETProfit);
                }
            }
        }

        private decimal? _RevenueCD;
        public decimal? RevenueCD
        {
            get { return _RevenueCD; }

            set
            {
                if (_RevenueCD != value)
                {
                    _RevenueCD = value;
                    OnPropertyChanged(() => RevenueCD);
                }
            }
        }

        private decimal? _RevenueVinyl;
        public decimal? RevenueVinyl
        {
            get { return _RevenueVinyl; }

            set
            {
                if (_RevenueVinyl != value)
                {
                    _RevenueVinyl = value;
                    OnPropertyChanged(() => RevenueVinyl);
                }
            }
        }

        private decimal? _RevenueMC;
        public decimal? RevenueMC
        {
            get { return _RevenueMC; }

            set
            {
                if (_RevenueMC != value)
                {
                    _RevenueMC = value;
                    OnPropertyChanged(() => RevenueMC);
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
                    _CalculateCommand = new BaseCommand(() => AllCalculations());
                return _CalculateCommand;
            }
        }

        private void AllCalculations()
        {
            Revenue = new RevenueB(db).TotalRevenue(DateFrom, DateTo);
            TotalCost = new RevenueB(db).TotalInvoiceRevenue(DateFrom, DateTo);
            NETProfit = new RevenueB(db).CalculateNetProfit(DateFrom, DateTo); 
            RevenueCD = new RevenueB(db).TotalRevenueForCategoryCD(DateFrom, DateTo);
            RevenueVinyl = new RevenueB(db).TotalRevenueForCategoryVinyl(DateFrom, DateTo);
            RevenueMC = new RevenueB(db).TotalRevenueForCategoryMC(DateFrom, DateTo);
        }

        #endregion
    }
}
