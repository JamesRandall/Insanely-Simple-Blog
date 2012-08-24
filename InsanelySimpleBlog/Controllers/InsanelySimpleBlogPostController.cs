using System;
using System.Collections.Generic;
using System.Web.Http;
using CuttingEdge.Conditions;
using InsanelySimpleBlog.Services;
using InsanelySimpleBlog.ViewModel;

namespace InsanelySimpleBlog.Controllers
{
    /// <summary>
    /// We don't just rely on the namespace to differentiate our controller from other controllers that might be called
    /// "PostController" as currently Web API doesn't deal with two controllers in different namespaces with the same name.
    /// </summary>
    public class InsanelySimpleBlogPostController : ApiController
    {
        private readonly IPostsService _postsService;
        
        public InsanelySimpleBlogPostController(
            IPostsService postsService)
        {
            Condition.Requires(postsService, "postsService").IsNotNull();
            
            _postsService = postsService;
        }

        public PostViewModel GetPost(int id)
        {
            return _postsService.Get(id);
        }

        public IEnumerable<PostViewModel> GetPosts(int pageNumber, int pageSize, int? categoryId=null, DateTime? startDate= null, DateTime? endDate=null)
        {
            return _postsService.RecentPosts(pageNumber, pageSize, categoryId);
        }
    }
}
