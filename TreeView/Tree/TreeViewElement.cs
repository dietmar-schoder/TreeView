namespace TreeView.Tree
{
    public class TreeViewElement : PageViewElement
    {
        private TreeElement _tree { get; set; }

        public TreeViewElement(
            TreeElement tree,
            string htmlTabTitle,
            Orientation contentOrientation = Orientation.Vertical,
            Alignment alignment = Alignment.LeftTop,
            bool leavePageWarning = false)
            : base(htmlTabTitle, contentOrientation, alignment, leavePageWarning)
        {
            _tree = tree;
        }

        public override string ToHtml()
        {
            CalculateSizesAndXYs();
            CenterHorizontalParents(_tree);
            AddConnections(_tree);
            return ToSvgTree;
        }
    }
}
