using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InsanelySimpleBlog.Controllers;
using InsanelySimpleBlog.Services;
using InsanelySimpleBlog.ViewModel;
using NUnit.Framework;
using Rhino.Mocks;

namespace InsanelySimpleBlog.Tests.Unit.Controllers
{
    [TestFixture]
    public class InsanelySimpleBlogCategoryControllerTests
    {
        private ICategoriesService _service;
        [SetUp]
        public void Setup()
        {
            _service = MockRepository.GenerateStub<ICategoriesService>();
        }

        [Test]
        public void GetCategoriesReturnsCategories()
        {
            // Arrange
            List<CategoryViewModel> serviceModel = new List<CategoryViewModel> { new CategoryViewModel { CategoryID = 1 } };
            _service.Stub(x => x.All()).Return(serviceModel);
            InsanelySimpleBlogCategoryController controller = new InsanelySimpleBlogCategoryController(_service);

            // Act
            IEnumerable<CategoryViewModel> model = controller.GetAll();

            // Assert
            Assert.That(model, Is.EqualTo(serviceModel));
        }
    }
}
