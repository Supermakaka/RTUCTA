using System.Web;
using System.Web.Optimization;

namespace WebSite
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/main-scripts").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate.js",
                        "~/Scripts/jquery.validate.unobtrusive.js",
                        "~/Scripts/jquery.validate.unobtrusive.bootstrap.customized.js",
                        "~/Scripts/moment.js",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/bootstrap.min.js",
                        "~/Scripts/jquery.datetimepicker.js",
                        "~/Scripts/leaflet.js",
                        "~/Scripts/leaflet-src.js",
                        "~/Scripts/bootstrap-alert.js",
                        "~/Scripts/site.js",
                        "~/Scripts/leaflet-routing-machine.js"
            
                        ));

            bundles.Add(new StyleBundle("~/bundles/main-styles")
                .Include("~/Content/bootstrap.css", new CssRewriteUrlTransform())
                .Include("~/Content/bootstrap-datetimepicker.css", new CssRewriteUrlTransform())
                .Include("~/Content/style.css", new CssRewriteUrlTransform())
                .Include("~/Content/leaflet.css", new CssRewriteUrlTransform())
                .Include("~/Content/leaflet-routing-machine.css", new CssRewriteUrlTransform())
            );
        }
    }
}