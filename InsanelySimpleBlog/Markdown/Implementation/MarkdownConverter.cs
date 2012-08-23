namespace InsanelySimpleBlog.Markdown.Implementation
{
    class MarkdownConverter : IMarkdownConverter
    {
        private readonly MarkdownSharp.Markdown _markdown;
        
        public MarkdownConverter()
        {
            _markdown = new MarkdownSharp.Markdown();
        }

        public string ToHtml(string source)
        {
            return _markdown.Transform(source);
        }
    }
}
