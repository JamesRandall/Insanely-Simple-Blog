using System.Collections;
using System.Collections.Generic;
using InsanelySimpleBlog.Controllers;
using InsanelySimpleBlog.Services;
using InsanelySimpleBlog.ViewModel;
using NUnit.Framework;
using Rhino.Mocks;

namespace InsanelySimpleBlog.Tests.Unit.Controllers
{
    [TestFixture]
    class InsanelySimpleBlogPostControllerTests
    {
        private IPostsService _service;
        [SetUp]
        public void Setup()
        {
            _service = MockRepository.GenerateStub<IPostsService>();
        }

        [Test]
        public void GetPost_ReturnsPost()
        {
            // Arrange
            PostViewModel serviceModel = new PostViewModel {PostID = 1};
            _service.Stub(x => x.Get(1)).Return(serviceModel);
            InsanelySimpleBlogPostController controller = new InsanelySimpleBlogPostController(_service);

            // Act
            PostViewModel model = controller.GetPost(1);

            // Assert
            Assert.That(model, Is.EqualTo(serviceModel));
        }

        [Test]
        public void GetPosts_ReturnsPosts()
        {
            // Arrange
            List<PostViewModel> serviceModel = new List<PostViewModel> { new PostViewModel { PostID = 1 }};
            _service.Stub(x => x.RecentPosts(1, 2, null, null, null)).Return(serviceModel);
            InsanelySimpleBlogPostController controller = new InsanelySimpleBlogPostController(_service);

            // Act
            IEnumerable<PostViewModel> model = controller.GetPosts(1, 2, null);

            // Assert
            Assert.That(model, Is.EqualTo(serviceModel));
        }
    }
}
