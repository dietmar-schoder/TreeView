namespace TreeView
{
    public class MyTreeElement : Tree.TreeElement
    {
        public string Number { get; set; }

        public string ToSvg()
        {
            // Draw box with number
            var svg =  $"<g transform=\"translate({ViewElement.X} {ViewElement.Y})\">" +
            $"<rect x=\"0\" y=\"0\" rx=\"4px\" ry=\"4px\"" +
            $" width=\"{ViewElement.Width}px\"" +
            $" height=\"{ViewElement.Height}px\"" +
            $" style=\"fill:transparent;stroke-width:1;stroke:rgb(0,0,0)\" />" +
            $"<text alignment-baseline=\"middle\"" +
            $" text-anchor=\"middle\" x=\"{ViewElement.Width / 2}\"" +
            $" y=\"{ViewElement.Height / 2 + 1}\">{Number}" +
            $"</text>" +
            $"</g>";

            // Draw lines connecting this element with its children
            TreeElementConnections.ForEach(c =>
                svg += $"<line x1=\"{c.X1}\" y1=\"{c.Y1}\" x2=\"{c.X2} \" y2=\" {c.Y2}\"" +
                    $" style=\"stroke-width:1px;stroke:rgb(0,0,0)\" />");

            return svg;
        }

        public MyTreeElement() => Number = "1";

        public MyTreeElement(MyTreeElement parent)
        {
            parent.AddAsChild(this);
            Number = $"{parent.Number}.{parent.Children.Count}";
        }
    }
}
