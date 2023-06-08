namespace TreeView.Tree
{
    public class StructLineViewElement : ViewElement
    {
        private TreeElement _parent { get; set; }

        public StructLineViewElement(TreeElement parent) : base()
        {
            _parent = parent;
            Position = Position.Absolute;
        }

        public override string ToHtmlStart()
        {
            var svg = string.Empty;

            if (_parent.ChildrenAreVertical)
            {
                svg += VerticalLine(_parent.ViewElement.X + 10, _parent.ViewElement.Y + 111, _parent.Children.Last().ViewElement.Y + 56);
            }
            else
            {
                if (_parent.Children.Count > 1)
                {
                    svg += VerticalLine(_parent.ViewElement.X + 90, _parent.ViewElement.Y + 111, _parent.ViewElement.Y + 111 + 10);
                    svg += HorizontalLine(_parent.Children.First().ViewElement.X + 90, _parent.Children.Last().ViewElement.X + 90, _parent.Children.First().ViewElement.Y - 10);
                }
            }

            foreach (var child in _parent.Children)
            {
                if (_parent.ChildrenAreVertical)
                {
                    svg += HorizontalLine(_parent.ViewElement.X + 10, child.ViewElement.X, child.ViewElement.Y + 56);
                }
                else
                {
                    svg += VerticalLine(child.ViewElement.X + 90, child.ViewElement.Y, child.ViewElement.Y - (_parent.Children.Count == 1 ? 20 : 10));
                }
            }

            return svg;

            string HorizontalLine(int x1, int x2, int y)
                => $"<line x1=\"{x1}\" y1=\"{y}\" x2=\"{x2} \" y2=\" {y}\"" +
                    $" style=\"stroke-width:1px;stroke:rgb(0,0,0)\" />";

            string VerticalLine(int x, int y1, int y2)
                => $"<line x1=\"{x}\" y1=\"{y1}\" x2=\"{x} \" y2=\" {y2}\"" +
                    $" style=\"stroke-width:1px;stroke:rgb(0,0,0)\" />";
        }
    }
}
