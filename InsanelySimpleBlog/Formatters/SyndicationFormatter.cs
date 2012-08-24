using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;
using CuttingEdge.Conditions;
using InsanelySimpleBlog.Services;
using InsanelySimpleBlog.Syndication;
using InsanelySimpleBlog.ViewModel;

namespace InsanelySimpleBlog.Formatters
{
    abstract class SyndicationFormatter : MediaTypeFormatter
    {
        private readonly ISettingsService _settingsService;
        private readonly ISyndication _syndication;

        protected SyndicationFormatter(string format,
            ISettingsService settingsService,
            ISyndication syndication)
        {
            Condition.Requires(syndication, "syndication").IsNotNull();
            Condition.Requires(settingsService, "settingsService").IsNotNull();
            Condition.Requires(format, "format").IsNotNullOrWhiteSpace();

            _settingsService = settingsService;
            _syndication = syndication;
        } 

        public override bool CanReadType(Type type)
        {
            return false;
        }

        public override bool CanWriteType(Type type)
        {
            return (type == typeof(PostViewModel) || type == typeof(IEnumerable<PostViewModel>));
        }

        public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext)
        {
            return Task.Factory.StartNew(() =>
                                             {
                                                 if (type == typeof(PostViewModel) || type == typeof(IEnumerable<PostViewModel>))
                                                 {
                                                     BuildSyndicationFeed(value, writeStream);
                                                 }
                                             });
        }

        private void BuildSyndicationFeed(object models, Stream writeStream)
        {
            SettingsViewModel settings = _settingsService.GetSettings();
            string baseUrl = settings.BlogPageUrl;
            SyndicationFeed feed = _syndication.BuildFeed(models, settings.Name, baseUrl);

            WriteFeed(writeStream, feed);
        }

        protected virtual void WriteFeed(Stream writeStream, SyndicationFeed feed)
        {
            using (XmlWriter writer = XmlWriter.Create(writeStream))
            {
                SyndicationFeedFormatter formatter = GetFormatter(feed);
                formatter.WriteTo(writer);
            }
        }

        protected abstract SyndicationFeedFormatter GetFormatter(SyndicationFeed feed);
    }
}
