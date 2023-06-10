using TreeView;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<TreeView.Tree.ITreePanel, TreeView.Tree.TreePanel>();
builder.Services.AddScoped<IMyTreeViewer, MyTreeViewer>();

var app = builder.Build();

app.MapGet("index", (IMyTreeViewer treeViewer) =>
{
    // Create a random data tree
    int noOfChildrenPerParent = 4, noOfLevels = 4;
    treeViewer.GenerateTree(noOfChildrenPerParent, noOfLevels);

    // Calculate the TreePanel & convert it into HTML/SVG
    int boxWidth = 180, boxHeight = (int)(boxWidth / 1.61803398875), margin = 20;
    return treeViewer.GetHtml(boxWidth, boxHeight, margin);
});

app.Run();
