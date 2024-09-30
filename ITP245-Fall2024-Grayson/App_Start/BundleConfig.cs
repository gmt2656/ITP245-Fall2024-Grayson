using System.Web;
using System.Web.Optimization;

namespace ITP245_Fall2024_Grayson
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Add jQuery script bundle
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Add jQuery validation script bundle
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Add Modernizr for feature detection
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            // Add Bootstrap script bundle
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/bootstrap.bundle.js"));

            // Add Bootstrap theme bundle with a hard-coded path
            AddBootstrapThemeBundle(bundles);

            // Add existing CSS files including your Sports.css
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/Sports.css",
                      "~/Content/site.css"));

            // Enable optimizations for bundling and minification
            BundleTable.EnableOptimizations = true;
        }

        // Method to add Bootstrap theme bundle with a static path
        private static void AddBootstrapThemeBundle(BundleCollection bundles)
        {
            // Hard-code the Bootstrap theme path
            string bootstrapThemePath = "~/Content/bootstraptheme1.css"; // Example hard-coded path

            // Add the bundle using the hard-coded theme path
            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                        bootstrapThemePath));
        }
    }
}
