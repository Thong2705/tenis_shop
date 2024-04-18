using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalProject.Controllers
{
    public class HomeController : Controller
    {
        ShopEntities2 _db = new ShopEntities2();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Footer()
        {
            var v = from t in _db.Menus where t.hide == false orderby t.order ascending select t;
            return PartialView(v);
        }

        public ActionResult CheckOut()
        {
            return View();
        }
    }
}