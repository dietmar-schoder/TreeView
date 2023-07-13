using System.Text;

namespace TreeView
{
    public class TreeViewer : ITreeViewer
    {
        private readonly Tree.ITreePanel _treePanel;
        private readonly MyTreeElement _tree;

        public TreeViewer(Tree.ITreePanel treePanel)
            => (_treePanel, _tree) = (treePanel, new());

        public void GenerateTree(int numberOfChildrenPerParent, int numberOfLevels, MyTreeElement treeElement = null)
        {
            treeElement = treeElement ?? _tree;
            while (treeElement.Level < numberOfLevels - 1
                && treeElement.Children.Count < numberOfChildrenPerParent)
            {
                GenerateTree(numberOfChildrenPerParent, numberOfLevels, treeElement.NewChild());
            }
        }
        public void CalculateScreene(int boxWidth, int boxHeight, int margin)
            => _treePanel.Calculate(_tree, boxWidth, boxHeight, margin);

        public IResult GetHtmlSvg()
        {
            (var width, var height) = (_treePanel.Width, _treePanel.Height);
            var fontFamily = "font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Roboto,Helvetica Neue,Arial,Noto Sans,sans-serif;";
            var fontSize = "font-size:10px;";
            var html = new StringBuilder($"<!DOCTYPE html><html lang=\"en\" style=\"{fontFamily}{fontSize}margin-left:calc(100vw - 100%);\">" +
                $"<head><title>Schoder.Tree Example</title><meta charset=\"utf-8\"></head>" +
                $"<body style=\"padding:0;width:{width}px;margin:0 auto;\">" +
                $"<svg width=\"{width}\" height=\"{height}\"" +
                $" viewBox=\"0 0 {width} {height}\" xmlns=\"http://www.w3.org/2000/svg\">");

            _treePanel.TreeElements.OfType<MyTreeElement>().ToList().ForEach(e => html.Append(e.ToSvg()));

            return new HtmlContent(html.Append("</svg></body></html>").ToString());
        }
    }
}
