namespace TreeView.Tree
{
    public static class Extensions
    {
        public static IResult HtmlResponse(this IResultExtensions extensions, string html)
            => new HtmlContent(html);
    }
}
