using System.Collections.Generic;
using System.IO;
using System.ServiceModel.Syndication;
using InsanelySimpleBlog.Formatters;
using InsanelySimpleBlog.Services;
using InsanelySimpleBlog.Syndication;
using InsanelySimpleBlog.ViewModel;
using NUnit.Framework;
using Rhino.Mocks;

namespace InsanelySimpleBlog.Tests.Unit.Formatters
{
    [TestFixture]
    public class SyndicationFormatterTests
    {
        private class NonAbstractSyndicationFormatter : SyndicationFormatter
        {
            public bool WrittenToFeed { get; set; }

            public NonAbstractSyndicationFormatter(string format, ISettingsService settingsService, ISyndication syndication) : base(format, settingsService, syndication)
            {
                WrittenToFeed = false;
            }

            protected override void WriteFeed(Stream writeStream, SyndicationFeed feed)
            {
                WrittenToFeed = true;
            }

            protected override SyndicationFeedFormatter GetFormatter(SyndicationFeed feed)
            {
                return null;
            }
        }

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
        public void WriteToStreamAsyncBuildsFeed()
        {
            // Arrange
            NonAbstractSyndicationFormatter formatter = new NonAbstractSyndicationFormatter("html/text", _settingsService, _syndication);
            MemoryStream ms = new MemoryStream();
            IEnumerable<PostViewModel> posts = new List<PostViewModel>{ new PostViewModel
                                                                     {
                                                                         Subject = "Title",
                                                                         PostID = 1
                                                                     }};

            // Act
            formatter.WriteToStreamAsync(typeof(IEnumerable<PostViewModel>), posts, ms, null, null).Wait();

            // Assert
            _syndication.AssertWasCalled(x => x.BuildFeed(posts, "My blog", "http://localhost/mypage"));
            Assert.That(formatter.WrittenToFeed, Is.True);
        }
    }
}
