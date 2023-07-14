namespace TreeView
{
    public interface IListViewer
    {
        void GenerateTree(int numberOfChildrenPerParent, int numberOfLevels, MyTreeElement treeElement = null);

        void CalculateScreen();

        IResult GetHtmlSvg(int margin, int lineHeight, int boxHeight);
    }
}
