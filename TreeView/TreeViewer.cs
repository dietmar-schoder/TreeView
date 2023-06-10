namespace TreeView
{
    public class TreeViewer : ITreeViewer
    {
        private const string FONT_FAMILY = "font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Roboto,Helvetica Neue,Arial,Noto Sans,sans-serif,Apple Color Emoji,Segoe UI Emoji,Segoe UI Symbol,Noto Color Emoji;";
        private const string FONT_SIZE = "font-size:14px;";

        public MyTreeElement GenerateTree(int numberOfChildren, int numberOfLevels)
        {
            var rootElement = new MyTreeElement();
            GenerateChildren(rootElement);
            return rootElement;

            void GenerateChildren(MyTreeElement treeElement)
            {
                for (int i = 0; i < numberOfChildren; i++)
                {
                    var child = new MyTreeElement(parent: treeElement);
                    if (child.Level < numberOfLevels - 1)
                    {
                        GenerateChildren(child);
                    }
                }
            }
        }

        public IResult GetHtml(Tree.TreePanel treePanel)
        {
            var html = $"<!DOCTYPE html><html style=\"{FONT_FAMILY}{FONT_SIZE}margin-left:calc(100vw - 100%);\">" +
                $"<head><title>Schoder.Tree Example</title><meta charset=\"utf-8\"></head>" +
                $"<body style=\"padding:0;width:{treePanel.Width}px;margin:0 auto;\">" +
                $"<svg width=\"{treePanel.Width}\" height=\"{treePanel.Height}\"" +
                $" viewBox=\"0 0 {treePanel.Width} {treePanel.Height}\" xmlns=\"http://www.w3.org/2000/svg\">";

            foreach (MyTreeElement element in treePanel.TreeElements)
            {
                html += element.ToSvg();
            }

            html += "</svg></body></html>";

            return new HtmlContent(html);
        }
    }
}
