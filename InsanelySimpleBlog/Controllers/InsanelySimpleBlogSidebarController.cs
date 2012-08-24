using System.Web.Http;
using CuttingEdge.Conditions;
using InsanelySimpleBlog.Services;
using InsanelySimpleBlog.ViewModel;

namespace InsanelySimpleBlog.Controllers
{
    public class InsanelySimpleBlogSidebarController : ApiController
    {
        private readonly ICategoriesService _categoriesService;
        private readonly IIndexService _indexService;

        public InsanelySimpleBlogSidebarController(
            ICategoriesService categoriesService,
            IIndexService indexService)
        {
            Condition.Requires(categoriesService, "categoriesService").IsNotNull();
            Condition.Requires(indexService, "indexService").IsNotNull();

            _categoriesService = categoriesService;
            _indexService = indexService;
        }

        public SidebarViewModel Get()
        {
            SidebarViewModel model = new SidebarViewModel
                                         {
                                             Categories = _categoriesService.All(),
                                             Indices = _indexService.All(),
                                         };
            return model;
        }
    }
}
