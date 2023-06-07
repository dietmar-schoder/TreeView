using TreeView.Tree;

namespace TreeView
{
    public class MyTreeElement : TreeElement
    {
        public string Number { get; set; } = "1";

        public MyTreeElement NewChild()
        {
            var child = new MyTreeElement();
            AddAsChild(child);
            child.Number = $"{Number}.{Children.Count}";
            return child;
        }
    }
}
