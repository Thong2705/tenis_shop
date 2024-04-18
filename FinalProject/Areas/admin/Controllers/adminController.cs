using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalProject.Areas.admin.Controllers
{
    public class adminController : Controller
    {
        // GET: admin/admin
        public adminController()
        {
            if (System.Web.HttpContext.Current.Session["Admin"].Equals(""))
            {
                System.Web.HttpContext.Current.Response.Redirect("~/admin/LoginAdmin");
            }
        }
    }
}