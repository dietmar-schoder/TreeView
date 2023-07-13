using System.Text;

namespace TreeView
{
    public class TreeViewer : ITreeViewer
    {
        private const string FONT_FAMILY = "font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Roboto,Helvetica Neue,Arial,Noto Sans,sans-serif;";
        private const string FONT_SIZE = "font-size:10px;";

        private readonly Tree.ITreePanel _treePanel;
        private readonly MyTreeElement _tree = new();

        public TreeViewer(Tree.ITreePanel treePanel) => _treePanel = treePanel;

        public void GenerateTree(int numberOfChildren, int numberOfLevels, MyTreeElement treeElement = null)
        {
            treeElement = treeElement ?? _tree;
            if (treeElement.Level >= numberOfLevels - 1) { return; }
            for (int i = 0; i < numberOfChildren; i++)
            {
                GenerateTree(numberOfChildren, numberOfLevels, new MyTreeElement(parent: treeElement));
            }
        }

        public IResult GetHtml(int boxWidth, int boxHeight, int margin)
        {
            _treePanel.Calculate(_tree, boxWidth, boxHeight, margin);

            (var width, var height) = (_treePanel.Width, _treePanel.Height);
            var html = new StringBuilder($"<!DOCTYPE html><html lang=\"en\" style=\"{FONT_FAMILY}{FONT_SIZE}margin-left:calc(100vw - 100%);\">" +
                $"<head><title>Schoder.Tree Example</title><meta charset=\"utf-8\"></head>" +
                $"<body style=\"padding:0;width:{width}px;margin:0 auto;\">" +
                $"<svg width=\"{width}\" height=\"{height}\"" +
                $" viewBox=\"0 0 {width} {height}\" xmlns=\"http://www.w3.org/2000/svg\">");

            _treePanel.TreeElements.OfType<MyTreeElement>().ToList().ForEach(e => html.Append(e.ToSvg()));

            return new HtmlContent(html.Append("</svg></body></html>").ToString());
        }
    }
}
