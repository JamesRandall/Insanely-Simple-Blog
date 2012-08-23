using System.Collections.Generic;
using InsanelySimpleBlog.ViewModel;

namespace InsanelySimpleBlog.Services
{
    public interface ICategoriesService
    {
        IEnumerable<CategoryViewModel> All();
    }
}
