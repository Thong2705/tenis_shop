using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FinalProject
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Product", "{type}/{meta}",
                new { controller = "Product", action = "Index", meta = UrlParameter.Optional },
                new RouteValueDictionary
                {
                    {"type", "san-pham" }
                },
                namespaces: new[] { "FinalProject.Controllers" });

            routes.MapRoute("Detail", "{type}/{meta}/{id}",
                new { controller = "Product", action = "Detail", id = UrlParameter.Optional },
                new RouteValueDictionary
                {
                    {"type", "san-pham" }
                },
                namespaces: new[] { "FinalProject.Controllers" });

            routes.MapRoute("NewsList", "{type}",
                new { controller = "News", action = "Index" },
                new RouteValueDictionary
                {
                    {"type", "tin-tuc" }
                },
                namespaces: new[] { "FinalProject.Controllers" });

            routes.MapRoute("DetailNews", "{type}/{meta}/{id}",
                new { controller = "News", action = "Detail", id = UrlParameter.Optional },
                new RouteValueDictionary
                {
                    {"type", "tin-tuc" }
                },
                namespaces: new[] { "FinalProject.Controllers" });

            routes.MapRoute("Cart", "{type}",

               new { controller = "Cart", action = "Index" },
               new RouteValueDictionary
               {
                    {"type","gio-hang" }
               },
              namespaces: new[] { "FinalProject.Controllers" });

            routes.MapRoute("CheckOut", "{type}",

               new { controller = "Cart", action = "CheckOut" },
               new RouteValueDictionary
               {
                    {"type","thanh-toan" }
               },
              namespaces: new[] { "FinalProject.Controllers" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "FinalProject.Controllers" }
            );
        }
    }
}
