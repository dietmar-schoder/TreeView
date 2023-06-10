using TreeView;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ITreeViewer, TreeViewer>();

var app = builder.Build();

app.MapGet("index", (ITreeViewer treeViewer) =>
{
    var boxWidth = 180;
    var boxHeight = (int)(boxWidth / 1.61803398875);
    var noOfChildrenPerParent = 2;
    var noOfLevels = 8;
    var tree = treeViewer.GenerateTree(noOfChildrenPerParent, noOfLevels);
    // Create the TreePanel
    var treePanel = treeViewer.CreateTreePanel(tree, boxWidth, boxHeight, 20);
    // Convert panel into HTML/SVG
    return treeViewer.GetHtml(treePanel);
});

app.Run();
