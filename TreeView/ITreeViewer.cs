namespace TreeView
{
    public interface ITreeViewer
    {
        MyTreeElement GenerateTree(int numberOfChildren, int numberOfLevels);

        IResult GetHtml(Tree.TreePanel treePanel);
    }
}
