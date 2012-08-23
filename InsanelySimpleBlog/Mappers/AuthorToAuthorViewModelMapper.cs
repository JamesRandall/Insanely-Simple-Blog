using CuttingEdge.Conditions;
using InsanelySimpleBlog.DataModel;
using InsanelySimpleBlog.System.Mappers;
using InsanelySimpleBlog.ViewModel;

namespace InsanelySimpleBlog.Mappers
{
    class AuthorToAuthorViewModelMapper : IMapper<Author, AuthorViewModel>
    {
        public AuthorViewModel Map(Author @from)
        {
            Condition.Requires(@from, "@from").IsNotNull();

            return new AuthorViewModel
                       {
                           AuthorID = @from.AuthorID,
                           Name = @from.Name
                       };
        }
    }
}
