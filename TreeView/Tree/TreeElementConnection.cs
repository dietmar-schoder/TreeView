namespace TreeView.Tree
{
    public class TreeElementConnection
    {
        private readonly StructureLine _structureLine;

        public string ToSvg
            => $"<line x1=\"{_structureLine.X1}\"" +
                $" y1=\"{_structureLine.Y1}\"" +
                $" x2=\"{_structureLine.X2} \"" +
                $" y2=\" {_structureLine.Y2}\"" +
                $" style=\"stroke-width:1px;stroke:rgb(0,0,0)\" />";

        public TreeElementConnection(StructureLine structureLine)
            => _structureLine = structureLine;
    }
}
