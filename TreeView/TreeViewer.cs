using TreeView.Tree;

namespace TreeView
{
    public class TreeViewer : ITreeViewer
    {
        public MyTreeElement GenerateTree(int numberOfChildren, int numberOfLevels)
        {
            var rootElement = new MyTreeElement(isRoot: true);
            GenerateChildren(rootElement);
            return rootElement;

            void GenerateChildren(MyTreeElement treeElement)
            {
                for (int i = 0; i < numberOfChildren; i++)
                {
                    var child = treeElement.AddChild(new MyTreeElement());
                    if (child.Level < numberOfLevels)
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
            // Draw result
            foreach (var element in treePanel.TreeElements)
            {
                // draw box
                var x = element.X;
                // ...
            }
            foreach (var connection in treePanel.TreeElementConnections)
            {
                // draw lines connecting boxes
                var x1 = connection.X1;
                // ...
            }

            return Results.Empty;
        }
    }
}
