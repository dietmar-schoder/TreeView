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

        public ViewElement() { }

        public ViewElement(Orientation contentOrientation)
            => ContentOrientation = contentOrientation;

        public ViewElement(int width, int height)
            => (Width, Height) = (width, height);

        private ViewElement AddContainer(Orientation contentOrientation)
            => AddViewElement(this, new ViewElement(contentOrientation));

        public ViewElement AddHorizontalBox()
            => AddContainer(Orientation.Horizontal);

        public ViewElement AddVerticalBox()
            => AddContainer(Orientation.Vertical);

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
            else
            {
                CalculateSizeVertical();
            }

            void CalculateSizeHorizontal()
            {
                ViewElements.ForEach(e =>
                {
                    e.CalculateSizes();
                    Width += e.LeftMargin + e.Width + (e.AddMargins ? Constants.SVG_MARGIN : 0);
                    Height = Math.Max(Height, (e.AddMargins ? Constants.SVG_MARGIN : 0) + e.TopMargin + e.Height);
                });
            }

            void CalculateSizeVertical()
            {
                ViewElements.ForEach(e =>
                {
                    e.CalculateSizes();
                    Height += e.TopMargin + e.Height + (e.AddMargins ? Constants.SVG_MARGIN : 0);
                    Width = Math.Max(Width, (e.AddMargins ? Constants.SVG_MARGIN : 0) + e.LeftMargin + e.Width);
                });
            }
        }


        private void CalculateXYs()
        {
            if (ViewElements == null) { return; }
            if (ContentOrientation == Orientation.Horizontal)
            {
                CalculateXYsHorizontal();
            }
            else
            {
                CalculateXYsVertical();
            }

            void CalculateXYsHorizontal()
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

            void CalculateXYsVertical()
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
}
