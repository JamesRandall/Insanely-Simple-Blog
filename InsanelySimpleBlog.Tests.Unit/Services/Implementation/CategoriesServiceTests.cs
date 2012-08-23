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
    public class CategoriesServiceTests
    {
        private IUnitOfWorkFactory _unitOfWorkFactory;
        private IUnitOfWork _unitOfWork;
        private IRepository<Category> _repository; 
        private IMapper<Category, CategoryViewModel> _mapper;

        [SetUp]
        public void Setup()
        {
            _unitOfWorkFactory = MockRepository.GenerateStub<IUnitOfWorkFactory>();
            _unitOfWork = MockRepository.GenerateStub<IUnitOfWork>();
            _repository = MockRepository.GenerateStub<IRepository<Category>>();
            _mapper = MockRepository.GenerateStub<IMapper<Category, CategoryViewModel>>();

            _unitOfWorkFactory.Stub(x => x.Create()).Return(_unitOfWork);
            _unitOfWork.Stub(x => x.GetRepository<Category>()).Return(_repository);
            _unitOfWork.Stub(x => x.Execute(Arg<Action>.Is.Anything)).WhenCalled(x => ((Action)x.Arguments[0])());
        }

        [Test]
        public void AllReturnsCategoriesInOrder()
        {
            // Arrange
            CategoriesService service = new CategoriesService(_unitOfWorkFactory, _mapper);
            Category category1 = new Category {CategoryID = 1, Name = "zoo"};
            Category category2 = new Category {CategoryID = 2, Name = "balloon"};
            List<Category> categories = new List<Category> {category1, category2};
            CategoryViewModel model1 = new CategoryViewModel {CategoryID = 1};
            CategoryViewModel model2 = new CategoryViewModel {CategoryID = 2};
            _mapper.Stub(x => x.Map(category1)).Return(model1);
            _mapper.Stub(x => x.Map(category2)).Return(model2);
            _repository.Stub(x => x.All).Return(categories.AsQueryable());

            // Act
            IEnumerable<CategoryViewModel> models = service.All();
            
            // Assert
            Assert.That(models.First(), Is.EqualTo(model2));
            Assert.That(models.Last(), Is.EqualTo(model1));
        }
    }
}
