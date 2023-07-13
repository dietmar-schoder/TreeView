using TreeView;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<TreeView.Tree.ITreePanel, TreeView.Tree.TreePanel>();
builder.Services.AddScoped<ITreeViewer, TreeViewer>();

var app = builder.Build();

app.MapGet("index", (ITreeViewer treeViewer) =>
{
    // Create a random data tree
    treeViewer.GenerateTree(numberOfChildrenPerParent: 7, numberOfLevels: 3);

    // Calculate the sizes and positions
    int boxWidth = 80;
    int boxHeight = (int)(boxWidth / 1.61803398875);
    int margin = 32;
    treeViewer.CalculateScreene(boxWidth, boxHeight, margin);

    // Return HTML/SVG
    return treeViewer.GetHtmlSvg();
});

app.Run();
