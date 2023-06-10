using TreeView;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ITreeViewer, TreeViewer>();
builder.Services.AddScoped<TreeView.Tree.ITreePanel, TreeView.Tree.TreePanel>();

var app = builder.Build();

app.MapGet("index", (ITreeViewer treeViewer, TreeView.Tree.ITreePanel treePanel) =>
{
    // Create a data tree
    var noOfChildrenPerParent = 4;
    var noOfLevels = 4;
    var tree = treeViewer.GenerateTree(noOfChildrenPerParent, noOfLevels);

    // Create the TreePanel
    var boxWidth = 180;
    var boxHeight = (int)(boxWidth / 1.61803398875);
    var margin = 20;
    var panel = treePanel.Create(tree, boxWidth, boxHeight, margin);

    // Convert TreePanel into HTML/SVG
    return treeViewer.GetHtml(panel);
});

app.Run();
