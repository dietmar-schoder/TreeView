using TreeView;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ITreeViewer, TreeViewer>();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("index", (ITreeViewer treeViewer) =>
{
    var tree = treeViewer.GenerateTree(2, 5);
    var treePanel = treeViewer.CreateTreePanel(tree, 100, 60, 8);
    return treeViewer.GetHtml(treePanel);
});

app.Run();
