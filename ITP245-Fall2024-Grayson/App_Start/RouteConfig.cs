using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace ITP245_Fall2024_Grayson
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            // Ignore routes for certain resources
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Define default route
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            // Additional route for games (optional)
            routes.MapRoute(
                name: "Games",
                url: "Games/{action}/{id}",
                defaults: new { controller = "Games", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
