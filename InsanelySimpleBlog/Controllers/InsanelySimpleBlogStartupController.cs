using System.Web.Http;
using CuttingEdge.Conditions;
using InsanelySimpleBlog.Services;
using InsanelySimpleBlog.ViewModel;

namespace InsanelySimpleBlog.Controllers
{
    public class InsanelySimpleBlogStartupController : ApiController
    {
        private readonly IPostsService _postsService;
        private readonly ICategoriesService _categoriesService;
        private readonly IIndexService _indexService;

        public InsanelySimpleBlogStartupController(
            IPostsService postsService,
            ICategoriesService categoriesService,
            IIndexService indexService)
        {
            Condition.Requires(postsService, "postsService").IsNotNull();
            Condition.Requires(categoriesService, "categoriesService").IsNotNull();
            Condition.Requires(indexService, "indexService").IsNotNull();

            _postsService = postsService;
            _categoriesService = categoriesService;
            _indexService = indexService;
        }

        public StartupViewModel Get()
        {
            StartupViewModel model = new StartupViewModel
                                         {
                                             Categories = _categoriesService.All(),
                                             Indices = _indexService.DisplayIndexes(),
                                             Posts = _postsService.RecentPosts(0, 10, null)
                                         };
            return model;
        }
    }
}
