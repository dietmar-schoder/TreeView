using TreeView;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<TreeView.Tree.ITreePanel, TreeView.Tree.TreePanel>();
builder.Services.AddScoped<ITreeViewer, TreeViewer>();
builder.Services.AddScoped<IListViewer, ListViewer>();

var app = builder.Build();

var numberOfChildrenPerParent = 2;
var numberOfLevels = 7;

app.MapGet("index", (ITreeViewer treeViewer) =>
{
    // Create a random data tree
    treeViewer.GenerateTree(numberOfChildrenPerParent, numberOfLevels);

    // Calculate sizes and positions
    int boxWidth = 80;
    int boxHeight = (int)(boxWidth / 1.61803398875);
    int margin = 24;
    treeViewer.CalculateScreen(boxWidth, boxHeight, margin);

    // Return HTML/SVG
    return treeViewer.GetHtmlSvg();
});

app.MapGet("list", (IListViewer listViewer) =>
{
    // Create a random data tree
    listViewer.GenerateTree(numberOfChildrenPerParent, numberOfLevels);

    // Create list of elements with element levels
    listViewer.CalculateScreen();

    // Return HTML/SVG
    int margin = 12;
    int lineHeight = 24;
    int boxHeight = 20;
    return listViewer.GetHtmlSvg(margin, lineHeight, boxHeight);
});

app.Run();
