using System.Web;
using System.Web.Optimization;

namespace AnzamtechWS
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = true;
            BundleTable.Bundles.UseCdn = true;

            /*----------------------------------------------------------*/
            //content share for admin vs user
            bundles.Add(new StyleBundle("~/style/share").Include(
                "~/Content/Share/bootstrap.min.css",
                "~/Content/Share/PagedList.css"));
          
            //scrip jquery required
            bundles.Add(new ScriptBundle("~/bundle/jquery").Include(
                "~/Scripts/Share/jquery.min.js"
                ));
            //script share for admin vs user
            //load ajax
            bundles.Add(new ScriptBundle("~/bundle/share").Include(
                 "~/Scripts/Share/popper.min.js",
                 "~/Scripts/Share/jquery.tmpl.min",
                 "~/Scripts/Share/bootstrap.min.js"
               
                 ));
            bundles.Add(new ScriptBundle("~/bundle/ajax").Include(
                 "~/Scripts/Share/jquery.unobtrusive-ajax.min.js"));
            /*----------------------------------------------------------*/
            // content for user
            bundles.Add(new StyleBundle("~/style/user").Include(
              "~/Content/User/mainstyle.css"));
            //script for user
            bundles.Add(new ScriptBundle("~/bundle/user").Include(
              "~/Scripts/User/jquery.lazyload.min.js",
              "~/Scripts/User/jquery.scrollstop.min.js",
              "~/Scripts/User/user.js"));
            /*----------------------------------------------------------*/
            //content for admin
            bundles.Add(new StyleBundle("~/style/admin").Include(
              "~/Content/Admin/animate.min.css",
              "~/Content/Admin/imagedialog.css",
              "~/Content/Admin/style.min.css",
              "~/Content/Admin/waves.min.css"));
            bundles.Add(new StyleBundle("~/style/imagedialog").Include(
                "~/Content/ImageDialog/style.min.css"
                ));
            //script for admin
            bundles.Add(new ScriptBundle("~/bundle/admin").Include(
               "~/Scripts/Admin/admin.js",
               "~/Scripts/Admin/jquery.slimscroll.js",
               "~/Scripts/Admin/waves.min.js"));
            bundles.Add(new ScriptBundle("~/bundle/imagedialog").Include(
               "~/Scripts/ImageDialog/jstree.min.js",
               "~/Scripts/ImageDialog/ImageDialog.js"));
        }
    }
}
