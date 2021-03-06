﻿using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.ServiceModel.Syndication;
using System.Web.Http;
using InsanelySimpleBlog.Services;
using InsanelySimpleBlog.Services.Implementation;
using InsanelySimpleBlog.Syndication;

namespace InsanelySimpleBlog.Formatters
{
    class AtomSyndicationFormatter : SyndicationFormatter
    {
        private const string Atom = "application/atom+xml";

        public AtomSyndicationFormatter() : this("text/html", new SettingsService(), new Syndication.Implementation.Syndication())
        {
            
        }

        public AtomSyndicationFormatter(string format,
            ISettingsService settingsService,
            ISyndication syndication) : base(format, settingsService, syndication)
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue(Atom));
            this.AddQueryStringMapping("formatter", "atom", new MediaTypeHeaderValue(format));
        }

        protected override SyndicationFeedFormatter GetFormatter(SyndicationFeed feed)
        {
            return new Atom10FeedFormatter(feed);
        }
    }
}
