using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
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
    public class PostsServiceTests
    {
        private IRepository<Post> _postRepository; 
        private IUnitOfWorkFactory _unitOfWorkFactory;
        private IUnitOfWork _unitOfWork;
        private IMapper<Post, PostViewModel> _mapper;
        private Post _post1;
        private Post _post2;
        private Category _category1;
        private Category _category2;

        [SetUp]
        public void Setup()
        {
            _category1 = new Category {CategoryID = 1};
            _category2 = new Category {CategoryID = 2};
            _post1 = new Post { PostID = 1, PostedAt = DateTime.Now.Subtract(TimeSpan.FromDays(50)), Categories = new Collection<Category>{_category1}};
            _post2 = new Post { PostID = 2,  PostedAt = DateTime.Now, Categories = new Collection<Category>{ _category2}};
            List<Post> posts = new List<Post> { _post1, _post2 };

            _unitOfWorkFactory = MockRepository.GenerateStub<IUnitOfWorkFactory>();
            _unitOfWork = MockRepository.GenerateStub<IUnitOfWork>();
            _postRepository = MockRepository.GenerateStub<IRepository<Post>>();
            _mapper = MockRepository.GenerateStub<IMapper<Post, PostViewModel>>();

            _unitOfWorkFactory.Stub(x => x.Create()).Return(_unitOfWork);
            _unitOfWork.Stub(x => x.GetRepository<Post>()).Return(_postRepository);
            _unitOfWork.Stub(x => x.Execute(Arg<Action>.Is.Anything)).WhenCalled(x => ((Action)x.Arguments[0])());
            
            _postRepository.Stub(y => y.AllIncluding(Arg<Expression<Func<Post, object>>>.Is.Anything)).Return(posts.AsQueryable());
        }

        [Test]
        public void GetReturnsCorrectPost()
        {
            // Arrange
            PostViewModel mappedPost = new PostViewModel {PostID = 2};
            PostsService service = new PostsService(_unitOfWorkFactory, _mapper);
            _mapper.Stub(x => x.Map(_post2)).Return(mappedPost);

            // Act
            PostViewModel model = service.Get(2);

            // Assert
            Assert.That(model, Is.EqualTo(mappedPost));
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RecentPostsThrowsExceptionOnTooLargePageSize()
        {
            // Arrange
            PostsService service = new PostsService(_unitOfWorkFactory, _mapper);

            // Act
            service.RecentPosts(0, 220, null, null, null);
        }

        [Test]
        public void RecentPostsInDescendingDateOrder()
        {
            // Arrange
            PostViewModel mappedPost1 = new PostViewModel {PostID = 1};
            PostViewModel mappedPost2 = new PostViewModel {PostID = 2};
            PostsService service = new PostsService(_unitOfWorkFactory, _mapper);
            _mapper.Stub(x => x.Map(_post1)).Return(mappedPost1);
            _mapper.Stub(x => x.Map(_post2)).Return(mappedPost2);

            // Act
            IEnumerable<PostViewModel> models = service.RecentPosts(0, 10, null, null, null);

            // Assert
            Assert.That(models.First(), Is.EqualTo(mappedPost2));
            Assert.That(models.Skip(1).First(), Is.EqualTo(mappedPost1));
        }

        [Test]
        public void RecentPostsInCategory()
        {
            // Arrange
            PostViewModel mappedPost1 = new PostViewModel { PostID = 1 };
            PostViewModel mappedPost2 = new PostViewModel { PostID = 2 };
            PostsService service = new PostsService(_unitOfWorkFactory, _mapper);
            _mapper.Stub(x => x.Map(_post1)).Return(mappedPost1);
            _mapper.Stub(x => x.Map(_post2)).Return(mappedPost2);

            // Act
            IEnumerable<PostViewModel> models = service.RecentPosts(0, 10, 1, null, null);

            // Assert
            Assert.That(models.First(), Is.EqualTo(mappedPost1));
            Assert.That(models.Count(), Is.EqualTo(1));
        }

        [Test]
        public void RecentPostsAfterStartDate()
        {
            // Arrange
            PostViewModel mappedPost2 = new PostViewModel { PostID = 2 };
            PostsService service = new PostsService(_unitOfWorkFactory, _mapper);
            _mapper.Stub(x => x.Map(_post2)).Return(mappedPost2);

            // Act
            IEnumerable<PostViewModel> models = service.RecentPosts(0, 10, null, DateTime.Now.Subtract(TimeSpan.FromDays(1)), null);

            // Assert
            Assert.That(models.First(), Is.EqualTo(mappedPost2));
            Assert.That(models.Count(), Is.EqualTo(1));
        }

        [Test]
        public void RecentPostsBeforeEndDate()
        {
            // Arrange
            PostViewModel mappedPost1 = new PostViewModel { PostID = 1 };
            PostsService service = new PostsService(_unitOfWorkFactory, _mapper);
            _mapper.Stub(x => x.Map(_post1)).Return(mappedPost1);

            // Act
            IEnumerable<PostViewModel> models = service.RecentPosts(0, 10, null, null, DateTime.Now.Subtract(TimeSpan.FromDays(1)));

            // Assert
            Assert.That(models.First(), Is.EqualTo(mappedPost1));
            Assert.That(models.Count(), Is.EqualTo(1));
        }
    }
}
