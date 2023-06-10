using TreeView;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<TreeView.Tree.ITreePanel, TreeView.Tree.TreePanel>();
builder.Services.AddScoped<ITreeViewer, TreeViewer>();

var app = builder.Build();

app.MapGet("index", (ITreeViewer treeViewer) =>
{
    var noOfChildrenPerParent = 4;
    var noOfLevels = 4;
    // Create a random data tree
    treeViewer.GenerateTree(noOfChildrenPerParent, noOfLevels);

    var boxWidth = 180;
    var boxHeight = (int)(boxWidth / 1.61803398875);
    var margin = 20;
    // Calculate the TreePanel, convert TreePanel into HTML/SVG
    return treeViewer.GetHtml(boxWidth, boxHeight, margin);
});

app.Run();
