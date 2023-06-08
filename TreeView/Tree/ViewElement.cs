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

        public Alignment Alignment { get; set; }

        public Position Position { get; set; }

        public string Colour { get; set; }

        public string ToSvgTree
        {
            get
            {
                var svg = ToHtmlStart();
                if (ViewElements is not null)
                ViewElements.ForEach(e => { svg += e.ToSvgTree; });
                svg += ToHtmlEnd();
                return svg;
            }
        }

        public bool AddMargins => ViewElements == null;

        public ViewElement(
            Orientation contentOrientation = Orientation.Horizontal,
            Alignment alignment = Alignment.LeftTop,
            int topMargin = 0)
        {
            ContentOrientation = contentOrientation;
            Alignment = alignment;
            TopMargin = topMargin;
        }

        public ViewElement() { }

        public ViewElement(int width, int height)
        {
            Width = width;
            Height = height;
        }

        private ViewElement AddContainer(
            Orientation contentOrientation = Orientation.Horizontal,
            Alignment alignment = Alignment.LeftTop,
            int topMargin = 0)
            => AddViewElement(this, new ViewElement(contentOrientation, alignment, topMargin));

        public ViewElement AddHorizontalBox(Alignment alignment = Alignment.LeftTop, int topMargin = 0)
            => AddContainer(Orientation.Horizontal, alignment, topMargin);

        public ViewElement AddVerticalBox(Alignment alignment = Alignment.LeftTop, int topMargin = 0)
            => AddContainer(Orientation.Vertical, alignment, topMargin);

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

        public virtual string ToHtmlStart() => string.Empty;
        //{
        //    //For testing:
        //    return $"<rect x=\"{X}\" y=\"{Y}\"" +
        //    $" width=\"{Width}\"" +
        //    $" height=\"{Height}\"" +
        //    $" style=\"fill:transparent;stroke-width:1;stroke:rgb(196,196,196)\" />";
        //}

        public virtual string ToHtmlEnd() => string.Empty;

        public virtual void AdaptWidthCenter()
            => Width = Alignment == Alignment.Center
                ? ParentViewElement.Width - LeftMargin - (AddMargins ? Constants.SVG_MARGIN : 0)
                : Width;

        public virtual void AdaptHeightCenter() { }

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
            ViewElements.Where(e => e.Position == Position.Relative).ToList().ForEach(e =>
            {
                e.CalculateSizes();
                Width += e.LeftMargin + e.Width + (e.AddMargins ? Constants.SVG_MARGIN : 0);
                Height = Math.Max(Height, (e.AddMargins ? Constants.SVG_MARGIN : 0) + e.TopMargin + e.Height);
            });
        }

        private void CalculateSizeVertical()
        {
            ViewElements.Where(e => e.Position == Position.Relative).ToList().ForEach(e =>
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
            ViewElements.Where(e => e.Position == Position.Relative).ToList().ForEach(e =>
            {
                e.X = xIncrement + e.LeftMargin + (e.AddMargins ? Constants.SVG_MARGIN : 0);
                e.Y = e.TopMargin + Y + (e.AddMargins ? Constants.SVG_MARGIN : 0);
                e.AdaptHeightCenter();
                xIncrement = e.X + e.Width - e.LeftMargin;
                e.CalculateXYs();
            });
        }

        private void CalculateXYsVertical()
        {
            var yIncrement = Y;
            ViewElements.Where(e => e.Position == Position.Relative).ToList().ForEach(e =>
            {
                e.X = e.LeftMargin + X + (e.AddMargins ? Constants.SVG_MARGIN : 0);
                e.X += Alignment == Alignment.Center ? (Width - e.Width - e.LeftMargin - (e.AddMargins ? Constants.SVG_MARGIN : 0)) / 2 : 0;
                e.Y = yIncrement + e.TopMargin + (e.AddMargins ? Constants.SVG_MARGIN : 0);
                e.AdaptWidthCenter();
                yIncrement = e.Y + e.Height;
                e.CalculateXYs();
            });
        }
    }
}
