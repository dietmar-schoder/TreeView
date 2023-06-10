namespace TreeView
{
    public interface ITreeViewer
    {
        void GenerateTree(int numberOfChildren, int numberOfLevels);

        IResult GetHtml(int boxWidth, int boxHeight, int margin);
    }
}
