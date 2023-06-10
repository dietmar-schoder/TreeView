namespace TreeView.Tree
{
    public class TreeElement
    {
        public TreeElement Parent { get; set; }

        public List<TreeElement> Children { get; set; } = new();

        public int Level { get; set; }

        public bool IsRoot => Parent is null;

        public bool HasChildren => Children.Count > 0;

        public bool HasMoreThanOneHorizontalChildren => HasChildren && Children.Count > 1 && !ChildrenAreVertical;

        public bool ChildrenAreVertical => Level % 2 == 1;

        public ViewElement ViewElement { get; set; }

        public List<TreeElementConnection> TreeElementConnections { get; set; } = new();

        public void AddVerticalConnection(int x, int y1, int y2)
            => TreeElementConnections.Add(new TreeElementConnection(x, y1, x, y2));

        public void AddHorizontalConnection(int x1, int x2, int y)
            => TreeElementConnections.Add(new TreeElementConnection(x1, y, x2, y));

        public TreeElement AddAsChild(TreeElement element)
        {
            element.Parent = this;
            element.Level = Level + 1;
            Children.Add(element);
            return element;
        }
    }
}
