using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalProject.Areas.admin.Controllers
{
    public class inventoryController : Controller
    {
        // GET: admin/inventory
        private ShopEntities2 db = new ShopEntities2();

        // GET: admin/inventory
        public ActionResult Index()
        {
            var productInventory = db.products
                .Select(od => new ProductInventoryViewModel
                {
                    ProductName = od.name,
                    quantity = (int)od.quantity,
                    Month = od.datebegin.Value.Month,
                    Year = od.datebegin.Value.Year
                })
                .OrderByDescending(od => od.Year)
                .ThenByDescending(od => od.Month)
                .ToList();

            return View(productInventory);
        }
    }
}