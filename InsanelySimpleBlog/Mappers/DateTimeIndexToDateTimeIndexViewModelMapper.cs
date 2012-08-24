using InsanelySimpleBlog.DataModel;
using InsanelySimpleBlog.System.Mappers;
using InsanelySimpleBlog.ViewModel;

namespace InsanelySimpleBlog.Mappers
{
    internal class DateTimeIndexToDateTimeIndexViewModelMapper : IMapper<DateTimeIndex, DateTimeIndexViewModel>
    {
        public DateTimeIndexViewModel Map(DateTimeIndex @from)
        {
            return new DateTimeIndexViewModel
                       {
                           EndDate = @from.EndDate,
                           NumberOfPosts = @from.NumberOfPosts,
                           StartDate = @from.StartDate
                       };
        }
    }
}
