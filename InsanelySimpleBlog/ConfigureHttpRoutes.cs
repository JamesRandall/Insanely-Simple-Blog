using System.Web.Http;
using InsanelySimpleBlog.Formatters;

namespace InsanelySimpleBlog
{
    public static class ConfigureHttpRoutes
    {
        public static void RegisterRoutes(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "InsanelySimpleBlogApi",
                routeTemplate: "api/InsanelySimpleBlog{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            GlobalConfiguration.Configuration.Formatters.Add(new RssSyndicationFormatter());
            GlobalConfiguration.Configuration.Formatters.Add(new AtomSyndicationFormatter());
        }
    }
}
