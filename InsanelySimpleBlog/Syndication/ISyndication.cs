using System.ServiceModel.Syndication;
using InsanelySimpleBlog.ViewModel;

namespace InsanelySimpleBlog.Syndication
{
    internal interface ISyndication
    {
        SyndicationFeed BuildFeed(object models, string blogName, string baseUrl);
    }
}
