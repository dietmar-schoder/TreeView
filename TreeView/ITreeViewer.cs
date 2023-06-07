using TreeView.Tree;

namespace TreeView
{
    public interface ITreeViewer
    {
        MyTreeElement GenerateTree(int numberOfChildren, int numberOfLevels);

        TreePanel CreateTreePanel(MyTreeElement tree, int boxWidth, int boxHeight, int margin);

        IResult GetHtml(TreePanel treePanel);
    }
}
