namespace TreeView
{
    public interface IMyTreeViewer
    {
        void GenerateTree(int numberOfChildren, int numberOfLevels);

        IResult GetHtml(int boxWidth, int boxHeight, int margin);
    }
}
