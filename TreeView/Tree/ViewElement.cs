namespace TreeView.Tree
{
    public class ViewElement
    {
        public int X { get; set; }

        public int Y { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public int LeftMargin { get; set; }

        public int RightBottomMargin { get; set; }

        public ViewElement ParentViewElement { get; set; }

        public List<ViewElement> ViewElements { get; set; }

        public Orientation ContentOrientation { get; set; }

        private bool AddMargins => ViewElements == null;

        public ViewElement() { }

        public ViewElement(Orientation contentOrientation) => ContentOrientation = contentOrientation;

        public ViewElement(int width, int height, int rightBottomMargin)
            => (Width, Height, RightBottomMargin) = (width, height, rightBottomMargin);

        public ViewElement AddContainer(Orientation contentOrientation = Orientation.Vertical)
            => AddViewElement(new ViewElement(contentOrientation));

        public ViewElement AddElement(ViewElement viewElement) => AddViewElement(viewElement).ParentViewElement;

        protected ViewElement CalculateSizesAndXYs()
        {
            CalculateSizes();
            Width += RightBottomMargin;
            Height += RightBottomMargin;
            CalculatePositions();
            return this;
        }

        private ViewElement AddViewElement(ViewElement viewElement)
        {
            viewElement.ParentViewElement = this;
            ViewElements = ViewElements ?? new();
            ViewElements.Add(viewElement);
            return viewElement;
        }

        private void CalculateSizes()
        {
            if (ViewElements is not null)
            {
                (ContentOrientation == Orientation.Horizontal).DoEither(CalculateHorizontalSize, CalculateVerticalSize);
            }

            void CalculateHorizontalSize()
            {
                ViewElements.ForEach(e =>
                {
                    e.CalculateSizes();
                    Width += e.LeftMargin + e.Width + (e.AddMargins ? e.RightBottomMargin : 0);
                    Height = Math.Max(Height, (e.AddMargins ? e.RightBottomMargin : 0) + e.Height);
                });
            }

            void CalculateVerticalSize()
            {
                ViewElements.ForEach(e =>
                {
                    e.CalculateSizes();
                    Height += e.Height + (e.AddMargins ? e.RightBottomMargin : 0);
                    Width = Math.Max(Width, (e.AddMargins ? e.RightBottomMargin : 0) + e.LeftMargin + e.Width);
                });
            }
        }


        private void CalculatePositions()
        {
            if (ViewElements is not null)
            {
                (ContentOrientation == Orientation.Horizontal).DoEither(CalculateHorizontalPosition, CalculateVerticalPosition);
            }

            void CalculateHorizontalPosition()
            {
                var xIncrement = X;
                ViewElements.ForEach(e =>
                {
                    e.X = xIncrement + e.LeftMargin + (e.AddMargins ? e.RightBottomMargin : 0);
                    e.Y = Y + (e.AddMargins ? e.RightBottomMargin : 0);
                    xIncrement = e.X + e.Width - e.LeftMargin;
                    e.CalculatePositions();
                });
            }

            void CalculateVerticalPosition()
            {
                var yIncrement = Y;
                ViewElements.ForEach(e =>
                {
                    e.X = e.LeftMargin + X + (e.AddMargins ? e.RightBottomMargin : 0);
                    e.Y = yIncrement + (e.AddMargins ? e.RightBottomMargin : 0);
                    yIncrement = e.Y + e.Height;
                    e.CalculatePositions();
                });
            }
        }
    }
}
