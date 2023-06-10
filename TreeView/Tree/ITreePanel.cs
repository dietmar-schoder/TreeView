namespace TreeView.Tree
{
    public interface ITreePanel
    {
        TreePanel Create(TreeElement rootElement, int boxWidth, int boxHeight, int margin);
    }
}
