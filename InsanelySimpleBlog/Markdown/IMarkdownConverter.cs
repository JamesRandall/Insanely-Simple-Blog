namespace InsanelySimpleBlog.Markdown
{
    interface IMarkdownConverter
    {
        string ToHtml(string markdown);
    }
}
