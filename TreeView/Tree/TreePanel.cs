namespace TreeView.Tree
{
    public class TreePanel : ViewElement
    {
        public List<TreeElement> TreeElements { get; set; } = new();

        private readonly int _boxWidth;

        private readonly int _boxHeight;

        private readonly int _margin;

        private TreePanel(MyTreeElement rootElement, int boxWidth, int boxHeight, int margin)
            : base()
        {
            (_boxWidth, _boxHeight, _margin) = (boxWidth, boxHeight, margin);

            var structureBox = AddVerticalBox();
            AddParentAndChildren(rootElement, structureBox);
            CalculateSizesAndXYs();
            CenterHorizontalParents(rootElement);
            AddConnections(rootElement);
            Tree2List(rootElement);

            void AddParentAndChildren(MyTreeElement treeElement, ViewElement parentBox)
            {
                var branchBox = treeElement.HasChildren ? parentBox.AddVerticalBox() : parentBox;
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

            void CenterHorizontalParents(MyTreeElement element)
            {
                if (element.HasMoreThanOneHorizontalChildren)
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

                foreach (MyTreeElement child in element.Children)
                {
                    CenterHorizontalParents(child);
                }
            }

            void AddConnections(TreeElement element)
            {
                if (element.HasChildren)
                {
                    if (element.ChildrenAreVertical)
                    {
                        element.AddConnection(StructureLine.Vertical(element.ViewElement.X + 10, element.ViewElement.Y + 111, element.Children.Last().ViewElement.Y + 56));
                    }
                    else
                    {
                        if (element.Children.Count > 1)
                        {
                            element.AddConnection(StructureLine.Vertical(element.ViewElement.X + 90, element.ViewElement.Y + 111, element.ViewElement.Y + 111 + 10));
                            element.AddConnection(StructureLine.Horizontal(element.Children.First().ViewElement.X + 90, element.Children.Last().ViewElement.X + 90, element.Children.First().ViewElement.Y - 10));
                        }
                    }

                    foreach (var child in element.Children)
                    {
                        if (element.ChildrenAreVertical)
                        {
                            element.AddConnection(StructureLine.Horizontal(element.ViewElement.X + 10, child.ViewElement.X, child.ViewElement.Y + 56));
                        }
                        else
                        {
                            element.AddConnection(StructureLine.Vertical(child.ViewElement.X + 90, child.ViewElement.Y, child.ViewElement.Y - (element.Children.Count == 1 ? 20 : 10)));
                        }
                    }

                    element.Children.ForEach(child => AddConnections(child));
                }
            }

            void Tree2List(TreeElement treeElement)
            {
                TreeElements.Add(treeElement);
                treeElement.Children.ForEach(child => { Tree2List(child); });
            }
        }

        public static TreePanel Create(MyTreeElement rootElement, int boxWidth, int boxHeight, int margin)
            => new TreePanel(rootElement, boxWidth, boxHeight, margin);
    }
}
