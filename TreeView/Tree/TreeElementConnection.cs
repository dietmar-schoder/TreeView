namespace TreeView.Tree
{
    public class TreeElementConnection
    {
        public int X1 { get; set; }

        public int Y1 { get; set; }

        public int X2 { get; set; }

        public int Y2 { get; set; }

        public TreeElementConnection(int x1, int y1, int x2, int y2)
            => (X1, Y1, X2, Y2) = (x1, y1, x2, y2);
    }
}
