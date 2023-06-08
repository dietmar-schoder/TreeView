namespace TreeView.Tree
{
    public class TreeElement
    {
        public TreeElement Parent { get; set; }

        public List<TreeElement> Children { get; set; } = new();

        public int Level { get; set; }

        public bool IsRoot => Parent is null;

        public bool HasChildren => Children.Count > 0;

        public bool ChildrenAreVertical => Level % 2 == 1;

        public ViewElement ViewElement { get; set; }

        public List<TreeElementConnection> TreeElementConnections { get; set; } = new();

        public TreeElement AddAsChild(TreeElement element)
        {
            element.Parent = this;
            element.Level = Level + 1;
            Children.Add(element);
            return element;
        }
    }
}
