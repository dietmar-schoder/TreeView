namespace TreeView
{
    public interface ITreeViewer
    {
        void GenerateTree(int numberOfChildren, int numberOfLevels, MyTreeElement treeElement = null);

        IResult GetHtml(int boxWidth, int boxHeight, int margin);
    }
}
