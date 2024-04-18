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
    public class image_productController : BaseController
    {
        private ShopEntities2 db = new ShopEntities2();

        // GET: admin/image_product
        public ActionResult Index()
        {
            var image_product = db.image_product.Include(i => i.product);
            return View(image_product.ToList());
        }

        // GET: admin/image_product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            image_product image_product = db.image_product.Find(id);
            if (image_product == null)
            {
                return HttpNotFound();
            }
            return View(image_product);
        }

        // GET: admin/image_product/Create
        public ActionResult Create()
        {
            ViewBag.productid = new SelectList(db.products, "id", "name");
            return View();
        }

        // POST: admin/image_product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,img,order,productid")] image_product image_product)
        {
            if (ModelState.IsValid)
            {
                db.image_product.Add(image_product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.productid = new SelectList(db.products, "id", "name", image_product.productid);
            return View(image_product);
        }

        // GET: admin/image_product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            image_product image_product = db.image_product.Find(id);
            if (image_product == null)
            {
                return HttpNotFound();
            }
            ViewBag.productid = new SelectList(db.products, "id", "name", image_product.productid);
            return View(image_product);
        }

        // POST: admin/image_product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,img,order,productid")] image_product image_product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(image_product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.productid = new SelectList(db.products, "id", "name", image_product.productid);
            return View(image_product);
        }

        // GET: admin/image_product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            image_product image_product = db.image_product.Find(id);
            if (image_product == null)
            {
                return HttpNotFound();
            }
            return View(image_product);
        }

        // POST: admin/image_product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            image_product image_product = db.image_product.Find(id);
            db.image_product.Remove(image_product);
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
