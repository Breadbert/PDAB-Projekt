using System;
using System.Collections.Generic;
using System.Linq;
using MVVMFirma.Models.Entities;

namespace MVVMFirma.Models.BusinessLogic
{
    public class StatisticsB : DatabaseClass
    {
        #region Constructor
        public StatisticsB(PDABv2Entities db) : base(db) { }
        #endregion

        #region Business Functions

        public List<MonthlyStatistics> GetMonthlySalesAndCosts(int year)
        {
            return
            (
                from order in db.Orders
                join orderItem in db.OrderItems on order.OrderID equals orderItem.OrderID
                join invoice in db.Invoice on order.OrderID equals invoice.InvoiceID
                join invoicePosition in db.InvoicePosition on invoice.InvoiceID equals invoicePosition.InvoiceID
                where order.OrderDate.Value.Year == year
                group new { orderItem, invoicePosition } by order.OrderDate.Value.Month
                into monthlyGroup
                select new MonthlyStatistics
                {
                    Month = monthlyGroup.Key,
                    Sales = monthlyGroup.Sum(g => g.orderItem.Price * g.orderItem.Quantity),
                    Costs = monthlyGroup.Sum(g => g.invoicePosition.Price * g.invoicePosition.Amount)
                }
            ).OrderBy(stat => stat.Month).ToList();
        }

        #endregion
    }

    public class MonthlyStatistics
    {
        public int Month { get; set; }
        public decimal? Sales { get; set; }
        public decimal? Costs { get; set; }
    }
}
