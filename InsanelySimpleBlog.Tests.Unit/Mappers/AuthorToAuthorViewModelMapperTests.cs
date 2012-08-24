using InsanelySimpleBlog.DataModel;
using InsanelySimpleBlog.Mappers;
using InsanelySimpleBlog.ViewModel;
using NUnit.Framework;

namespace InsanelySimpleBlog.Tests.Unit.Mappers
{
    [TestFixture]
    public class AuthorToAuthorViewModelMapperTests
    {
        [Test]
        public void AuthorMapsToViewModel()
        {
            // Arrange
            AuthorToAuthorViewModelMapper mapper = new AuthorToAuthorViewModelMapper();
            Author author = new Author {AuthorID = 1, Name = "User"};

            // Act
            AuthorViewModel result = mapper.Map(author);

            // Assert
            Assert.That(result.AuthorID, Is.EqualTo(1));
            Assert.That(result.Name, Is.EqualTo("User"));
        }
    }
}
