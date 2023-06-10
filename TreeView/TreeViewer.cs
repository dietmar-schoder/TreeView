using TreeView.Tree;
using Microsoft.AspNetCore.Mvc;

namespace TreeView
{
    public class TreeViewer : ITreeViewer
    {
        public MyTreeElement GenerateTree(int numberOfChildren, int numberOfLevels)
        {
            var rootElement = new MyTreeElement();
            rootElement.Width = 180;
            rootElement.Height = (int)(rootElement.Width / 1.61803398875);
            GenerateChildren(rootElement);
            return rootElement;

            void GenerateChildren(MyTreeElement treeElement)
            {
                for (int i = 0; i < numberOfChildren; i++)
                {
                    var child = treeElement.NewChild();
                    if (child.Level < numberOfLevels - 1)
                    {
                        GenerateChildren(child);
                    }
                }
            }
        }

        public TreePanel CreateTreePanel(MyTreeElement tree, int boxWidth, int boxHeight, int margin)
            => TreePanel.Create(tree, boxWidth, boxHeight, margin);

        public IResult GetHtml(TreePanel treePanel)
        {
            var html = DoctypeHtmlHead("Schoder.Tree Example") +
                $"<body style=\"padding:0;width:{treePanel.Width}px;margin:0 auto;\">" +
                $"<svg width=\"{treePanel.Width}\" height=\"{treePanel.Height}\"" +
                $" viewBox=\"0 0 {treePanel.Width} {treePanel.Height}\" xmlns=\"http://www.w3.org/2000/svg\">";

            // Draw result
            foreach (MyTreeElement element in treePanel.TreeElements)
            {
                // draw box
                html += element.ToSvg;
                foreach (var connection in element.TreeElementConnections)
                {
                    // draw lines connecting boxes
                    html += connection.ToSvg;
                }
            }

            html += "</svg></body></html>";

            return new HtmlContent(html);

            string DoctypeHtmlHead(string htmlTabTitle)
                => $"<!DOCTYPE html><html style=\"{Constants.FONT_FAMILY}{Constants.FONT_SIZE}margin-left:calc(100vw - 100%);\">" +
                    $"<head><title>{htmlTabTitle}</title><meta charset=\"utf-8\"></head>";
        }
    }
}
