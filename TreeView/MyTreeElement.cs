using TreeView.Tree;

namespace TreeView
{
    public class MyTreeElement : TreeElement
    {
        public string Number { get; set; }

        public MyTreeElement(bool isRoot = false) : base()
            => Number = isRoot ? "1" : string.Empty;

        public MyTreeElement AddChild(MyTreeElement element)
        {
            base.AddChild(element);
            element.Number = $"{Number}.{Children.Count}";
            return element;
        }
    }
}
