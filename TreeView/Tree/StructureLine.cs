namespace TreeView.Tree
{
    public class StructureLine
    {
        public int X1 { get; set; }

        public int Y1 { get; set; }

        public int X2 { get; set; }

        public int Y2 { get; set; }

        public StructureLine(int x1, int y1, int x2, int y2)
            => (X1, Y1, X2, Y2) = (x1, y1, x2, y2);

        public static StructureLine Vertical(int x, int y1, int y2)
            => new StructureLine(x, y1, x, y2);

        public static StructureLine Horizontal(int x1, int x2, int y)
            => new StructureLine(x1, y, x2, y);
    }
}
