using System.Web.Http;
using System.Web.Optimization;
using InsanelySimpleBlog.Formatters;

[assembly: WebActivator.PostApplicationStartMethod(typeof(InsanelySimpleBlog.Configure), "Start")]

namespace InsanelySimpleBlog
{
    public static class Configure
    {
        public static void RegisterRoutes(HttpConfiguration configuration)
        {
            configuration.Routes.MapHttpRoute(
                name: "InsanelySimpleBlogApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            configuration.Formatters.Add(new RssSyndicationFormatter());
            configuration.Formatters.Add(new AtomSyndicationFormatter());
        }

        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/insanelySimpleBlog").Include(
                "~/Scripts/moment.js",
                "~/Scripts/underscore.js",
                "~/Scripts/backbone.js",
                "~/Scripts/handlebars.js",
                "~/Scripts/insanelySimpleBlog.js"
                ));

            bundles.Add(new StyleBundle("~/Content/insanelySimpleBlogCss").Include(
                "~/Content/insanelySimpleBlog.css"));
        }

        public static void Start()
        {
            RegisterRoutes(GlobalConfiguration.Configuration);
            RegisterBundles(BundleTable.Bundles);
        }
    }
}
