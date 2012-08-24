using System;
using System.Collections.Generic;
using System.Linq;
using InsanelySimpleBlog.DataModel;
using InsanelySimpleBlog.Services.Implementation;
using InsanelySimpleBlog.System.Mappers;
using InsanelySimpleBlog.System.Repositories;
using InsanelySimpleBlog.ViewModel;
using NUnit.Framework;
using Rhino.Mocks;

namespace InsanelySimpleBlog.Tests.Unit.Services.Implementation
{
    [TestFixture]
    public class IndexServiceTests
    {
        private IUnitOfWorkFactory _unitOfWorkFactory;
        private IUnitOfWork _unitOfWork;
        private IRepository<DateTimeIndex> _repository;
        private IMapper<DateTimeIndex, DateTimeIndexViewModel> _mapper;

        [SetUp]
        public void Setup()
        {
            _unitOfWorkFactory = MockRepository.GenerateStub<IUnitOfWorkFactory>();
            _unitOfWork = MockRepository.GenerateStub<IUnitOfWork>();
            _repository = MockRepository.GenerateStub<IRepository<DateTimeIndex>>();
            _mapper = MockRepository.GenerateStub<IMapper<DateTimeIndex, DateTimeIndexViewModel>>();

            _unitOfWorkFactory.Stub(x => x.Create()).Return(_unitOfWork);
            _unitOfWork.Stub(x => x.GetRepository<DateTimeIndex>()).Return(_repository);
            _unitOfWork.Stub(x => x.Execute(Arg<Action>.Is.Anything)).WhenCalled(x => ((Action)x.Arguments[0])());
        }

        [Test]
        public void AllReturnsIndexesInDescendingOrder()
        {
            // Arrange
            IndexService service = new IndexService(_unitOfWorkFactory, _mapper);
            DateTimeIndex dateTimeIndex1 = new DateTimeIndex { StartDate = DateTime.Now.Subtract(TimeSpan.FromDays(2)) };
            DateTimeIndex dateTimeIndex2 = new DateTimeIndex { StartDate = DateTime.Now };
            List<DateTimeIndex> indices = new List<DateTimeIndex> { dateTimeIndex1, dateTimeIndex2 };
            DateTimeIndexViewModel model1 = new DateTimeIndexViewModel {NumberOfPosts = 1};
            DateTimeIndexViewModel model2 = new DateTimeIndexViewModel { NumberOfPosts = 2 };
            _mapper.Stub(x => x.Map(dateTimeIndex1)).Return(model1);
            _mapper.Stub(x => x.Map(dateTimeIndex2)).Return(model2);
            _repository.Stub(x => x.All).Return(indices.AsQueryable());

            // Act
            IEnumerable<DateTimeIndexViewModel> models = service.All();

            // Assert
            Assert.That(models.First(), Is.EqualTo(model2));
            Assert.That(models.Last(), Is.EqualTo(model1));
        }
    }
}
