using System.Collections.Generic;
using System.Web.Http;
using CuttingEdge.Conditions;
using InsanelySimpleBlog.Services;
using InsanelySimpleBlog.ViewModel;

namespace InsanelySimpleBlog.Controllers
{
    public class InsanelySimpleBlogIndexController : ApiController
    {
        private readonly IIndexService _indexService;

        public InsanelySimpleBlogIndexController(IIndexService indexService)
        {
            Condition.Requires(indexService, "indexService").IsNotNull();
            _indexService = indexService;
        }

        public IEnumerable<DateTimeIndexViewModel> GetIndices()
        {
            return _indexService.DisplayIndexes();
        }
    }
}
