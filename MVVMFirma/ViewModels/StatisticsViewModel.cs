using LiveCharts;
using LiveCharts.Wpf;
using MVVMFirma.Models.BusinessLogic;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using MVVMFirma.Helper;
using MVVMFirma.Models.Entities;

namespace MVVMFirma.ViewModels
{
    public class StatisticsViewModel : WorkspaceViewModel
    {
        #region DB
        private PDABv2Entities db;
        private StatisticsB _statisticsB;
        #endregion

        #region Constructor
        public StatisticsViewModel()
        {
            base.DisplayName = "Statistics";
            db = new PDABv2Entities();
            _statisticsB = new StatisticsB(db);

            // Initialize default values
            SelectedYear = System.DateTime.Now.Year; 
            Years = Enumerable.Range(2020, 6).ToList(); 
            SeriesCollection = new SeriesCollection();
            Labels = new string[12]; // Placeholder for months

            // Initialize commands
            LoadStatisticsCommand = new BaseCommand(() => LoadStatistics());
        }
        #endregion

        #region Fields

        public List<int> Years { get; set; } 

        private int _SelectedYear;
        public int SelectedYear
        {
            get { return _SelectedYear; }
            set
            {
                if (_SelectedYear != value)
                {
                    _SelectedYear = value;
                    OnPropertyChanged(() => SelectedYear);
                }
            }
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }

        #endregion

        #region Commands

        public ICommand LoadStatisticsCommand { get; set; }

        private void LoadStatistics()
        {
            var data = _statisticsB.GetMonthlySalesAndCosts(SelectedYear);

            var salesValues = data.Select(stat => (double)(stat.Sales ?? 0)).ToArray(); // Preparing the data for the chart here, respectively for both bars
            var costsValues = data.Select(stat => (double)(stat.Costs ?? 0)).ToArray();
            Labels = data.Select(stat => stat.Month.ToString()).ToArray();

            SeriesCollection.Clear();
            SeriesCollection.Add(new ColumnSeries // Updating the series collection with the new Data
            {
                Title = "Sales",
                Values = new ChartValues<double>(salesValues)
            });
            SeriesCollection.Add(new ColumnSeries
            {
                Title = "Costs",
                Values = new ChartValues<double>(costsValues)
            });

            OnPropertyChanged(() => SeriesCollection); // Notifying changes for both the series and the labels
            OnPropertyChanged(() => Labels);
        }

        #endregion
    }
}
