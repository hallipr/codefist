using System.Web;
using System.Web.Optimization;

namespace CodeFist.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                      "~/Scripts/angular-loader.js",
                      "~/Scripts/angular.js",
                      "~/Scripts/angular-route.js",
                      "~/Scripts/angular-resource.js",
                      "~/Scripts/ui-bootstrap-tpls-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/codemirror").Include(
                  "~/Scripts/jshint.js",
                  "~/Scripts/codemirror/lib/codemirror.js",
                  "~/Scripts/codemirror/addon/lint/lint.js",
                  "~/Scripts/codemirror/addon/lint/javascript-lint.js",
                  "~/Scripts/codemirror/addon/fold/foldcode.js",
                  "~/Scripts/codemirror/addon/fold/foldgutter.js",
                  "~/Scripts/codemirror/addon/fold/brace-fold.js",
                  "~/Scripts/codemirror/addon/fold/comment-fold.js",
                  "~/Scripts/codemirror/mode/javascript/javascript.js",
                  "~/Scripts/codemirror/ui-codemirror.js"));

            bundles.Add(new ScriptBundle("~/bundles/app").IncludeDirectory("~/app", "*.js", true));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap-theme.css",
                      "~/Scripts/codemirror/lib/codemirror.css",
                      "~/Scripts/codemirror/addon/fold/foldgutter.css",
                      "~/Scripts/codemirror/addon/lint/lint.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/codemirror").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
