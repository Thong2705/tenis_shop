using FinalProject.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalProject.Controllers
{
    public class DefaultController : Controller
    {
        ShopEntities2 _db = new ShopEntities2();
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult getCategory()
        {
            var v = from t in _db.categories select t;
            return PartialView(v.ToList());
        }

        public ActionResult getProduct(int? page)
        {
            var products = _db.products.ToList();
            int pageSize = 9;
            int pageNumber = (page ?? 1);
            return PartialView(products.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult getNews()
        {
            var v = from t in _db.news where t.hide == false orderby t.order descending select t;
            return PartialView(v.ToList());
        }

    }
}