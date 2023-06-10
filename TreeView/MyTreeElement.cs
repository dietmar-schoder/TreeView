using TreeView.Tree;

namespace TreeView
{
    public class MyTreeElement : TreeElement
    {
        public string Number { get; set; } = "1";

        public int Width { get; set; }

        public int Height { get; set; }

        public string ToSvg =>
            $"<g transform=\"translate({ViewElement.X} {ViewElement.Y})\">" +

            $"<rect x=\"0\" y=\"0\" rx=\"4px\" ry=\"4px\"" +
            $" width=\"{Width}px\"" +
            $" height=\"{Height}px\"" +
            $" style=\"fill:transparent;stroke-width:1;stroke:rgb(0,0,0)\" />" +

            $"<text alignment-baseline=\"middle\"" +
            //$" font-size=\"{FontSize}\"" +
            $" text-anchor=\"middle\" x=\"{Width / 2}\"" +
            $" y=\"{Height / 2 + 1}\">{Number}" +
            $"</text>" +

            $"</g>";

        public MyTreeElement NewChild()
        {
            var child = new MyTreeElement();
            child.Width = 180;
            child.Height = (int)(child.Width / 1.61803398875);
            AddAsChild(child);
            child.Number = $"{Number}.{Children.Count}";
            return child;
        }
    }
}
