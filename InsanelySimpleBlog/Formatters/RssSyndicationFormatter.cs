using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.ServiceModel.Syndication;
using System.Web.Http;
using InsanelySimpleBlog.Services;
using InsanelySimpleBlog.Syndication;

namespace InsanelySimpleBlog.Formatters
{
    class RssSyndicationFormatter : SyndicationFormatter
    {
        private const string Rss = "application/rss+xml";

        public RssSyndicationFormatter() : this("text/html",
            (ISettingsService)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(ISettingsService)),
            (ISyndication)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(ISyndication)))
        {
            
        }

        public RssSyndicationFormatter(string format,
            ISettingsService settingsService,
            ISyndication syndication) : base(format, settingsService, syndication)
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue(Rss));
            this.AddQueryStringMapping("formatter", "rss", new MediaTypeHeaderValue(format));
        }

        protected override SyndicationFeedFormatter GetFormatter(SyndicationFeed feed)
        {
            return new Rss20FeedFormatter(feed);
        }
    }
}
