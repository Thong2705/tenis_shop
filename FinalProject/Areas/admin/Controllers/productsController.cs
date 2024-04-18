using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinalProject.Models;

namespace FinalProject.Areas.admin.Controllers
{
    public class productsController : BaseController
    {
        private ShopEntities2 db = new ShopEntities2();

        // GET: admin/products
        public ActionResult Index(long? id = null)
        {
            getCategory(id);
            return View();
        }

        public void getCategory(long? selectedId = null)
        {
            ViewBag.Category = new SelectList(db.categories.Where(x => x.hide == false)
                .OrderBy(x => x.order), "id", "name", selectedId);
        }

        public ActionResult getProduct(long? id)
        {
            if (id == null)
            {
                var v = db.products.OrderBy(x => x.order).ToList();
                return PartialView(v);
            }
            var m = db.products.Where(x => x.categoryid == id).OrderBy(x => x.order).ToList();
            return PartialView(m);
        }

        // GET: admin/products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: admin/products/Create
        public ActionResult Create()
        {
            getCategory();
            return View();
        }

        // POST: admin/products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "id,name,price,img,description,meta,size,color,hide,order,datebegin,categoryid,quantity")] product product, [Bind(Include = "id,img,order,productid")] image_product image_Product,
            HttpPostedFileBase[] files, HttpPostedFileBase img)
        {
            try
            {
                int count = 0;
                var path = "";
                var filename = "";
                var path1 = "";
                var filename1 = "";
                if (ModelState.IsValid)
                {
                    product.datebegin = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                    product.order = getMaxOrder(product.categoryid);
                    db.products.Add(product);
                    if (img != null)
                    {
                        filename1 = img.FileName;
                        path1 = Path.Combine(Server.MapPath("~/Content/img/product-img"), filename1);
                        img.SaveAs(path1);
                        product.img = filename1;
                    }
                    else
                    {
                        product.img = "logo.png";
                    }
                    foreach (var file in files)
                    {
                        if (file != null)
                        {
                            filename = file.FileName;
                            path = Path.Combine(Server.MapPath("~/Content/img/product-img"), filename);
                            file.SaveAs(path);
                            image_Product.img = filename;
                            image_Product.productid = product.id;
                            image_Product.order = count;
                            db.image_product.Add(image_Product);
                            db.SaveChanges();
                            count++;
                        }
                        else
                        {
                            image_Product.img = "logo.png";
                            image_Product.productid = product.id;
                            db.image_product.Add(image_Product);
                            db.SaveChanges();
                        }
                    }
                    db.SaveChanges();
                    return RedirectToAction("Index", "products", new { id = product.categoryid });
                }
            }
            catch (DbEntityValidationException e)
            {
                throw e;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(product);
        }

        // GET: admin/products/Edit/5

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            getCategory(product.categoryid);
            return View(product);
        }

        // POST: admin/products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "id,name,price,img,description,meta,size,color,hide,order,datebegin,categoryid,quantity")] product product, HttpPostedFileBase img)
        {
            try
            {
                var path = "";
                var filename = "";
                product temp = db.products.Find(product.id);
                if (ModelState.IsValid)
                {
                    if (img != null)
                    {
                        filename = img.FileName;
                        path = Path.Combine(Server.MapPath("~/Content/img/product-img"), filename);
                        img.SaveAs(path);
                        temp.img = filename;
                    }
                    temp.name = product.name;
                    temp.price = product.price;
                    temp.description = product.description;
                    temp.meta = product.meta;
                    temp.color = product.color;
                    temp.size = product.size;
                    temp.hide = product.hide;
                    temp.order = product.order;
                    temp.datebegin = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                    temp.categoryid = product.categoryid;
                    temp.quantity= product.quantity;
                    db.Entry(temp).State = EntityState.Modified; db.SaveChanges();

                    return RedirectToAction("Index", "products", new { id = product.categoryid });
                }
            }
            catch (DbEntityValidationException e)
            {
                throw e;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(product);
        }

        // GET: admin/products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);

            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: admin/products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            product product = db.products.Find(id);
            var v = from t in db.image_product where t.productid == id select t;
            var x = from t in db.orderdetails where t.productid == id select t;
            foreach (var item in x)
            {
                db.orderdetails.Remove(item);
            }
            foreach (var item in v)
            {
                db.image_product.Remove(item);
            }
            db.products.Remove(product);
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

        public product getById(long id)
        {
            return (product)db.products.Where(m => m.id == id);
        }

        public int getMaxOrder(int? CategoryId)
        {
            return db.products.Where(x => x.categoryid == CategoryId).Count();
        }
    }
}
