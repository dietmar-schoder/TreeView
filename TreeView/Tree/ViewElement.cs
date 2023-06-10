namespace TreeView.Tree
{
    public class ViewElement
    {
        public int X { get; set; }

        public int Y { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public int LeftMargin { get; set; }

        public int TopMargin { get; set; }

        public ViewElement ParentViewElement { get; set; }

        public List<ViewElement> ViewElements { get; set; }

        public Orientation ContentOrientation { get; set; }

        public bool AddMargins => ViewElements == null;

        public ViewElement(Orientation contentOrientation = Orientation.Horizontal, int topMargin = 0)
            => (ContentOrientation, TopMargin) = (contentOrientation, topMargin);

        public ViewElement() { }

        public ViewElement(int width, int height)
            => (Width, Height) = (width, height);

        private ViewElement AddContainer(
            Orientation contentOrientation = Orientation.Horizontal,
            int topMargin = 0)
            => AddViewElement(this, new ViewElement(contentOrientation, topMargin));

        public ViewElement AddHorizontalBox(int topMargin = 0)
            => AddContainer(Orientation.Horizontal, topMargin);

        public ViewElement AddVerticalBox(int topMargin = 0)
            => AddContainer(Orientation.Vertical, topMargin);

        public ViewElement AddElement(ViewElement viewElement)
        {
            AddViewElement(this, viewElement);
            return this;
        }

        public ViewElement CalculateSizesAndXYs()
        {
            CalculateSizes();
            Width += Constants.SVG_MARGIN;
            Height += Constants.SVG_MARGIN;
            CalculateXYs();
            return this;
        }

        private ViewElement AddViewElement(ViewElement parentViewElement, ViewElement viewElement)
        {
            viewElement.ParentViewElement = parentViewElement;
            ViewElements = ViewElements ?? new();
            ViewElements.Add(viewElement);
            return viewElement;
        }

        private void CalculateSizes()
        {
            if (ViewElements == null) { return; }
            if (ContentOrientation == Orientation.Horizontal)
            {
                CalculateSizeHorizontal();
            }
            if (ContentOrientation == Orientation.Vertical)
            {
                CalculateSizeVertical();
            }
        }

        private void CalculateSizeHorizontal()
        {
            ViewElements.ForEach(e =>
            {
                e.CalculateSizes();
                Width += e.LeftMargin + e.Width + (e.AddMargins ? Constants.SVG_MARGIN : 0);
                Height = Math.Max(Height, (e.AddMargins ? Constants.SVG_MARGIN : 0) + e.TopMargin + e.Height);
            });
        }

        private void CalculateSizeVertical()
        {
            ViewElements.ForEach(e =>
            {
                e.CalculateSizes();
                Height += e.TopMargin + e.Height + (e.AddMargins ? Constants.SVG_MARGIN : 0);
                Width = Math.Max(Width, (e.AddMargins ? Constants.SVG_MARGIN : 0) + e.LeftMargin + e.Width);
            });
        }

        private void CalculateXYs()
        {
            if (ViewElements == null) { return; }
            if (ContentOrientation == Orientation.Horizontal)
            {
                CalculateXYsHorizontal();
            }
            if (ContentOrientation == Orientation.Vertical)
            {
                CalculateXYsVertical();
            }
        }

        private void CalculateXYsHorizontal()
        {
            var xIncrement = X;
            ViewElements.ForEach(e =>
            {
                e.X = xIncrement + e.LeftMargin + (e.AddMargins ? Constants.SVG_MARGIN : 0);
                e.Y = e.TopMargin + Y + (e.AddMargins ? Constants.SVG_MARGIN : 0);
                xIncrement = e.X + e.Width - e.LeftMargin;
                e.CalculateXYs();
            });
        }

        private void CalculateXYsVertical()
        {
            var yIncrement = Y;
            ViewElements.ForEach(e =>
            {
                e.X = e.LeftMargin + X + (e.AddMargins ? Constants.SVG_MARGIN : 0);
                e.Y = yIncrement + e.TopMargin + (e.AddMargins ? Constants.SVG_MARGIN : 0);
                yIncrement = e.Y + e.Height;
                e.CalculateXYs();
            });
        }
    }
}
