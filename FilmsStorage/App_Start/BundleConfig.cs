using System.Web.Optimization;

namespace FilmsStorage
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(
                        new StyleBundle("~/Content/css")
                    .Include("~/Content/Site.css")
                .Include("~/Content/bootstrap.min.css")
            );

            bundles.Add(
                   new ScriptBundle("~/bundles/modern")
               .Include("~/Scripts/modernizr-2.8.3.js")
           );

            bundles.Add(
                    new ScriptBundle("~/bundles/js")
                .Include("~/Scripts/jquery-3.6.0.min.js")
                .Include("~/Scripts/bootstrap.min.js")
                .Include("~/Scripts/popper.min.js")
            );

            BundleTable.EnableOptimizations = true;
        }
    }
}