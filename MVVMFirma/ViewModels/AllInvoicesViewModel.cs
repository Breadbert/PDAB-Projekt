using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMFirma.Models.Entities;
using System.Windows.Documents;
using MVVMFirma.Models.EntitiesForView;
using MVVMFirma.Helper;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;
using System.Windows;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace MVVMFirma.ViewModels
{
    public class AllInvoicesViewModel : AllViewModels<InvoiceForAllView>
    {

        #region Constructor
        public AllInvoicesViewModel()
            : base("Invoices")
        {
            ShowOnlyApprovedInvoices = false;
            ShowOverdueInvoices = false;
            SortByIssueDateDescending = false;
            SaveInvoiceAsPDFCommand = new BaseCommand(SaveSelectedInvoiceAsPDF);
        }
        #endregion

        #region Properties
        private InvoiceForAllView _ChosenInvoice;
        public InvoiceForAllView ChosenInvoice
        {
            get
            {
                return _ChosenInvoice;
            }
            set
            {
                _ChosenInvoice = value;
                Messenger.Default.Send(_ChosenInvoice);
                //OnRequestClose();
            }
        }
        #endregion

        #region Checkboxes

        private ObservableCollection<InvoiceForAllView> _FilteredList;
        public ObservableCollection<InvoiceForAllView> FilteredList
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

        private bool _ShowOnlyApprovedInvoices;
        public bool ShowOnlyApprovedInvoices
        {
            get { return _ShowOnlyApprovedInvoices; }
            set
            {
                _ShowOnlyApprovedInvoices = value;
                ApplyFilters();
            }
        }

        private bool _ShowOverdueInvoices;
        public bool ShowOverdueInvoices
        {
            get { return _ShowOverdueInvoices; }
            set
            {
                _ShowOverdueInvoices = value;
                ApplyFilters();
            }
        }

        private bool _SortByIssueDateDescending;
        public bool SortByIssueDateDescending
        {
            get { return _SortByIssueDateDescending; }
            set
            {
                _SortByIssueDateDescending = value;
                ApplyFilters();
            }
        }

        private void ApplyFilters()
        {
            var query = List.AsEnumerable();

            if (ShowOnlyApprovedInvoices)
                query = query.Where(item => (bool)item.IsApproved);

            if (ShowOverdueInvoices)
                query = query.Where(item => item.DueDate < DateTime.Now);

            if (SortByIssueDateDescending)
                query = query.OrderByDescending(item => item.IssueDate);

            FilteredList = new ObservableCollection<InvoiceForAllView>(query);
        }

        #endregion

        #region Save as PDF
        public ICommand SaveInvoiceAsPDFCommand { get; set; }

        private void SaveSelectedInvoiceAsPDF()
        {
            if (ChosenInvoice == null)
            {
                System.Windows.MessageBox.Show("Please select an invoice to export.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                var dialog = new Microsoft.Win32.SaveFileDialog
                {
                    FileName = $"Invoice_{ChosenInvoice.Number}",
                    DefaultExt = ".pdf",
                    Filter = "PDF documents (.pdf)|*.pdf",
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                };

                if (dialog.ShowDialog() == true)
                {
                    string filePath = dialog.FileName;

                    // Query all invoice positions for the selected invoice
                    var invoicePositions = pdabEntities.InvoicePosition
                        .Where(ip => ip.InvoiceID == ChosenInvoice.InvoiceID)
                        .Select(ip => new
                        {
                            ProductName = ip.Product.Name,
                            Price = ip.Price ?? 0, // Ensure a valid default value
                            Amount = ip.Amount ?? 0, // Ensure a valid default value
                            Total = (ip.Price ?? 0) * (ip.Amount ?? 0)
                        })
                        .ToList();

                    decimal grandTotal = invoicePositions.Sum(ip => ip.Total);

                    PdfDocument document = new PdfDocument();
                    document.Info.Title = $"Invoice {ChosenInvoice.Number}";

                    PdfPage page = document.AddPage();
                    XGraphics gfx = XGraphics.FromPdfPage(page);

                    // Fonts
                    XFont titleFont = new XFont("Arial", 18);
                    XFont headerFont = new XFont("Arial", 12);
                    XFont tableFont = new XFont("Arial", 10);

                    // Draw invoice header
                    int startX = 50;
                    int startY = 50;
                    int rowHeight = 20;

                    gfx.DrawString($"Invoice Number: {ChosenInvoice.Number}", titleFont, XBrushes.Black, startX, startY);
                    gfx.DrawString($"Is Approved: {((bool)ChosenInvoice.IsApproved ? "Yes" : "No")}", tableFont, XBrushes.Black, startX, startY + rowHeight);
                    gfx.DrawString($"Issue Date: {ChosenInvoice.IssueDate:yyyy-MM-dd}", tableFont, XBrushes.Black, startX, startY + rowHeight * 2);
                    gfx.DrawString($"Due Date: {ChosenInvoice.DueDate:yyyy-MM-dd}", tableFont, XBrushes.Black, startX, startY + rowHeight * 3);
                    gfx.DrawString($"Contractor: {ChosenInvoice.ContractorName} (NIP: {ChosenInvoice.ContractorNIP})", tableFont, XBrushes.Black, startX, startY + rowHeight * 4);
                    gfx.DrawString($"Payment Method: {ChosenInvoice.PaymentMethod}", tableFont, XBrushes.Black, startX, startY + rowHeight * 5);

                    startY += rowHeight * 7; // Adjust startY for table

                    // Draw table headers
                    gfx.DrawString("Product Name", headerFont, XBrushes.Black, startX, startY);
                    gfx.DrawString("Unit Price", headerFont, XBrushes.Black, startX + 200, startY);
                    gfx.DrawString("Amount", headerFont, XBrushes.Black, startX + 300, startY);
                    gfx.DrawString("Total", headerFont, XBrushes.Black, startX + 400, startY);

                    startY += rowHeight;

                    // Draw table rows
                    foreach (var position in invoicePositions)
                    {
                        gfx.DrawString(position.ProductName, tableFont, XBrushes.Black, startX, startY);
                        gfx.DrawString($"{position.Price:C}", tableFont, XBrushes.Black, startX + 200, startY);
                        gfx.DrawString($"{position.Amount}", tableFont, XBrushes.Black, startX + 300, startY);
                        gfx.DrawString($"{position.Total:C}", tableFont, XBrushes.Black, startX + 400, startY);

                        startY += rowHeight;

                        // Handle page overflow
                        if (startY > page.Height - 50)
                        {
                            page = document.AddPage();
                            gfx = XGraphics.FromPdfPage(page);
                            startY = 50;
                        }
                    }

                    // Draw grand total at the bottom
                    startY += rowHeight;
                    gfx.DrawString($"Grand Total: {grandTotal:C}", headerFont, XBrushes.Black, startX + 400, startY);

                    // Save the document
                    document.Save(filePath);

                    System.Windows.MessageBox.Show("PDF file saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"An error occurred while saving the PDF: {ex.Message}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<InvoiceForAllView>
                (
                    from invoice in pdabEntities.Invoice
                    select new InvoiceForAllView
                    {
                        InvoiceID = invoice.InvoiceID,
                        Number = invoice.Number,
                        IsApproved = invoice.IsApproved,
                        IssueDate = invoice.IssueDate,
                        DueDate = invoice.DueDate,
                        ContractorNIP = invoice.Contractor.NIP,
                        ContractorName = invoice.Contractor.Name,
                        PaymentMethod = invoice.PaymentMethod.Method,
                    }
                );
            ApplyFilters();
        }

        #region Sort and Find
        public override List<string> getComboBoxSortList()
        {
            return new List<string> { "IsApproved", "IssueDate", "DueDate", "ContractorName", "IsApproved and PaymentMethod" };
        }

        public override void Sort()
        {
            if (SortField == "IsApproved")
                List = new ObservableCollection<InvoiceForAllView>(List.OrderBy(item => item.IsApproved));
            if (SortField == "IssueDate")
                List = new ObservableCollection<InvoiceForAllView>(List.OrderBy(item => item.IssueDate));
            if (SortField == "DueDate")
                List = new ObservableCollection<InvoiceForAllView>(List.OrderBy(item => item.DueDate));
            if (SortField == "ContractorName")
                List = new ObservableCollection<InvoiceForAllView>(List.OrderBy(item => item.ContractorName));
            if (SortField == "IsApproved and PaymentMethod")
                List = new ObservableCollection<InvoiceForAllView>(List.OrderBy(item => item.IsApproved).OrderBy(item => item.PaymentMethod));
        }

        public override List<string> getComboBoxFindList()
        {
            return new List<string>() { "DueDate", "ContractorNIP", "ContractorName" };
        }

        public override void Find()
        {
            Load();
            if (FindField == "DueDate")
                List = new ObservableCollection<InvoiceForAllView>(List.Where(item => item.DueDate != null && item.DueDate.ToString().StartsWith(FindTextBox)));
            if (FindField == "ContractorNIP")
                List = new ObservableCollection<InvoiceForAllView>(List.Where(item => item.ContractorNIP != null && item.ContractorNIP.StartsWith(FindTextBox)));
            if (FindField == "ContractorName")
                List = new ObservableCollection<InvoiceForAllView>(List.Where(item => item.ContractorName != null && item.ContractorName.StartsWith(FindTextBox)));
        }
        #endregion
    }
    #endregion
}