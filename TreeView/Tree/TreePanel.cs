namespace TreeView.Tree
{
    public class TreePanel
    {
        public List<TreeElement> TreeElements { get; set; } = new();

        public List<TreeElementConnection> TreeElementConnections { get; set; } = new();

        private readonly int _boxWidth;

        private readonly int _boxHeight;

        private readonly int _margin;

        private TreePanel(MyTreeElement rootElement, int boxWidth, int boxHeight, int margin)
        {
            (_boxWidth, _boxHeight, _margin) = (boxWidth, boxHeight, margin);
            //------------
            var page = new TreeViewElement(rootElement, "Schoder.Tree Example");
            var structureBox = page.AddVerticalBox();
            AddParentAndChildren(rootElement, structureBox);

            void AddParentAndChildren(MyTreeElement treeElement, ViewElement parentBox)
            {
                var branchBox = treeElement.HasChildren ? parentBox.AddVerticalBox() : parentBox;

                //treeElement.ViewElement = new HyperlinkViewElement(treeElement.Number);
                // -> create SVG in MyTreeElement

                treeElement.ViewElement = new ViewElement { Width = _boxWidth, Height = _boxHeight };
                branchBox.AddElement(treeElement.ViewElement);

                if (treeElement.HasChildren)
                {
                    var childrenBox = treeElement.ChildrenAreVertical ? branchBox.AddVerticalBox() : branchBox.AddHorizontalBox();
                    childrenBox.LeftMargin = treeElement is not null && treeElement.ChildrenAreVertical ? _margin : 0;
                    foreach (MyTreeElement child in treeElement.Children)
                    {
                        AddParentAndChildren(child, childrenBox);
                    }
                }
            }
            //------------
            page.CalculateSizesAndXYs();
            CenterHorizontalParents(rootElement);
            AddConnections(rootElement);

            Tree2Lists(rootElement);

            void CenterHorizontalParents(MyTreeElement element)
            {
                if (element.HasChildren && element.Children.Count > 1 && !element.ChildrenAreVertical)
                {
                    if (element.Children.Count % 2 == 0)
                    {
                        var index = (int)(element.Children.Count / 2 - 1);
                        var rightX = element.Children[index + 1].ViewElement.X;
                        var leftX = element.Children[index].ViewElement.X;
                        element.ViewElement.X = leftX + (int)((rightX - leftX) / 2);
                    }
                    else
                    {
                        element.ViewElement.X = element.Children[(int)(element.Children.Count - 1) / 2].ViewElement.X;
                    }
                }

                if (element.HasChildren)
                {
                    foreach (MyTreeElement child in element.Children)
                    {
                        CenterHorizontalParents(child);
                    }
                }
            }

            void AddConnections(TreeElement element)
            {
                if (element.HasChildren)
                {
                    //page.AddElement(new StructLineViewElement(element));
                    // -> element.TreeElementConnections.Add(new StructLineViewElement(element));
                    element.Children.ForEach(child => AddConnections(child));
                }
            }

            void Tree2Lists(TreeElement treeElement)
            {
                TreeElements.Add(treeElement);
                TreeElementConnections.AddRange(treeElement.TreeElementConnections);
                if (treeElement.HasChildren)
                {
                    treeElement.Children.ForEach(child => { Tree2Lists(child); });
                }
            }
        }

        public static TreePanel Create(MyTreeElement rootElement, int boxWidth, int boxHeight, int margin)
            => new TreePanel(rootElement, boxWidth, boxHeight, margin);
    }
}
