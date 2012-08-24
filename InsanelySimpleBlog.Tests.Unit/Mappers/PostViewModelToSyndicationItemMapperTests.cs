using System;
using System.ServiceModel.Syndication;
using InsanelySimpleBlog.Mappers;
using InsanelySimpleBlog.ViewModel;
using NUnit.Framework;

namespace InsanelySimpleBlog.Tests.Unit.Mappers
{
    [TestFixture]
    public class PostViewModelToSyndicationItemMapperTests
    {
        [Test]
        public void MapReturnsSyndicationItem()
        {
            // Arrange
            Guid referenceGuid = Guid.NewGuid();
            DateTime referenceDate = DateTime.UtcNow;
            PostViewModel model = new PostViewModel
                                      {
                                          AuthorName = "Fred",
                                          BodyAsHtml = "Some content",
                                          ExternalIdentifier = referenceGuid,
                                          PostedAt = referenceDate,
                                          PostID = 1,
                                          Subject = "My subject"
                                      };
            PostViewModelToSyndicationItemMapper mapper = new PostViewModelToSyndicationItemMapper();

            // Act
            SyndicationItem result = mapper.Map(model, "http://localhost");

            // Assert
            Assert.That(result.BaseUri, Is.EqualTo(new Uri("http://localhost/#/posts/1")));
            Assert.That(((TextSyndicationContent)result.Content).Text, Is.EqualTo("Some content"));
            Assert.That(result.Id, Is.EqualTo(referenceGuid.ToString()));
            Assert.That(result.LastUpdatedTime.UtcDateTime, Is.EqualTo(referenceDate));
            Assert.That(((TextSyndicationContent)result.Title).Text, Is.EqualTo("My subject"));
            Assert.That(result.Authors.Count, Is.EqualTo(1));
            Assert.That(((SyndicationPerson)result.Authors[0]).Name, Is.EqualTo("Fred"));
        }
    }
}
