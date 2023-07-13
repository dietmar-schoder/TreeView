namespace TreeView
{
    public interface IMyTreeViewer
    {
        void GenerateTree(int numberOfChildren, int numberOfLevels, MyTreeElement treeElement = null);

        IResult GetHtml(int boxWidth, int boxHeight, int margin);
    }
}
