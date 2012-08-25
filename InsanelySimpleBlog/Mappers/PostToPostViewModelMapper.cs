using System.Linq;
using CuttingEdge.Conditions;
using InsanelySimpleBlog.DataModel;
using InsanelySimpleBlog.Markdown;
using InsanelySimpleBlog.Markdown.Implementation;
using InsanelySimpleBlog.System.Mappers;
using InsanelySimpleBlog.ViewModel;

namespace InsanelySimpleBlog.Mappers
{
    class PostToPostViewModelMapper : IMapper<Post, PostViewModel>
    {
        private readonly IMapper<Category, CategoryViewModel> _categoryMapper;
        private readonly IMarkdownConverter _markdownConverter;

        public PostToPostViewModelMapper() : this(new CategoryToCategoryViewModelMapper(), new MarkdownConverter())
        {
            
        }

        public PostToPostViewModelMapper(
            IMapper<Category, CategoryViewModel> categoryMapper,
            IMarkdownConverter markdownConverter)
        {
            Condition.Requires(categoryMapper, "categoryMapper").IsNotNull();
            Condition.Requires(markdownConverter, "markdownConverter").IsNotNull();

            _categoryMapper = categoryMapper;
            _markdownConverter = markdownConverter;
        }

        public PostViewModel Map(Post @from)
        {
            Condition.Requires(@from, "@from").IsNotNull();
            Condition.Requires(@from.Categories, "@from.Categories").IsNotNull();
            Condition.Requires(@from.Author, "@from.Author").IsNotNull();

            return new PostViewModel
                       {
                           AuthorId = @from.Author.AuthorID,
                           AuthorName = @from.Author.Name,
                           BodyAsHtml = _markdownConverter.ToHtml(@from.Body),
                           Categories = @from.Categories.Select(x => _categoryMapper.Map(x)).ToList(),
                           ExternalIdentifier = @from.ExternalIdentifier,
                           PostID = @from.PostID,
                           PostedAt = @from.PostedAt,
                           Subject = @from.Subject
                       };
        }
    }
}
