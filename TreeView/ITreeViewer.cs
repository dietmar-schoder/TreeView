namespace TreeView
{
    public interface ITreeViewer
    {
        void GenerateTree(int numberOfChildrenPerParent, int numberOfLevels, MyTreeElement treeElement = null);

        IResult GetHtml(int boxWidth, int boxHeight, int margin);
    }
}
