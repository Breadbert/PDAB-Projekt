using System.Collections.ObjectModel;
using System.Linq;
using MVVMFirma.Models.Entities;
using MVVMFirma.Helper;
using MVVMFirma.Models.EntitiesForView;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows;
using System;
using System.IO;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using GalaSoft.MvvmLight.Messaging;

namespace MVVMFirma.ViewModels
{
    public class AllProductsViewModel : AllViewModels<ProductsForAllView>
    {
        #region Constructor
        public AllProductsViewModel() : base("Products")
        {
            SaveAsPDFCommand = new BaseCommand(SaveTableAsPDF);
            ShowExpensiveProducts = false;
            ShowHighProfitProducts = false;
            GroupByCategory = false;
        }
        #endregion

        #region Properties
        private ProductsForAllView _ChosenProduct;
        public ProductsForAllView ChosenProduct
        {
            get
            {
                return _ChosenProduct;
            }
            set
            {
                _ChosenProduct = value;
                Messenger.Default.Send(_ChosenProduct);
                OnRequestClose();
            }
        }
        #endregion

        #region Commands

        public ICommand SaveAsPDFCommand { get; set; }

        private void SaveTableAsPDF()
        {
            try
            {
                var dialog = new Microsoft.Win32.SaveFileDialog
                {
                    FileName = "ProductsTable",
                    DefaultExt = ".pdf",
                    Filter = "PDF documents (.pdf)|*.pdf",
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                };

                if (dialog.ShowDialog() == true)
                {
                    string filePath = dialog.FileName;

                    PdfDocument document = new PdfDocument();
                    document.Info.Title = "Products Table";

                    PdfPage page = document.AddPage();
                    XGraphics gfx = XGraphics.FromPdfPage(page);

                    // === FONTS ===
                    XFont titleFont = new XFont("Arial", 18);
                    XFont headerFont = new XFont("Arial", 12);
                    XFont tableFont = new XFont("Arial", 10);

                    // === TITLE ===
                    gfx.DrawString("Products Table", titleFont, XBrushes.Black, new XRect(0, 20, page.Width, 50), XStringFormats.TopCenter);

                    // === COLUMN SPACING ===
                    int startX = 50;
                    int startY = 100;
                    int rowHeight = 20;

                    int col1X = startX;         // Product ID
                    int col2X = startX + 100;   // Product Name 
                    int col3X = startX + 300;   // Price 
                    int col4X = startX + 380;   // Cost
                    int col5X = startX + 460;   // Category

                    // === COLUMN HEADERS ===
                    gfx.DrawString("Product ID", headerFont, XBrushes.Black, col1X, startY);
                    gfx.DrawString("Product Name", headerFont, XBrushes.Black, col2X, startY);
                    gfx.DrawString("Price", headerFont, XBrushes.Black, col3X, startY);
                    gfx.DrawString("Cost", headerFont, XBrushes.Black, col4X, startY);
                    gfx.DrawString("Category", headerFont, XBrushes.Black, col5X, startY);

                    startY += rowHeight;

                    // === TABLE ROWS ===
                    foreach (var product in List)
                    {
                        gfx.DrawString(product.ProductID.ToString() ?? "N/A", tableFont, XBrushes.Black, col1X, startY);
                        gfx.DrawString(product.Name ?? "N/A", tableFont, XBrushes.Black, col2X, startY);
                        gfx.DrawString(product.Price.HasValue ? product.Price.Value.ToString("C") : "N/A", tableFont, XBrushes.Black, col3X, startY);
                        gfx.DrawString(product.Cost.HasValue ? product.Cost.Value.ToString("C") : "N/A", tableFont, XBrushes.Black, col4X, startY);
                        gfx.DrawString(product.Category ?? "N/A", tableFont, XBrushes.Black, col5X, startY);

                        startY += rowHeight;

                        // === IF PAGE OVERFLOWS ===
                        if (startY > page.Height - XUnit.FromPoint(50))
                        {
                            page = document.AddPage();
                            gfx = XGraphics.FromPdfPage(page);
                            startY = 50;

                            // === REDRAW HEADERS ON NEW PAGE ===
                            gfx.DrawString("Product ID", headerFont, XBrushes.Black, col1X, startY);
                            gfx.DrawString("Product Name", headerFont, XBrushes.Black, col2X, startY);
                            gfx.DrawString("Price", headerFont, XBrushes.Black, col3X, startY);
                            gfx.DrawString("Cost", headerFont, XBrushes.Black, col4X, startY);
                            gfx.DrawString("Category", headerFont, XBrushes.Black, col5X, startY);

                            startY += rowHeight;
                        }
                    }

                    // === SAVE PDF ===
                    document.Save(filePath);

                    System.Windows.MessageBox.Show("PDF file saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"An error occurred while saving the PDF: {ex.Message}",
                    "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private bool _ShowExpensiveProducts;
        public bool ShowExpensiveProducts
        {
            get { return _ShowExpensiveProducts; }
            set
            {
                _ShowExpensiveProducts = value;
                FilterProducts();
                OnPropertyChanged(() => ShowExpensiveProducts);
            }
        }

        private bool _ShowHighProfitProducts;
        public bool ShowHighProfitProducts
        {
            get { return _ShowHighProfitProducts; }
            set
            {
                _ShowHighProfitProducts = value;
                FilterProducts();
                OnPropertyChanged(() => ShowHighProfitProducts);
            }
        }

        private bool _GroupByCategory;
        public bool GroupByCategory
        {
            get { return _GroupByCategory; }
            set
            {
                _GroupByCategory = value;
                FilterProducts();
                OnPropertyChanged(() => GroupByCategory);
            }
        }

        private ObservableCollection<ProductsForAllView> _FilteredProducts;
        public ObservableCollection<ProductsForAllView> FilteredProducts
        {
            get { return _FilteredProducts; }
            set
            {
                _FilteredProducts = value;
                OnPropertyChanged(() => FilteredProducts);
            }
        }

        #endregion

        #region Helpers

        public override void Load()
        {
            List = new ObservableCollection<ProductsForAllView>
                (
                    from product in pdabEntities.Product
                    select new ProductsForAllView
                    {
                        ProductID = product.ProductID,
                        Name = product.Name,
                        Price = product.Price,
                        Cost = product.Cost,
                        Category = product.Categories.Name
                    }
                );

            FilterProducts(); 
        }

        private void FilterProducts()
        {
            var filtered = List.AsQueryable();

            if (ShowExpensiveProducts)
            {
                filtered = filtered.Where(p => p.Price >= 150);
            }

            if (ShowHighProfitProducts)
            {
                filtered = filtered.Where(p => (p.Price - p.Cost) >= 30);
            }

            if (GroupByCategory)
            {
                filtered = filtered.OrderBy(p => p.Category);
            }

            FilteredProducts = new ObservableCollection<ProductsForAllView>(filtered);
        }

        #endregion

        #region Sort and Find
        public override List<string> getComboBoxSortList()
        {
            return new List<string> { "Name", "Price", "Cost", "Category" };
        }

        public override void Sort()
        {
            if (SortField == "Name")
                List = new ObservableCollection<ProductsForAllView>(List.OrderBy(item => item.Name));
            if (SortField == "Price")
                List = new ObservableCollection<ProductsForAllView>(List.OrderBy(item => item.Price));
            if (SortField == "Cost")
                List = new ObservableCollection<ProductsForAllView>(List.OrderBy(item => item.Cost));
            if (SortField == "Category")
                List = new ObservableCollection<ProductsForAllView>(List.OrderBy(item => item.Category));
        }

        public override List<string> getComboBoxFindList()
        {
            return new List<string>() { "Name", "OrderDate", "ShippedDate" };
        }

        public override void Find()
        {
            Load();
            if (FindField == "Name")
                List = new ObservableCollection<ProductsForAllView>(List.Where(item => item.Name != null && item.Name.StartsWith(FindTextBox)));
        }
        #endregion
    }
}
