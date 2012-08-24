using System;
using System.ServiceModel.Syndication;
using CuttingEdge.Conditions;
using InsanelySimpleBlog.System.Mappers;
using InsanelySimpleBlog.ViewModel;

namespace InsanelySimpleBlog.Mappers
{
    internal class PostViewModelToSyndicationItemMapper : IMapperWithContext<PostViewModel, SyndicationItem, string>
    {
        public SyndicationItem Map(PostViewModel @from, string baseUri)
        {
            Condition.Requires(@from, "@from").IsNotNull();
            Condition.Requires(baseUri, "baseUri").IsNotNullOrWhiteSpace();

            SyndicationItem item = new SyndicationItem
            {
                BaseUri = new Uri(String.Format("{0}/#/posts/{1}", baseUri, @from.PostID)),
                Content = new TextSyndicationContent(@from.BodyAsHtml),
                Id = @from.ExternalIdentifier.ToString(),
                LastUpdatedTime = @from.PostedAt,
                Title = new TextSyndicationContent(@from.Subject)
            };
            item.Authors.Add(new SyndicationPerson { Name = @from.AuthorName});
            return item;
        }
    }
}
