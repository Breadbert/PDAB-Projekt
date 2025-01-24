using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMFirma.Models.Entities;
using MVVMFirma.Models.EntitiesForView;
using System.Windows.Documents;
using MVVMFirma.Helper;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Windows.Input;
using System.Windows;

namespace MVVMFirma.ViewModels
{
    public class AllInventoryViewModel : AllViewModels<InventoryForAllView>
    {
        #region Constructor
        public AllInventoryViewModel()
            : base("Inventory")
        {
            HideZeroMinimumStock = false;
            ShowOnlyLowStock = false;
            SortByQuantityDescending = false;
            SaveAsPDFCommand = new BaseCommand(SaveTableAsPDF);
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

            ApplyFilters(); // Apply filters after loading
        }

        private void ApplyFilters()
        {
            var query = List.AsEnumerable();

            if (HideZeroMinimumStock)
                query = query.Where(item => item.MinimumStockLevel > 0);

            if (ShowOnlyLowStock)
                query = query.Where(item => item.Quantity < item.MinimumStockLevel);

            if (SortByQuantityDescending)
                query = query.OrderByDescending(item => item.Quantity);

            FilteredList = new ObservableCollection<InventoryForAllView>(query);
        }
        #endregion

        #region Save as PDF
        public ICommand SaveAsPDFCommand { get; set; }

        private void SaveTableAsPDF()
        {
            try
            {
                var dialog = new Microsoft.Win32.SaveFileDialog
                {
                    FileName = "InventoryTable",
                    DefaultExt = ".pdf",
                    Filter = "PDF documents (.pdf)|*.pdf",
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                };

                if (dialog.ShowDialog() == true)
                {
                    string filePath = dialog.FileName;

                    PdfDocument document = new PdfDocument();
                    document.Info.Title = "Inventory Table";

                    PdfPage page = document.AddPage();
                    XGraphics gfx = XGraphics.FromPdfPage(page);

                    // === FONTS ===
                    XFont titleFont = new XFont("Arial", 18);
                    XFont tableFont = new XFont("Arial", 10);

                    // === TITLE ===
                    gfx.DrawString("Inventory Table", titleFont, XBrushes.Black, new XRect(0, 20, page.Width, 50), XStringFormats.TopCenter);

                    // === COLUMN SPACING ===
                    int startX = 50;
                    int startY = 100;
                    int rowHeight = 20;

                    gfx.DrawString("Product ID", tableFont, XBrushes.Black, startX, startY);          // ProductID
                    gfx.DrawString("Product Name", tableFont, XBrushes.Black, startX + 50, startY);  // ProductName
                    gfx.DrawString("Quantity", tableFont, XBrushes.Black, startX + 250, startY);      // Quantity
                    gfx.DrawString("Minimum Stock", tableFont, XBrushes.Black, startX + 300, startY); // MinimumStockLevel
                    gfx.DrawString("Restock Date", tableFont, XBrushes.Black, startX + 400, startY);  // RestockDate

                    startY += rowHeight;

                    // === TABLE ROWS ===
                    foreach (var item in FilteredList)
                    {
                        gfx.DrawString(item.ProductID.ToString(), tableFont, XBrushes.Black, startX, startY);
                        gfx.DrawString(item.ProductName, tableFont, XBrushes.Black, startX + 50, startY);
                        gfx.DrawString(item.Quantity.ToString(), tableFont, XBrushes.Black, startX + 250, startY);
                        gfx.DrawString(item.MinimumStockLevel.ToString(), tableFont, XBrushes.Black, startX + 300, startY);
                        gfx.DrawString(item.RestockDate?.ToShortDateString() ?? "N/A", tableFont, XBrushes.Black, startX + 400, startY);

                        startY += rowHeight;

                        if (startY > page.Height - 50)
                        {
                            page = document.AddPage();
                            gfx = XGraphics.FromPdfPage(page);
                            startY = 50;
                        }
                    }

                    document.Save(filePath);

                    System.Windows.MessageBox.Show("PDF saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error saving PDF: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region Fields

        private ObservableCollection<InventoryForAllView> _FilteredList;
        public ObservableCollection<InventoryForAllView> FilteredList
        {
            get { return _FilteredList; }
            set
            {
                if (_FilteredList != value)
                {
                    _FilteredList = value;
                    OnPropertyChanged(() => FilteredList);
                }
            }
        }

        private bool _HideZeroMinimumStock;
        public bool HideZeroMinimumStock
        {
            get { return _HideZeroMinimumStock; }
            set
            {
                _HideZeroMinimumStock = value;
                ApplyFilters();
            }
        }

        private bool _ShowOnlyLowStock;
        public bool ShowOnlyLowStock
        {
            get { return _ShowOnlyLowStock; }
            set
            {
                _ShowOnlyLowStock = value;
                ApplyFilters();
            }
        }

        private bool _SortByQuantityDescending;
        public bool SortByQuantityDescending
        {
            get { return _SortByQuantityDescending; }
            set
            {
                _SortByQuantityDescending = value;
                ApplyFilters();
            }
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
