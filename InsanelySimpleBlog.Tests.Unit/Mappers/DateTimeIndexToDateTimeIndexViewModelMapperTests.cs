using System;
using InsanelySimpleBlog.DataModel;
using InsanelySimpleBlog.Mappers;
using InsanelySimpleBlog.ViewModel;
using NUnit.Framework;

namespace InsanelySimpleBlog.Tests.Unit.Mappers
{
    [TestFixture]
    public class DateTimeIndexToDateTimeIndexViewModelMapperTests
    {
        [Test]
        public void MapReturnsViewModel()
        {
            // Arrange
            DateTimeIndexToDateTimeIndexViewModelMapper mapper = new DateTimeIndexToDateTimeIndexViewModelMapper();
            DateTime referenceStartDate = DateTime.Now.Subtract(TimeSpan.FromDays(1));
            DateTime referenceEndDate = DateTime.Now; 
            DateTimeIndex index = new DateTimeIndex
                                      {
                                          DateTimeIndexID = 1,
                                          EndDate = referenceEndDate,
                                          NumberOfPosts = 2,
                                          StartDate = referenceStartDate
                                      };

            // Act
            DateTimeIndexViewModel result = mapper.Map(index);

            // Assert
            Assert.That(result.StartDate, Is.EqualTo(referenceStartDate));
            Assert.That(result.EndDate, Is.EqualTo(referenceEndDate));
            Assert.That(result.NumberOfPosts, Is.EqualTo(2));
        }
    }
}
