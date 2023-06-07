namespace TreeView.Tree
{
    public class TreePanel
    {
        public List<TreeElement> TreeElements { get; set; } = new();

        public List<TreeElementConnection> TreeElementConnections { get; set; } = new();

        private readonly int _boxWidth;

        private readonly int _boxHeight;

        private readonly int _margin;

        private TreePanel(TreeElement rootElement, int boxWidth, int boxHeight, int margin)
        {
            (_boxWidth, _boxHeight, _margin) = (boxWidth, boxHeight, margin);

            CalculateSizes(rootElement);
            CalculatePositions(rootElement);
            CalculateConnections(rootElement);
            Tree2Lists(rootElement);

            void CalculateSizes(TreeElement treeElement)
            {
                // ...
                treeElement.Children.ForEach(child => { CalculateSizes(child); });
            }

            void CalculatePositions(TreeElement treeElement)
            {
                // ...
                treeElement.Children.ForEach(child => { CalculatePositions(child); });
            }

            void CalculateConnections(TreeElement treeElement)
            {
                // ...
                treeElement.Children.ForEach(child => { CalculateConnections(child); });
            }

            void Tree2Lists(TreeElement treeElement)
            {
                TreeElements.Add(treeElement);
                TreeElementConnections.AddRange(treeElement.TreeElementConnections);
                treeElement.Children.ForEach(child => { Tree2Lists(child); });
            }
        }

        public static TreePanel Create(TreeElement rootElement, int boxWidth, int boxHeight, int margin)
            => new TreePanel(rootElement, boxWidth, boxHeight, margin);
    }
}
