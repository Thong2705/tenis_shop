using System.Web.Mvc;

namespace FinalProject.Areas.admin
{
    public class adminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "AdminLogin",
                "Admin/login",
                new { Controller = "Auth", action = "Login", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "AdminLogout",
                "Admin/logout",
                new { Controller = "Auth", action = "Logout", id = UrlParameter.Optional }
            );
            context.MapRoute(
                "LoginAdmin",
                "Admin/LoginAdmin",
                new { Controller = "Auth", action = "LoginAdmin", id = UrlParameter.Optional }
            );


            context.MapRoute(
                "admin_default",
                "admin/{controller}/{action}/{id}",
                new { Controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}