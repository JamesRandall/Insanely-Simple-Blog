using CuttingEdge.Conditions;
using InsanelySimpleBlog.DataModel;
using InsanelySimpleBlog.System.Mappers;
using InsanelySimpleBlog.ViewModel;

namespace InsanelySimpleBlog.Mappers
{
    class CategoryToCategoryViewModelMapper : IMapper<Category, CategoryViewModel>
    {
        public CategoryViewModel Map(Category @from)
        {
            Condition.Requires(@from, "@from").IsNotNull();

            return new CategoryViewModel
                       {
                           CategoryID = @from.CategoryID,
                           Name = @from.Name
                       };
        }
    }
}
