namespace TreeView.Tree
{
    public interface ITreePanel
    {
        int Width { get; set; }

        int Height { get; set; }

        List<TreeElement> TreeElements { get; set; }

        void Calculate(TreeElement rootElement, int boxWidth, int boxHeight, int margin);
    }
}
