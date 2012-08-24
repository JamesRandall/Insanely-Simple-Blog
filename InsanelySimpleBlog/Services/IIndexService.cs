using System.Collections.Generic;
using InsanelySimpleBlog.ViewModel;

namespace InsanelySimpleBlog.Services
{
    public interface IIndexService
    {
        IEnumerable<DateTimeIndexViewModel> DisplayIndexes();
    }
}
