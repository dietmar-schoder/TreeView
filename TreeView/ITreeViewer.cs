namespace TreeView
{
    public interface ITreeViewer
    {
        void GenerateTree(int numberOfChildrenPerParent, int numberOfLevels, MyTreeElement treeElement = null);

        void CalculateScreene(int boxWidth, int boxHeight, int margin);

        IResult GetHtmlSvg();
    }
}
