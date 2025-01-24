using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using MVVMFirma.Helper;
using System.Diagnostics;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Data;
using GalaSoft.MvvmLight.Messaging;
using MVVMFirma.Models.EntitiesForView;

namespace MVVMFirma.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region Fields
        private ReadOnlyCollection<CommandViewModel> _Commands;
        private ObservableCollection<WorkspaceViewModel> _Workspaces;
        #endregion

        #region Commands
        public ReadOnlyCollection<CommandViewModel> Commands
        {
            get
            {
                if (_Commands == null)
                {
                    List<CommandViewModel> cmds = this.CreateCommands();
                    _Commands = new ReadOnlyCollection<CommandViewModel>(cmds);
                }
                return _Commands;
            }
        }
        private List<CommandViewModel> CreateCommands()
        {
            Messenger.Default.Register<string>(this, Open);
            return new List<CommandViewModel>
            {
                new CommandViewModel(
                    "View Products",
                    new BaseCommand(() => this.ShowAllView<AllProductsViewModel>())),

                new CommandViewModel(
                    "View Invoices",
                    new BaseCommand(() => this.ShowAllView<AllInvoicesViewModel>())),

                new CommandViewModel(
                    "View Invoice Positions",
                    new BaseCommand(() => this.ShowAllView<AllInvoicePositionsViewModel>())),

                new CommandViewModel(
                    "View Addresses",
                    new BaseCommand(() => this.ShowAllView<AllAddressViewModel>())),

                new CommandViewModel(
                    "View Categories",
                    new BaseCommand(() => this.ShowAllView<AllCategoriesViewModel>())),

                new CommandViewModel(
                    "View Contractors",
                    new BaseCommand(() => this.ShowAllView<AllContractorsViewModel>())),

                new CommandViewModel(
                    "View Customers",
                    new BaseCommand(() => this.ShowAllView<AllCustomersViewModel>())),

                new CommandViewModel(
                    "View Discounts",
                    new BaseCommand(() => this.ShowAllView<AllDiscountsViewModel>())),

                new CommandViewModel(
                    "View Inventory",
                    new BaseCommand(() => this.ShowAllView<AllInventoryViewModel>())),

                new CommandViewModel(
                    "View Orders",
                    new BaseCommand(() => this.ShowAllView<AllOrdersViewModel>())),

                new CommandViewModel(
                    "View Order Items",
                    new BaseCommand(() => this.ShowAllView<AllOrderItemsViewModel>())),

                new CommandViewModel(
                    "View Payment Methods",
                    new BaseCommand(() => this.ShowAllView<AllPaymentMethodsViewModel>())),

                new CommandViewModel(
                    "View Statuses",
                    new BaseCommand(() => this.ShowAllView<AllStatusViewModel>())),

                new CommandViewModel(
                    "View Types",
                    new BaseCommand(() => this.ShowAllView<AllTypesViewModel>())),

                new CommandViewModel(
                    "Sales Report",
                    new BaseCommand(() => this.CreateView(new SalesReportViewModel()))),

                new CommandViewModel(
                    "Stock Management",
                    new BaseCommand(() => this.CreateView(new StockViewModel()))),

                new CommandViewModel(
                    "Statistics",
                    new BaseCommand(() => this.CreateView(new StatisticsViewModel())))
            };
        }
        #endregion

        #region Workspaces
        public ObservableCollection<WorkspaceViewModel> Workspaces
        {
            get
            {
                if (_Workspaces == null)
                {
                    _Workspaces = new ObservableCollection<WorkspaceViewModel>();
                    _Workspaces.CollectionChanged += this.OnWorkspacesChanged;
                }
                return _Workspaces;
            }
        }
        private void OnWorkspacesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.NewItems)
                    workspace.RequestClose += this.OnWorkspaceRequestClose;

            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.OldItems)
                    workspace.RequestClose -= this.OnWorkspaceRequestClose;
        }
        private void OnWorkspaceRequestClose(object sender, EventArgs e)
        {
            WorkspaceViewModel workspace = sender as WorkspaceViewModel;
            //workspace.Dispos();
            this.Workspaces.Remove(workspace);
        }

        #endregion // Workspaces

        #region Private Helpers
        private void CreateView(WorkspaceViewModel NewView)
        {
            this.Workspaces.Add(NewView);
            this.SetActiveWorkspace(NewView);
        }
        private void ShowAllView<TViewModel>() where TViewModel : WorkspaceViewModel, new()
        {
            TViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is TViewModel) as TViewModel;

            if (workspace == null)
            {
                workspace = new TViewModel();
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }

        private void SetActiveWorkspace(WorkspaceViewModel workspace)
        {
            Debug.Assert(this.Workspaces.Contains(workspace));

            ICollectionView collectionView = CollectionViewSource.GetDefaultView(this.Workspaces);
            if (collectionView != null)
                collectionView.MoveCurrentTo(workspace);
        }

        private void Open(string name)
        {
            switch(name)
            {
                case "ProductsAdd":
                    CreateView(new NewProductViewModel());
                    break;

                case "InvoicesAdd":
                    CreateView(new NewInvoiceViewModel());
                    break;

                case "InvoicesAll":
                    ShowAllView<AllInvoicesViewModel>();
                    break;

                case "InventoryAdd":
                    CreateView(new NewInventoryItemViewModel());
                    break;

                case "ContractorsAll":
                    ShowAllView<AllContractorsViewModel>();
                    break;

                case "ContractorsAdd":
                    CreateView(new NewContractorViewModel());
                    break;

                case "CustomersAdd":
                    CreateView(new NewCustomerViewModel());
                    break;

                case "CustomersAll":
                    ShowAllView<AllCustomersViewModel>();
                    break;

                case "AddressesAll":
                    ShowAllView<AllAddressViewModel>();
                    break;

                case "AddressesAdd":
                    CreateView(new NewAddressViewModel());
                    break;

                case "CategoriesAdd":
                    CreateView(new NewCategoryViewModel());
                    break;

                case "DiscountsAdd":
                    CreateView(new NewDiscountViewModel());
                    break;

                case "PaymentMethodsAdd":
                    CreateView(new NewPaymentMethodViewModel());
                    break;

                case "ProductsAll":
                    ShowAllView<AllProductsViewModel>();
                    break;

                case "StatusesAdd":
                    CreateView(new NewStatusViewModel());
                    break;

                case "TypesAdd":
                    CreateView(new NewTypeViewModel());
                    break;

                case "OrdersAll":
                    ShowAllView<AllOrdersViewModel>();
                    break;

                case "OrdersAdd":
                    CreateView(new NewOrderViewModel());
                    break;

                case "Order ItemsAdd":
                    CreateView(new NewOrderItemViewModel());
                    break;

                case "InvoicePositionAdd":
                    CreateView(new NewInvoicePositionViewModel());
                    break;
            }
        }
        #endregion
    }
}
