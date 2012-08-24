using InsanelySimpleBlog.DataModel;
using InsanelySimpleBlog.Mappers;
using InsanelySimpleBlog.ViewModel;
using NUnit.Framework;

namespace InsanelySimpleBlog.Tests.Unit.Mappers
{
    [TestFixture]
    public class SettingsToSettingsViewModelMapperTests
    {
        [Test]
        public void MapReturnsSettingsViewModel()
        {
            // Arrange
            Settings settings = new Settings
                                    {
                                        BlogPageUrl = "http://myblog/mypage",
                                        Name = "My Insane Blog"
                                    };
            SettingsToSettingsViewModelMapper mapper = new SettingsToSettingsViewModelMapper();

            // Act
            SettingsViewModel result = mapper.Map(settings);

            // Assert
            Assert.That(result.BlogPageUrl, Is.EqualTo("http://myblog/mypage"));
            Assert.That(result.Name, Is.EqualTo("My Insane Blog"));
        }
    }
}
