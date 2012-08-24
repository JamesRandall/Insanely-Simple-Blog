using System;
using System.Collections.Generic;
using InsanelySimpleBlog.DataModel;
using InsanelySimpleBlog.Mappers;
using InsanelySimpleBlog.Markdown;
using InsanelySimpleBlog.System.Mappers;
using InsanelySimpleBlog.ViewModel;
using NUnit.Framework;
using Rhino.Mocks;

namespace InsanelySimpleBlog.Tests.Unit.Mappers
{
    [TestFixture]
    public class PostToPostViewModelMapperTests
    {
        private IMapper<Category, CategoryViewModel> _categoryMapper;
        private IMarkdownConverter _markdownConverter;

        [SetUp]
        public void Setup()
        {
            _categoryMapper = MockRepository.GenerateStub<IMapper<Category, CategoryViewModel>>();
            _markdownConverter = MockRepository.GenerateStub<IMarkdownConverter>();
        }

        [Test]
        public void MapReturnsPostViewModel()
        {
            // Arrange
            DateTime referenceDate = DateTime.Now;
            Author author = new Author {AuthorID = 1, Name = "Fred"};
            Category category = new Category {CategoryID = 1, Name = "Cat"};
            Guid externalIdentifier = Guid.NewGuid();
            Post post = new Post
                            {
                                Author = author,
                                AuthorID = 1,
                                Body = "Some text",
                                Categories = new List<Category> {category},
                                ExternalIdentifier = externalIdentifier,
                                PostID = 2,
                                PostedAt = referenceDate,
                                Subject = "A subject"
                            };
            _categoryMapper.Stub(x => x.Map(category)).Return(new CategoryViewModel());
            _markdownConverter.Stub(x => x.ToHtml("Some text")).Return("converted");
            PostToPostViewModelMapper mapper = new PostToPostViewModelMapper(_categoryMapper, _markdownConverter);

            // Act
            PostViewModel result = mapper.Map(post);

            // Assert
            Assert.That(result.AuthorName, Is.EqualTo("Fred"));
            Assert.That(result.AuthorId, Is.EqualTo(1));
            Assert.That(result.BodyAsHtml, Is.EqualTo("converted"));
            Assert.That(result.Categories.Count, Is.EqualTo(1));
            Assert.That(result.ExternalIdentifier, Is.EqualTo(externalIdentifier));
            Assert.That(result.PostID, Is.EqualTo(2));
            Assert.That(result.PostedAt, Is.EqualTo(referenceDate));
            Assert.That(result.Subject, Is.EqualTo("A subject"));
        }
    }
}
