﻿using System.Web.Optimization;

namespace FreeMarket
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryunobtrusive").Include(
                        "~/Scripts/jquery.unobtrusive-ajax.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                      "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrapValidator").Include(
                      "~/Scripts/bootstrapValidator.min.js",
                      "~/Scripts/moment.min.js",
                      "~/Scripts/bootstrap-datetimepicker.min.js",
                      "~/Scripts/star-rating.js"));

            bundles.Add(new ScriptBundle("~/bundles/animsition").Include(
                      "~/Scripts/animsition.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/morris").Include(
                      "~/Scripts/raphael.min.js",
                      "~/Scripts/morris.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/globalisation").Include(
                      "~/Scripts/globalize.0.1.3/globalize.js",
                      "~/Scripts/globalize.0.1.3/cultures/globalize.culture.en-ZA.js"
                    ));

            bundles.Add(new ScriptBundle("~/bundles/zoom").Include(
                      "~/Scripts/jquery.zoom.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      //"~/Content/bootstrap-theme.min.css",
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap.datetimepicker.min.css",
                      "~/Content/site.css",
                      "~/Content/animsition.min.css",
                      "~/Content/star-rating.css",
                      "~/Content/morris.css"));
        }
    }
}
