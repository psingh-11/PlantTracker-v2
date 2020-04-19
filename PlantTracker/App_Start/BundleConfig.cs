using System.Web;
using System.Web.Optimization;

namespace PlantTracker
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",                    
                      "~/Content/site.css"));


            bundles.Add(new ScriptBundle("~/bundles/home").Include(
                "~/Scripts/dataTable.min.js",
                "~/app/Plant_table/js/plant_table.js",
                 "~/app/Plant_table/js/plant_table_service.js",
                "~/app/common/js/toolbox.js"

                ));

            bundles.Add(new StyleBundle("~/Plant_Details/css").Include(
                "~/App/Plant_Details/css/plant_detail.css"));

            bundles.Add(new StyleBundle("~/Plant_Details/js").Include(
                   "~/app/common/js/toolbox.js",
                    "~/App/Plant_Details/js/plant_detail.js",
                    "~/App/Plant_Details/js/plant_Detail_service.js"
                    ));





        }
    }
}
