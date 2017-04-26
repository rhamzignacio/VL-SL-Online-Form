using System.Web;
using System.Web.Optimization;

namespace VL_SL_Online_Form
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/vendors/bootstrap/dist/css/bootstrap.css",
                "~/Content/vendors/font-awesome/css/font-awesome.min.css",
                "~/Content/vendors/nprogress/nprogress.css",
                "~/Content/vendors/bootstrap-daterangepicker/daterangepicker.css",
                "~/Content/vendors/select2/dist/css/select2.min.css",
                "~/Content/build/css/custom.css",
                "~/Content/vendors/switchery/dist/switchery.min.css",
                "~/Content/vendors/starrr/dist/starrr.cs",
                "~/Content/vendors/growl/angular-growl.min.css",
                "~/Content/vendors/iCheck/skins/flat/green.css",
                "~/Content/vendors/fullcalendar/dist/fullcalendar.min.css",
                "~/Content/vendors/fullcalendar/dist/fullcalendar.print.css"
            ));

            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                "~/Content/vendors/jquery/dist/jquery.min.js",
                "~/Content/vendors/bootstrap/dist/js/bootstrap.min.js",
                "~/Content/vendors/fastclick/lib/fastclick.js",
                "~/Content/vendors/nprogress/nprogress.js",
                "~/Content/vendors/Chart.js/dist/Chart.min.js",
                "~/Content/vendors/jquery-sparkline/dist/jquery.sparkline.min.js",
                "~/Content/vendors/Flot/jquery.flot.js",
                "~/Content/vendors/Flot/jquery.flot.pie.js",
                "~/Content/vendors/Flot/jquery.flot.time.js",
                "~/Content/vendors/Flot/jquery.flot.stack.js",
                "~/Content/vendors/Flot/jquery.flot.resize.js",
                "~/Content/vendors/flot.orderbars/js/jquery.flot.orderBars.js",
                "~/Content/vendors/flot-spline/js/jquery.flot.spline.min.js",
                "~/Content/vendors/flot.curvedlines/curvedLines.js",
                "~/Content/vendors/DateJS/build/date.js",
                "~/Content/vendors/moment/min/moment.min.js",
                "~/Content/vendors/bootstrap-daterangepicker/daterangepicker.js",
                "~/Content/vendors/select2/dist/js/select2.full.min.js",
                "~/Content/build/js/custom.min.js",
                "~/Content/vendors/jquery.tagsinput/src/jquery.tagsinput.js",
                "~/Content/vendors/switchery/dist/switchery.min.js",
                "~/Scripts/angular.js",
                "~/Scripts/respond.js",
                "~/Scripts/angular-growl.min.js",
                "~/Content/vendors/autosize/dist/autosize.min.js",
                "~/Content/vendors/starrr/dist/starrr.js",
                "~/Content/vendors/bootstrap-wysiwyg/js/bootstrap-wysiwyg.min.js",
                "~/Content/vendors/jquery.hotkeys/jquery.hotkeys.js",
                "~/Content/angular-file-upload.js",
                "~/Content/vendors/fullcalendar/dist/fullcalendar.min.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                "~/App/App.js",
                "~/App/Controller/Login.js",
                "~/App/Controller/HRModule.js",
                "~/App/Controller/Calendar.js"
            ));

        }
    }
}
