using System.Web.Mvc;
using System.Web.Routing;

namespace Gite.WebSite
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Decription",
                url: "Description",
                defaults: new { controller = "Home", action = "Description" }
            );
            routes.MapRoute(
                name: "Contact",
                url: "Contact",
                defaults: new { controller = "Home", action = "Contact" }
            );
            routes.MapRoute(
                name: "Souvenirs",
                url: "Souvenirs",
                defaults: new { controller = "Home", action = "Souvenirs" }
            );
            routes.MapRoute(
                name: "Activites",
                url: "Activites",
                defaults: new { controller = "Home", action = "Activites" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
