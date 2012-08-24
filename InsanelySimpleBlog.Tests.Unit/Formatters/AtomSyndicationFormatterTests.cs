using System.Linq;
using System.Net.Http.Formatting;
using InsanelySimpleBlog.Formatters;
using InsanelySimpleBlog.Services;
using InsanelySimpleBlog.Syndication;
using InsanelySimpleBlog.ViewModel;
using NUnit.Framework;
using Rhino.Mocks;

namespace InsanelySimpleBlog.Tests.Unit.Formatters
{
    [TestFixture]
    public class AtomSyndicationFormatterTests
    {
        private ISettingsService _settingsService;
        private ISyndication _syndication;

        [SetUp]
        public void Setup()
        {
            _syndication = MockRepository.GenerateStub<ISyndication>();
            _settingsService = MockRepository.GenerateStub<ISettingsService>();
            _settingsService.Stub(x => x.GetSettings()).Return(new SettingsViewModel
            {
                BlogPageUrl = "http://localhost/mypage",
                Name = "My blog"
            });
        }

        [Test]
        public void ConfigurationIsCorrect()
        {
            // Arrange
            AtomSyndicationFormatter formatter = new AtomSyndicationFormatter("html/text", _settingsService, _syndication);
            
            // Assert
            Assert.That(formatter.SupportedMediaTypes.Any(x => x.MediaType == "application/atom+xml"), Is.True);
            Assert.That(((QueryStringMapping)formatter.MediaTypeMappings[0]).QueryStringParameterName, Is.EqualTo("formatter"));
            Assert.That(((QueryStringMapping)formatter.MediaTypeMappings[0]).QueryStringParameterValue, Is.EqualTo("atom"));
            Assert.That(((QueryStringMapping)formatter.MediaTypeMappings[0]).MediaType.MediaType, Is.EqualTo("html/text"));
        }
    }
}
