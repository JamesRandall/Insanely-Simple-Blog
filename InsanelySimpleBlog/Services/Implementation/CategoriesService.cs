using System.Collections.Generic;
using System.Linq;
using CuttingEdge.Conditions;
using InsanelySimpleBlog.DataModel;
using InsanelySimpleBlog.Mappers;
using InsanelySimpleBlog.System.Mappers;
using InsanelySimpleBlog.System.Repositories;
using InsanelySimpleBlog.System.Repositories.Implementation;
using InsanelySimpleBlog.ViewModel;

namespace InsanelySimpleBlog.Services.Implementation
{
    class CategoriesService : ICategoriesService
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IMapper<Category, CategoryViewModel> _categoryMapper;

        public CategoriesService() : this(new EntityFrameworkUnitOfWorkFactory(), new CategoryToCategoryViewModelMapper())
        {
            
        }

        public CategoriesService(
            IUnitOfWorkFactory unitOfWorkFactory,
            IMapper<Category, CategoryViewModel> categoryMapper)
        {
            Condition.Requires(unitOfWorkFactory, "unitOfWorkFactory").IsNotNull();
            Condition.Requires(categoryMapper, "categoryMapper").IsNotNull();

            _unitOfWorkFactory = unitOfWorkFactory;
            _categoryMapper = categoryMapper;
            
        }

        public IEnumerable<CategoryViewModel> All()
        {
            Category[] categories = null;

            using(IUnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {
                unitOfWork.Execute(() =>
                                        {
                                            IRepository<Category> repository = unitOfWork.GetRepository<Category>();
                                            categories = repository.All.OrderBy(x => x.Name).ToArray();
                                        });
            }

            return categories.Select(x => _categoryMapper.Map(x)).ToArray();
        }
    }
}
