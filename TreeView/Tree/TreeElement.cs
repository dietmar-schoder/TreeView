namespace TreeView.Tree
{
    public class TreeElement
    {
        public TreeElement Parent { get; set; }

        public List<TreeElement> Children { get; set; } = new();

        public int Level { get; set; }

        public bool IsRoot => Parent is null;

        public bool HasChildren => Children.Count > 0;

        public int X { get; set; }

        public int Y { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

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
