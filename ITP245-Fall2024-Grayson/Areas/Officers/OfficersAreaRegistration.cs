using System.Web.Mvc;

namespace ITP245_Fall2024_Grayson.Areas.Officers
{
    public class OfficersAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Officers";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            // General route for the Officers area
            context.MapRoute(
                "Officers_default", // Route name
                "Officers/{controller}/{action}/{id}", // URL pattern
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Defaults
            );
        }
    }
}
