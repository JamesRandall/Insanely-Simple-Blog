using InsanelySimpleBlog.DataModel;
using InsanelySimpleBlog.Mappers;
using InsanelySimpleBlog.ViewModel;
using NUnit.Framework;

namespace InsanelySimpleBlog.Tests.Unit.Mappers
{
    [TestFixture]
    public class CategoryToCategoryViewModelMapperTests
    {
        [Test]
        public void MapsToCategoryViewModel()
        {
            // Arrange
            CategoryToCategoryViewModelMapper mapper = new CategoryToCategoryViewModelMapper();
            Category category = new Category {CategoryID = 1, Name = "Cat"};

            // Act
            CategoryViewModel result = mapper.Map(category);

            // Assert
            Assert.That(result.CategoryID, Is.EqualTo(1));
            Assert.That(result.Name, Is.EqualTo("Cat"));
        }
    }
}
