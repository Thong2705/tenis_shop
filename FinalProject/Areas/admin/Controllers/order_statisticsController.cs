using CsvHelper.Configuration;
using CsvHelper;
using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalProject.Areas.admin.Controllers
{
    public class order_statisticsController : Controller
    {
        // GET: admin/order_statistics
        public ActionResult Index()
        {
            ShopEntities2 _db = new ShopEntities2();
            // Thống kê số đơn hàng trong một ngày
            var dailyStatistics = _db.orders
                .GroupBy(o => DbFunctions.TruncateTime(o.datebegin))
                .Select(g => new OrderStatisticsViewModel
                {
                    Date = g.Key.Value,
                    NumberOfOrders = g.Count()
                })
                .ToList();

            // Thống kê số đơn hàng trong một tháng
            var monthlyStatistics = _db.orders
    .ToList() // Trích xuất dữ liệu từ database sang memory
    .GroupBy(o => new { Year = o.datebegin.Value.Year, Month = o.datebegin.Value.Month })
    .Select(g => new OrderStatisticsViewModel
    {
        Date = new DateTime(g.Key.Year, g.Key.Month, 1),
        NumberOfOrders = g.Count()
    })
    .ToList();

            // Thống kê số đơn hàng trong một năm
            var yearlyStatistics = _db.orders
    .ToList() // Trích xuất dữ liệu từ database sang memory
    .GroupBy(o => o.datebegin.Value.Year)
    .Select(g => new OrderStatisticsViewModel
    {
        Date = new DateTime(g.Key, 1, 1),
        NumberOfOrders = g.Count()
    })
    .ToList();

            ViewBag.DailyStatistics = dailyStatistics;
            ViewBag.MonthlyStatistics = monthlyStatistics;
            ViewBag.YearlyStatistics = yearlyStatistics;

            return View();
        }
        private void ExportToCsv<T>(IEnumerable<T> data, string fileName)
        {
            var configuration = new CsvConfiguration(CultureInfo.CurrentCulture);
            using (var writer = new StreamWriter(Server.MapPath("~/Content/CSVFiles/" + fileName)))
            using (var csv = new CsvWriter(writer, configuration))
            {
                csv.WriteRecords(data);
            }
        }
        public ActionResult DownloadDailyCsv()
        {
            ShopEntities2 _db = new ShopEntities2();
            var dailyStatistics = _db.orders
                .GroupBy(o => DbFunctions.TruncateTime(o.datebegin))
                .Select(g => new OrderStatisticsViewModel
                {
                    Date = g.Key.Value,
                    NumberOfOrders = g.Count()
                })
                .ToList();

            ExportToCsv(dailyStatistics, "DailyStatistics.csv");
            return File("~/Content/CSVFiles/DailyStatistics.csv", "text/csv", "DailyStatistics.csv");
        }

        public ActionResult DownloadMonthlyCsv()
        {
            ShopEntities2 _db = new ShopEntities2();

            var monthlyStatistics = _db.orders
                .ToList() // Fetch data from the database to memory
                .GroupBy(o => new { Year = o.datebegin.Value.Year, Month = o.datebegin.Value.Month })
                .Select(g => new OrderStatisticsViewModel
                {
                    Date = new DateTime(g.Key.Year, g.Key.Month, 1),
                    NumberOfOrders = g.Count()
                })
                .ToList();

            ExportToCsv(monthlyStatistics, "MonthlyStatistics.csv");
            return File("~/Content/CSVFiles/MonthlyStatistics.csv", "text/csv", "MonthlyStatistics.csv");
        }


        public ActionResult DownloadYearlyCsv()
        {
            ShopEntities2 _db = new ShopEntities2();

            var yearlyStatistics = _db.orders
                .ToList() // Fetch data from the database to memory
                .GroupBy(o => o.datebegin.Value.Year)
                .Select(g => new OrderStatisticsViewModel
                {
                    Date = new DateTime(g.Key, 1, 1),
                    NumberOfOrders = g.Count()
                })
                .ToList();

            ExportToCsv(yearlyStatistics, "YearlyStatistics.csv");
            return File("~/Content/CSVFiles/YearlyStatistics.csv", "text/csv", "YearlyStatistics.csv");
        }


    }
}