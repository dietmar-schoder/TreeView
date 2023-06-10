namespace TreeView
{
    public class MyTreeViewer : IMyTreeViewer
    {
        private const string FONT_FAMILY = "font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Roboto,Helvetica Neue,Arial,Noto Sans,sans-serif,Apple Color Emoji,Segoe UI Emoji,Segoe UI Symbol,Noto Color Emoji;";
        private const string FONT_SIZE = "font-size:14px;";

        private readonly Tree.ITreePanel _treePanel;
        private MyTreeElement _tree = new();

        public MyTreeViewer(Tree.ITreePanel treePanel) => _treePanel = treePanel;

        public void GenerateTree(int numberOfChildren, int numberOfLevels)
        {
            GenerateChildren(_tree);

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

        public IResult GetHtml(int boxWidth, int boxHeight, int margin)
        {
            _treePanel.Calculate(_tree, boxWidth, boxHeight, margin);

            var html = $"<!DOCTYPE html><html style=\"{FONT_FAMILY}{FONT_SIZE}margin-left:calc(100vw - 100%);\">" +
                $"<head><title>Schoder.Tree Example</title><meta charset=\"utf-8\"></head>" +
                $"<body style=\"padding:0;width:{_treePanel.Width}px;margin:0 auto;\">" +
                $"<svg width=\"{_treePanel.Width}\" height=\"{_treePanel.Height}\"" +
                $" viewBox=\"0 0 {_treePanel.Width} {_treePanel.Height}\" xmlns=\"http://www.w3.org/2000/svg\">";

            foreach (MyTreeElement element in _treePanel.TreeElements)
            {
                html += element.ToSvg();
            }

            html += "</svg></body></html>";

            return new HtmlContent(html);
        }
    }
}
