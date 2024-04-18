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
    public class orderdetailsController : Controller
    {
        private ShopEntities2 db = new ShopEntities2();

        // GET: admin/orderdetails
        public ActionResult Index()
        {
            var orderdetails = db.orderdetails.Include(o => o.order).Include(o => o.product);
            return View(orderdetails.ToList());
        }

        // GET: admin/orderdetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            orderdetail orderdetail = db.orderdetails.Find(id);
            if (orderdetail == null)
            {
                return HttpNotFound();
            }
            return View(orderdetail);
        }

        // GET: admin/orderdetails/Create
        public ActionResult Create()
        {
            ViewBag.orderid = new SelectList(db.orders, "id", "code");
            ViewBag.productid = new SelectList(db.products, "id", "name");
            return View();
        }

        // POST: admin/orderdetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,orderid,productid,price,quantity")] orderdetail orderdetail)
        {
            if (ModelState.IsValid)
            {
                db.orderdetails.Add(orderdetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.orderid = new SelectList(db.orders, "id", "code", orderdetail.orderid);
            ViewBag.productid = new SelectList(db.products, "id", "name", orderdetail.productid);
            return View(orderdetail);
        }

        // GET: admin/orderdetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            orderdetail orderdetail = db.orderdetails.Find(id);
            if (orderdetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.orderid = new SelectList(db.orders, "id", "code", orderdetail.orderid);
            ViewBag.productid = new SelectList(db.products, "id", "name", orderdetail.productid);
            return View(orderdetail);
        }

        // POST: admin/orderdetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,orderid,productid,price,quantity")] orderdetail orderdetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderdetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.orderid = new SelectList(db.orders, "id", "code", orderdetail.orderid);
            ViewBag.productid = new SelectList(db.products, "id", "name", orderdetail.productid);
            return View(orderdetail);
        }

        // GET: admin/orderdetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            orderdetail orderdetail = db.orderdetails.Find(id);
            if (orderdetail == null)
            {
                return HttpNotFound();
            }
            return View(orderdetail);
        }

        // POST: admin/orderdetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            orderdetail orderdetail = db.orderdetails.Find(id);
            db.orderdetails.Remove(orderdetail);
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
