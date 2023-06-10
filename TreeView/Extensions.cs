using System.Net.Mime;
using System.Text;

namespace TreeView
{
    public static class Extensions
    {
        public static IResult HtmlResponse(this IResultExtensions extensions, string html)
            => new HtmlContent(html);
    }

    public class HtmlContent : IResult
    {
        private readonly string _htmlContent;

        public HtmlContent(string htmlContent) => _htmlContent = htmlContent;

        public async Task ExecuteAsync(HttpContext httpContext)
        {
            httpContext.Response.ContentType = MediaTypeNames.Text.Html;
            httpContext.Response.ContentLength = Encoding.UTF8.GetByteCount(_htmlContent);
            await httpContext.Response.WriteAsync(_htmlContent);
        }
    }
}
