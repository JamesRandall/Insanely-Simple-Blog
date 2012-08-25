using System.ServiceModel.Syndication;
using InsanelySimpleBlog.DataModel;
using InsanelySimpleBlog.Mappers;
using InsanelySimpleBlog.Markdown;
using InsanelySimpleBlog.Markdown.Implementation;
using InsanelySimpleBlog.Services;
using InsanelySimpleBlog.Services.Implementation;
using InsanelySimpleBlog.Syndication;
using InsanelySimpleBlog.System.Configuration;
using InsanelySimpleBlog.System.Configuration.Implementation;
using InsanelySimpleBlog.System.Mappers;
using InsanelySimpleBlog.System.Policies;
using InsanelySimpleBlog.System.Policies.Implementation;
using InsanelySimpleBlog.System.Repositories;
using InsanelySimpleBlog.System.Repositories.Implementation;
using InsanelySimpleBlog.System.Threading;
using InsanelySimpleBlog.System.Threading.Implementation;
using InsanelySimpleBlog.ViewModel;
using Microsoft.Practices.Unity;

namespace InsanelySimpleBlog
{
    public static class UnityBootstrapper
    {
        public static void RegisterDependencies(IUnityContainer container)
        {
            // System
            container.RegisterType<IConfiguration, Configuration>();
            container.RegisterType<IUnitOfWorkFactory, EntityFrameworkUnitOfWorkFactory>();
            container.RegisterType<IWaitHandle, ManualResetEventWaitHandle>();
            container.RegisterType<IRetryPolicy, SqlAzureRetryPolicy>(RetryPolicyType.Sql);

            // Data
            container.RegisterType<IDbContextFactory, SimpleBlogContextFactory>();
            
            // helpers
            container.RegisterType<IMarkdownConverter, MarkdownConverter>();
            container.RegisterType<ISyndication, Syndication.Implementation.Syndication>();

            // mappers
            container.RegisterType<IMapper<DateTimeIndex, DateTimeIndexViewModel>, DateTimeIndexToDateTimeIndexViewModelMapper>();
            container.RegisterType<IMapper<Settings, SettingsViewModel>, SettingsToSettingsViewModelMapper>();
            container.RegisterType<IMapper<Author, AuthorViewModel>, AuthorToAuthorViewModelMapper>();
            container.RegisterType<IMapper<Category, CategoryViewModel>, CategoryToCategoryViewModelMapper>();
            container.RegisterType<IMapper<Post, PostViewModel>, PostToPostViewModelMapper>();
            container.RegisterType<IMapperWithContext<PostViewModel, SyndicationItem, string>, PostViewModelToSyndicationItemMapper>();

            // services
            container.RegisterType<IPostsService, PostsService>();
            container.RegisterType<ICategoriesService, CategoriesService>();
            container.RegisterType<ISettingsService, SettingsService>();
            container.RegisterType<IIndexService, IndexService>();
        }
    }
}
