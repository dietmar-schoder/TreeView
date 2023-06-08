namespace TreeView.Tree
{
    public class PageViewElement : ViewElement
    {
        private readonly string _htmlTabTitle;

        public readonly bool _leavePageWarning;

        public PageViewElement(
            string htmlTabTitle,
            Orientation contentOrientation = Orientation.Vertical,
            Alignment alignment = Alignment.LeftTop,
            bool leavePageWarning = false)
            : base(contentOrientation, alignment)
        {
            _htmlTabTitle = htmlTabTitle;
            _leavePageWarning = leavePageWarning;
        }

        public virtual string ToHtml()
        {
            CalculateSizesAndXYs();
            return ToSvgTree;
        }

        public override string ToHtmlStart()
            => DoctypeHtmlHead(_htmlTabTitle) +
                $"<body style=\"padding:0;width:{Width}px;margin:0 auto;\"" +
                (_leavePageWarning ? " onbeforeunload=\"return 'leave'\"" : string.Empty) + 
                $"><svg width=\"{Width}\" height=\"{Height}\" viewBox=\"0 0 {Width} {Height}\" xmlns=\"http://www.w3.org/2000/svg\">";

        public override string ToHtmlEnd()
            => "</svg></body></html>";

        private string DoctypeHtmlHead(string htmlTabTitle)
            => $"<!DOCTYPE html><html style=\"{Constants.FONT_FAMILY}{Constants.FONT_SIZE}margin-left:calc(100vw - 100%);\">" +
                $"<head><title>{htmlTabTitle}</title><meta charset=\"utf-8\"></head>";
    }
}
