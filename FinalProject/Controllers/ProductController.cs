using FinalProject.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;

namespace FinalProject.Controllers
{
    public class ProductController : Controller
    {
        ShopEntities2 _db = new ShopEntities2();
        // GET: Product 
        public ActionResult Index(string meta, int? page)
        {
            ViewBag.PageNumber = page ?? 1;
            var v = from t in _db.categories
                    where t.meta == meta
                    select t;
            return View(v.FirstOrDefault());
        }

        public ActionResult getCategoryList()
        {
            ViewBag.meta = "san-pham";
            var v = from t in _db.categories where t.hide == false select t;
            return PartialView(v.ToList());
        }

        public ActionResult getProductList(long id, string metatitle, int? page)
        {
            ViewBag.meta = metatitle;
            var v = from t in _db.products orderby t.order ascending where t.hide == false && t.categoryid == id select t;
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            return PartialView(v.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Detail(long id)
        {
            var v = from t in _db.products
                    where t.id== id
                    select t;
            return View(v.FirstOrDefault()); 
        }
        public ActionResult Image_Product(long id)
        {
            var v = from t in _db.image_product 
                    where t.productid == id
                    select t;
            return PartialView(v.ToList());
        }
    }
}