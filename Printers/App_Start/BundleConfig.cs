using System.Web.Optimization;

namespace Printers
{
    public class BundleConfig
    {
        // Дополнительные сведения об объединении см. на странице https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate.js",
                        "~/Scripts/jquery.validate.unobtrusive.js",
                        "~/Scripts/jquery.validate.fix.js"));

            bundles.Add(new ScriptBundle("~/bundles/inputmask").Include(
                "~/Scripts/inputmask/inputmask.js",
                "~/Scripts/inputmask/inputmask.extensions.js",
                "~/Scripts/inputmask/inputmask.numeric.extensions.js",
                "~/Scripts/inputmask/inputmask.date.extensions.js",
                "~/Scripts/inputmask/jquery.inputmask.js"));


            // Используйте версию Modernizr для разработчиков, чтобы учиться работать. Когда вы будете готовы перейти к работе,
            // готово к выпуску, используйте средство сборки по адресу https://modernizr.com, чтобы выбрать только необходимые тесты.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/printersCreate").Include(
                "~/Scripts/Site/PrintersCreate.js"));

            bundles.Add(new ScriptBundle("~/bundles/search").Include(
                "~/Scripts/Site/Search.js"));
            bundles.Add(new ScriptBundle("~/bundles/selectCabinet").Include(
                "~/Scripts/Site/SelectCabinet.js"));
        }
    }
}
