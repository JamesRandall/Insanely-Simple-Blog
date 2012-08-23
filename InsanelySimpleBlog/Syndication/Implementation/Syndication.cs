using System;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using CuttingEdge.Conditions;
using InsanelySimpleBlog.System.Mappers;
using InsanelySimpleBlog.ViewModel;

namespace InsanelySimpleBlog.Syndication.Implementation
{
    class Syndication : ISyndication
    {
        private readonly IMapperWithContext<PostViewModel, SyndicationItem, string> _mapper;

        public Syndication(IMapperWithContext<PostViewModel, SyndicationItem, string> mapper)
        {
            Condition.Requires(mapper, "mapper").IsNotNull();
            _mapper = mapper;
        }

        public SyndicationFeed BuildFeed(object models, SettingsViewModel settings, string baseUrl)
        {
            SyndicationFeed feed = new SyndicationFeed
                                       {
                                           Title = new TextSyndicationContent(settings.Name),
                                           BaseUri = new Uri(settings.BlogPageUrl)
                                       };
            List<SyndicationItem> feedItems = new List<SyndicationItem>();

            if (models is IEnumerable<PostViewModel>)
            {
                IEnumerable<PostViewModel> posts = (IEnumerable<PostViewModel>) models;
                foreach (PostViewModel post in posts)
                {
                    feedItems.Add(_mapper.Map(post, baseUrl));
                }
            }
            else
            {
                PostViewModel post = (PostViewModel) models;
                feedItems.Add(_mapper.Map(post, baseUrl));
            }
            feed.Items = feedItems;
            return feed;
        }
    }
}
