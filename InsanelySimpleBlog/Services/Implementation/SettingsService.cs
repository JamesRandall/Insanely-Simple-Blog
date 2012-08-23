using System.Linq;
using CuttingEdge.Conditions;
using InsanelySimpleBlog.DataModel;
using InsanelySimpleBlog.System.Mappers;
using InsanelySimpleBlog.System.Repositories;
using InsanelySimpleBlog.ViewModel;

namespace InsanelySimpleBlog.Services.Implementation
{
    class SettingsService : ISettingsService
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IMapper<Settings, SettingsViewModel> _mapper;

        public SettingsService(IUnitOfWorkFactory unitOfWorkFactory,
            IMapper<Settings, SettingsViewModel> mapper)
        {
            Condition.Requires(unitOfWorkFactory, "unitOfWorkFactory").IsNotNull();
            Condition.Requires(mapper, "mapper").IsNotNull();

            _unitOfWorkFactory = unitOfWorkFactory;
            _mapper = mapper;
        }

        public SettingsViewModel GetSettings()
        {
            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {
                Settings settings= null;
                unitOfWork.Execute(() =>
                                       {
                                           IRepository<Settings> repository = unitOfWork.GetRepository<Settings>();
                                           settings = repository.All.Single();
                                       });
                return _mapper.Map(settings);
            }
        }
    }
}
