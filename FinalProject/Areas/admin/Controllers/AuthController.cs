using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor.Parser.SyntaxTree;
using FinalProject.Models;

namespace FinalProject.Areas.admin.Controllers
{
    public class AuthController : Controller
    {
        ShopEntities2 _db = new ShopEntities2();
        // GET: admin/Auth
        public ActionResult Login()
        {
            if (!Session["UserAdmin"].Equals(""))
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Error = "";

            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection field)
        {
            string strError = "";
            string username = field["username"];
            string password = MyString.Encrypt(field["password"]);
            user rowuser = _db.users.Where(m => m.status == true && (m.username == username || m.email == username)).FirstOrDefault();
            if (rowuser == null)
            {
                strError = "Tên đăng nhập không tồn tại";
            }
            else
            {
                if (rowuser.password.Equals(password))
                {
                    if(rowuser.admin == true) {
                        Session["UserAdmin"] = rowuser.username;
                        Session["UserId"] = rowuser.id;
                        Session["Fullname"] = rowuser.fullname;
                        Session["Image"] = rowuser.image;
                        Session["Admin"] = "Access";
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        Session["UserAdmin"] = rowuser.username;
                        Session["UserId"] = rowuser.id;
                        Session["Fullname"] = rowuser.fullname;
                        Session["Image"] = rowuser.image;
                        Session["Admin"] = "";
                        return RedirectToAction("Index", "Home");
                    }

                }
                else
                {
                    strError = "Mật khẩu không đúng";
                }
            }



            ViewBag.Error = "<span class= 'text-danger'>" + strError + "</span>";
            return View();
        }

        public ActionResult LoginAdmin()
        {
            if (!Session["Admin"].Equals(""))
            {
                return RedirectToAction("Index", "users");
            }
            ViewBag.Error = "";

            return View();
        }

        public ActionResult Logout()
        {
            Session["UserAdmin"] = "";
            Session["UserId"] = "";
            Session["Fullname"] = "";
            Session["Image"] = "";
            Session["Admin"] = "";
            return RedirectToAction("Login", "Auth");
        }
    }
}