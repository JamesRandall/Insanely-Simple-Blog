using System.Collections.Generic;
using System.Linq;
using CuttingEdge.Conditions;
using InsanelySimpleBlog.DataModel;
using InsanelySimpleBlog.System.Mappers;
using InsanelySimpleBlog.System.Repositories;
using InsanelySimpleBlog.ViewModel;

namespace InsanelySimpleBlog.Services.Implementation
{
    internal class IndexService : IIndexService
    {
        private const int MaximumDisplayIndexes = 10;
        private readonly IMapper<DateTimeIndex, DateTimeIndexViewModel> _mapper;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public IndexService(
            IUnitOfWorkFactory unitOfWorkFactory,
            IMapper<DateTimeIndex, DateTimeIndexViewModel> mapper)
        {
            Condition.Requires(unitOfWorkFactory, "unitOfWorkFactory").IsNotNull();
            Condition.Requires(mapper, "mapper").IsNotNull();

            _unitOfWorkFactory = unitOfWorkFactory;
            _mapper = mapper;
        }

        public IEnumerable<DateTimeIndexViewModel> All()
        {
            DateTimeIndex[] indices = null;

            using(IUnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {
                unitOfWork.Execute(() =>
                                        {
                                            IRepository<DateTimeIndex> repository = unitOfWork.GetRepository<DateTimeIndex>();
                                            indices = repository.All.OrderByDescending(x => x.StartDate).Take(MaximumDisplayIndexes).ToArray();
                                        });
            }

            return indices.Select(x => _mapper.Map(x)).ToArray();
        }
    }
}
