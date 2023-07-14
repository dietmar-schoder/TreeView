using System.Text;

namespace TreeView
{
    public class ListViewer : IListViewer
    {
        private readonly Tree.ITreePanel _treePanel;
        private readonly MyTreeElement _tree;

        public ListViewer(Tree.ITreePanel treePanel)
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

        public void CalculateScreen() => _treePanel.Calculate(_tree, 0, 0, 0);

        public IResult GetHtmlSvg(int margin, int lineHeight, int boxHeight)
        {
            (var width, var height) = (800, _treePanel.TreeElements.Count * lineHeight + margin + margin);
            var fontFamily = "font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Roboto,Helvetica Neue,Arial,Noto Sans,sans-serif;";
            var fontSize = "font-size:10px;";
            var html = new StringBuilder($"<!DOCTYPE html><html lang=\"en\" style=\"{fontFamily}{fontSize}margin-left:calc(100vw - 100%);\">" +
                $"<head><title>Schoder.Tree Example</title><meta charset=\"utf-8\"></head>" +
                $"<body style=\"padding:0;width:{width}px;margin:0 auto;\">" +
                $"<svg width=\"{width}\" height=\"{height}\"" +
                $" viewBox=\"0 0 {width} {height}\" xmlns=\"http://www.w3.org/2000/svg\">");

            var i = 0;
            foreach (var element in _treePanel.TreeElements.OfType<MyTreeElement>())
            {
                html.Append(element.ToListSvg(i++, margin, lineHeight, boxHeight));
            }

            return new HtmlContent(html.Append("</svg></body></html>").ToString());
        }
    }
}
