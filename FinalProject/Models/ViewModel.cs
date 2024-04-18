using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class OrderDailyStatisticsViewModel
    {
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }
    }

    public class OrderMonthlyStatisticsViewModel
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal TotalAmount { get; set; }
    }

    public class OrderYearlyStatisticsViewModel
    {
        public int Year { get; set; }
        public decimal TotalAmount { get; set; }
    }

    public class OrderStatisticsViewModel
    {
        public DateTime Date { get; set; }
        public int NumberOfOrders { get; set; }
    }
    public class ProductInventoryViewModel
    {
        public string ProductName { get; set; }
        public int quantity { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
    }
}