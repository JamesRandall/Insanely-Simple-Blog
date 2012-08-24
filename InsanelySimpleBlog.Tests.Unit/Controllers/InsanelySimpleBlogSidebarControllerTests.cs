using System.Collections.Generic;
using InsanelySimpleBlog.Controllers;
using InsanelySimpleBlog.Services;
using InsanelySimpleBlog.ViewModel;
using NUnit.Framework;
using Rhino.Mocks;

namespace InsanelySimpleBlog.Tests.Unit.Controllers
{
    [TestFixture]
    public class InsanelySimpleBlogSidebarControllerTests
    {
        private ICategoriesService _categoriesService;
        private IIndexService _indexService;

        [SetUp]
        public void Setup()
        {
            _categoriesService = MockRepository.GenerateStub<ICategoriesService>();
            _indexService = MockRepository.GenerateStub<IIndexService>();
        }

        [Test]
        public void GetReturnsBothIndicesAndCategories()
        {
            // Arrange
            _categoriesService.Stub(x => x.All()).Return(new List<CategoryViewModel> {new CategoryViewModel()});
            _indexService.Stub(x => x.All()).Return(new List<DateTimeIndexViewModel> {new DateTimeIndexViewModel()});
            InsanelySimpleBlogSidebarController controller = new InsanelySimpleBlogSidebarController(_categoriesService, _indexService);

            // Act
            SidebarViewModel result = controller.Get();

            // Assert
            Assert.That(result.Indices, Is.Not.Empty);
            Assert.That(result.Categories, Is.Not.Empty);
        }
    }
}
