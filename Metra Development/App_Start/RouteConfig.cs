using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Metra_Development
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                "PostManage",                                           // Route name
                "AddNewPost/{id}",                            // URL with parameters
                new { controller = "PostManage", action = "AddNewPost" }  // Parameter defaults
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                "AdminPanelController",                                           // Route name
                "ReviewNews/{id}",                            // URL with parameters
                new { controller = "AdminPanelController", action = "ReviewNews" }  // Parameter defaults
            );
        
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{language}/{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional, language = "ka-ge" }
            //);
        }
    }
}
