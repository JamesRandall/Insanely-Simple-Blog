using System.Collections.Generic;
using System.Web.Http;
using CuttingEdge.Conditions;
using InsanelySimpleBlog.Services;
using InsanelySimpleBlog.Services.Implementation;
using InsanelySimpleBlog.ViewModel;

namespace InsanelySimpleBlog.Controllers
{
    /// <summary>
    /// We don't just rely on the namespace to differentiate our controller from other controllers that might be called
    /// "PostController" as currently Web API doesn't deal with two controllers in different namespaces with the same name.
    /// </summary>
    public class InsanelySimpleBlogCategoryController : ApiController
    {
        private readonly ICategoriesService _categoriesService;

        public InsanelySimpleBlogCategoryController() : this(new CategoriesService())
        {
            
        }

        public InsanelySimpleBlogCategoryController(
            ICategoriesService categoriesService)
        {
            Condition.Requires(categoriesService, "categoriesService").IsNotNull();

            _categoriesService = categoriesService;
        }

        public IEnumerable<CategoryViewModel> GetAll()
        {
            return _categoriesService.All();
        }
    }
}
