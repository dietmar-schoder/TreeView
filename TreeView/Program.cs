using TreeView;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<TreeView.Tree.ITreePanel, TreeView.Tree.TreePanel>();
builder.Services.AddScoped<ITreeViewer, TreeViewer>();

var app = builder.Build();

app.MapGet("index", (ITreeViewer treeViewer) =>
{
    // Create a random data tree
    treeViewer.GenerateTree(numberOfChildrenPerParent: 7, numberOfLevels: 3);

    // Calculate the TreePanel & convert it into HTML/SVG
    int boxWidth = 80;
    int boxHeight = (int)(boxWidth / 1.61803398875);
    int margin = 32;
    return treeViewer.GetHtml(boxWidth, boxHeight, margin);
});

app.Run();
