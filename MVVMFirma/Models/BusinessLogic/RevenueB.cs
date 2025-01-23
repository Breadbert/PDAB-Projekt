using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using MVVMFirma.Helper;
using MVVMFirma.Models.Entities;

namespace MVVMFirma.Models.BusinessLogic
{
    public class RevenueB : DatabaseClass
    {
        #region Constructor
        public RevenueB(PDABv2Entities db) : base(db) { }
        #endregion

        #region Business functions
        public decimal? TotalRevenue(DateTime dateFrom, DateTime dateTo)
        {
            return
            (
                from order in db.Orders
                where order.OrderDate >= dateFrom && order.OrderDate <= dateTo
                join orderItem in db.OrderItems
                on order.OrderID equals orderItem.OrderID
                select orderItem.Price * orderItem.Quantity
            ).Sum();
        }

        public decimal? TotalInvoiceRevenue(DateTime dateFrom, DateTime dateTo)
        {
            return
            (
                from invoice in db.Invoice
                where invoice.IssueDate >= dateFrom && invoice.IssueDate <= dateTo
                join invoicePosition in db.InvoicePosition
                on invoice.InvoiceID equals invoicePosition.InvoiceID
                select invoicePosition.Price * invoicePosition.Amount
            ).Sum();
        }

        public decimal? CalculateNetProfit(DateTime dateFrom, DateTime dateTo)
        {
            var totalOrderRevenue =
            (
                from order in db.Orders
                where order.OrderDate >= dateFrom && order.OrderDate <= dateTo
                join orderItem in db.OrderItems
                on order.OrderID equals orderItem.OrderID
                select orderItem.Price * orderItem.Quantity
            ).Sum();

            var totalInvoiceRevenue =
            (
                from invoice in db.Invoice
                where invoice.IssueDate >= dateFrom && invoice.IssueDate <= dateTo
                join invoicePosition in db.InvoicePosition
                on invoice.InvoiceID equals invoicePosition.InvoiceID
                select invoicePosition.Price * invoicePosition.Amount
            ).Sum();

            return totalOrderRevenue - totalInvoiceRevenue;
        }

        public decimal? TotalRevenueForCategoryCD(DateTime dateFrom, DateTime dateTo)
        {
            return
            (
                from order in db.Orders
                where order.OrderDate >= dateFrom && order.OrderDate <= dateTo
                join orderItem in db.OrderItems
                on order.OrderID equals orderItem.OrderID
                join product in db.Product
                on orderItem.ProductID equals product.ProductID
                join category in db.Categories
                on product.CategoryID equals category.CategoryID
                where category.Name == "CD"
                select orderItem.Price * orderItem.Quantity
            ).Sum();
        }

        public decimal? TotalRevenueForCategoryVinyl(DateTime dateFrom, DateTime dateTo)
        {
            return
            (
                from order in db.Orders
                where order.OrderDate >= dateFrom && order.OrderDate <= dateTo
                join orderItem in db.OrderItems
                on order.OrderID equals orderItem.OrderID
                join product in db.Product
                on orderItem.ProductID equals product.ProductID
                join category in db.Categories
                on product.CategoryID equals category.CategoryID
                where category.Name == "Vinyl"
                select orderItem.Price * orderItem.Quantity
            ).Sum();
        }

        public decimal? TotalRevenueForCategoryMC(DateTime dateFrom, DateTime dateTo)
        {
            return
            (
                from order in db.Orders
                where order.OrderDate >= dateFrom && order.OrderDate <= dateTo
                join orderItem in db.OrderItems
                on order.OrderID equals orderItem.OrderID
                join product in db.Product
                on orderItem.ProductID equals product.ProductID
                join category in db.Categories
                on product.CategoryID equals category.CategoryID
                where category.Name == "Cassette"
                select orderItem.Price * orderItem.Quantity
            ).Sum();
        }
        #endregion
    }
}
