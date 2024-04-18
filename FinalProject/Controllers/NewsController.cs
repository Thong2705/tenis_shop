using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace FinalProject.Controllers
{
    public class NewsController : Controller
    {
        ShopEntities2 _db = new ShopEntities2();
        // GET: New
        public ActionResult Index(int? page)
        {
            ViewBag.PageNumber = page ?? 1; // Trang hiện tại là 1 nếu không có tham số truyền vào

            return View();
        }
        //public ActionResult getNewsList()
        //{
        //    var v = from t in _db.news select t;
        //    return PartialView(v.ToList());
        //}

        public ActionResult getNewsList(int? page)
        {
            var v = from t in _db.news where t.hide == false orderby t.order descending select t;
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            return PartialView(v.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Detail(long id)
        {
            var v = from t in _db.news
                    where t.id == id
                    select t;
            return View(v.FirstOrDefault());
        }
    }
}