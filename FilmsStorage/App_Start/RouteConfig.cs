using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FilmsStorage
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //Для роботи маршрутів на рівні контролерів та екшенів
            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "Shortcut",
                "Test",
                defaults: new {controller= "Home", action = "Test"}
                );

            routes.MapRoute(
                name: "ShowError",
                "Home/Error",
                defaults: new {controller = "Home", action = "Error"}
                );

            routes.MapRoute(
                name: "Files",
                url: "Files/{action}/File/{fileID}/User/{userID}",
                defaults: new { controller = "Files", fileID = UrlParameter.Optional, userID = UrlParameter.Optional}
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
