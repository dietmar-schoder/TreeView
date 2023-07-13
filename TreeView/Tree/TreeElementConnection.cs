namespace TreeView.Tree
{
    public class TreeElementConnection
    {
        public double X1 { get; set; }

        public double Y1 { get; set; }

        public double X2 { get; set; }

        public double Y2 { get; set; }

        public TreeElementConnection(double x1, double y1, double x2, double y2)
            => (X1, Y1, X2, Y2) = (x1, y1, x2, y2);
    }
}
