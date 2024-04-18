using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinalProject.Models;

namespace FinalProject.Areas.admin.Controllers
{
    public class orderspriceController : Controller
    {
        private ShopEntities2 db = new ShopEntities2();

        // GET: admin/ordersprice
        public ActionResult Index()
        {
            // Thống kê theo ngày
            var dailyStatistics = db.orders
                .GroupBy(o => DbFunctions.TruncateTime(o.datebegin))
                .Select(g => new OrderDailyStatisticsViewModel
                {
                    Date = g.Key.Value,
                    TotalAmount = g.Sum(o => o.total ?? 0) // Sử dụng biểu thức điều kiện
                })
                .ToList();

            // Thống kê theo tháng
            var monthlyStatistics = db.orders
    .GroupBy(o => new { Year = o.datebegin.Value.Year, Month = o.datebegin.Value.Month })
    .Select(g => new OrderMonthlyStatisticsViewModel
    {
        Year = g.Key.Year,
        Month = g.Key.Month,
        TotalAmount = g.Sum(o => o.total ?? 0) // Sử dụng biểu thức điều kiện
    })
    .ToList();

            // Thống kê theo năm
            var yearlyStatistics = db.orders
                .GroupBy(o => o.datebegin.Value.Year)
                .Select(g => new OrderYearlyStatisticsViewModel
                {
                    Year = g.Key,
                    TotalAmount = g.Sum(o => o.total ?? 0) // Sử dụng biểu thức điều kiện
                })
                .ToList();

            ViewBag.DailyStatistics = dailyStatistics;
            ViewBag.MonthlyStatistics = monthlyStatistics;
            ViewBag.YearlyStatistics = yearlyStatistics;

            return View();

        }

        // GET: admin/ordersprice/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            order order = db.orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: admin/ordersprice/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/ordersprice/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,code,customname,phone,address,total,description,typepayment,datebegin")] order order)
        {
            if (ModelState.IsValid)
            {
                db.orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(order);
        }

        // GET: admin/ordersprice/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            order order = db.orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: admin/ordersprice/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,code,customname,phone,address,total,description,typepayment,datebegin")] order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: admin/ordersprice/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            order order = db.orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: admin/ordersprice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            order order = db.orders.Find(id);
            db.orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
