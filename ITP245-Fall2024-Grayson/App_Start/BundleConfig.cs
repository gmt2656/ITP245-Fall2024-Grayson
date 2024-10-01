using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using ITP245_Fall2024_GraysonModel;

namespace ITP245_Fall2024_Grayson
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            using (var entities = new SportsEntities())
            {
                var options = entities.SystemOptions.First(); // Get the first options entry

                // Ensure that the Bootstrap property is not null or empty
                if (!string.IsNullOrEmpty(options.Bootstrap))
                {
                    // Add jQuery script bundle
                    bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

                    // Add Bootstrap script bundle
                    bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/bootstrap.bundle.js"));

                    // Add CSS bundles with the Bootstrap option
                    bundles.Add(new StyleBundle("~/Content/css").Include(
                        $"~/Content/{options.Bootstrap}", // Use the theme filename from the database
                        "~/Content/Sports.css", // Custom styles
                        "~/Content/site.css")); // Global site styles
                }
                else
                {
                    // Handle the case where Bootstrap is not set
                    // You might want to log this or set a default value
                }
            }

            // Enable optimizations for bundling and minification
            BundleTable.EnableOptimizations = true;
        }
    }
}
