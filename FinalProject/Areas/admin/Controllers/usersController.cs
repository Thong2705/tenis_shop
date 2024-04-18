using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinalProject.Models;

namespace FinalProject.Areas.admin.Controllers
{
    public class usersController : adminController
    {
        private ShopEntities2 db = new ShopEntities2();

        // GET: admin/users
        public ActionResult Index()
        {
            return View(db.users.ToList());
        }

        // GET: admin/users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: admin/users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,username,password,fullname,email,phone,image,status,admin,datebegin")] user users, HttpPostedFileBase img)
        {
            var path = "";
            var filename = "";
            if (ModelState.IsValid)
            {
                if (img != null)
                {
                    filename = img.FileName;
                    path = Path.Combine(Server.MapPath("~/Content/img/save-img"), filename);
                    img.SaveAs(path);
                    users.image = filename;
                }
                else
                {
                    users.image = "MinhDang.png";
                }
                users.datebegin = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                users.password = MyString.Encrypt(users.password);
                db.users.Add(users);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(users);
        }

        // GET: admin/users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: admin/users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,username,password,fullname,email,phone,image,status,admin,datebegin")] user user, HttpPostedFileBase img)
        {
            user temp = getById(user.id);
            var path = "";
            var filename = "";
            if (ModelState.IsValid)
            {

                if (img != null)
                {
                    filename = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss-") + img.FileName;
                    path = Path.Combine(Server.MapPath("~/Content/img/save-img"), filename);
                    img.SaveAs(path);
                    temp.image = filename;
                }
                else
                {
                    user.image = "MinhDang.png";
                }
                temp.username = user.username;
                if (temp.password != user.password)
                {
                    temp.password = MyString.Encrypt(user.password);
                }
                temp.fullname= user.fullname;
                temp.email= user.email;
                temp.phone= user.phone;
                temp.status= user.status;
                temp.admin= user.admin;
                temp.datebegin = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                db.Entry(temp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: admin/users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: admin/users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            user user = db.users.Find(id);
            db.users.Remove(user);
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

        public user getById(long id)
        {
            return db.users.Where(x => x.id == id).FirstOrDefault();
        }
    }
}
