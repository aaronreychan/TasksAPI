using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Optimization;

namespace CBCTestWebApp
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                 "~/Content/bootstrap.css",
                 "~/Content/Site.css"));

            bundles.Add(new ScriptBundle("~/bundles/myAngularApp")
                //.Include("~/Scripts/angular1.2.min.js")
                //.Include("~/Scripts/angular-route1.2.min.js")
                .IncludeDirectory("~/Scripts/angular-ui", "*.js")
                .IncludeDirectory("~/Scripts/App", "*.js")
                .IncludeDirectory("~/Scripts/Controllers", "*.js")
                    );
        }
    }
}
