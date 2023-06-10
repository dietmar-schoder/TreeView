namespace TreeView.Tree
{
    public class TreePanel : ViewElement, ITreePanel
    {
        public List<TreeElement> TreeElements { get; set; } = new();

        public TreePanel() { }

        public void Calculate(TreeElement rootElement, int boxWidth, int boxHeight, int margin)
        {
            var structureBox = AddVerticalBox();
            CreateViewElements(rootElement, structureBox);
            CalculateSizesAndXYs();
            CenterHorizontalParents(rootElement);
            AddConnections(rootElement);
            Tree2List(rootElement);

            void CreateViewElements(TreeElement element, ViewElement parentBox)
            {
                var branchBox = element.HasChildren ? parentBox.AddVerticalBox() : parentBox;
                element.ViewElement = new ViewElement(boxWidth, boxHeight);
                branchBox.AddElement(element.ViewElement);
                if (element.HasChildren)
                {
                    var childrenBox = element.ChildrenAreVertical ? branchBox.AddVerticalBox() : branchBox.AddHorizontalBox();
                    childrenBox.LeftMargin = element is not null && element.ChildrenAreVertical ? margin : 0;
                    element.Children.ForEach(child => CreateViewElements(child, childrenBox));
                }
            }

            void CenterHorizontalParents(TreeElement element)
            {
                if (element.HasMoreThanOneHorizontalChildren)
                {
                    if (element.Children.Count % 2 == 0)
                    {
                        var index = element.Children.Count / 2 - 1;
                        var rightX = element.Children[index + 1].ViewElement.X;
                        var leftX = element.Children[index].ViewElement.X;
                        element.ViewElement.X = leftX + (rightX - leftX) / 2;
                    }
                    else
                    {
                        element.ViewElement.X = element.Children[(element.Children.Count - 1) / 2].ViewElement.X;
                    }
                }

                element.Children.ForEach(child => CenterHorizontalParents(child));
            }

            void AddConnections(TreeElement element)
            {
                var w2 = boxWidth / 2;
                var h = boxHeight;
                var h2 = boxHeight / 2;
                var m2 = margin / 2;
                if (element.HasChildren)
                {
                    if (element.ChildrenAreVertical)
                    {
                        element.AddVerticalConnection(element.ViewElement.X + m2, element.ViewElement.Y + h, element.Children.Last().ViewElement.Y + h2);
                    }
                    else
                    {
                        if (element.Children.Count > 1)
                        {
                            element.AddVerticalConnection(element.ViewElement.X + w2, element.ViewElement.Y + h, element.ViewElement.Y + h + m2);
                            element.AddHorizontalConnection(element.Children.First().ViewElement.X + w2, element.Children.Last().ViewElement.X + w2, element.Children.First().ViewElement.Y - m2);
                        }
                    }

                    element.Children.ForEach(child =>
                    {
                        if (element.ChildrenAreVertical)
                        {
                            element.AddHorizontalConnection(element.ViewElement.X + m2, child.ViewElement.X, child.ViewElement.Y + h2);
                        }
                        else
                        {
                            element.AddVerticalConnection(child.ViewElement.X + w2, child.ViewElement.Y, child.ViewElement.Y - (element.Children.Count == 1 ? margin : m2));
                        }
                    });

                    element.Children.ForEach(child => AddConnections(child));
                }
            }

            void Tree2List(TreeElement treeElement)
            {
                TreeElements.Add(treeElement);
                treeElement.Children.ForEach(child => Tree2List(child));
            }
        }
    }
}
