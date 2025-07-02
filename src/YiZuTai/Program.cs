using BlazorDownloadFile;
using Tewr.Blazor.FileReader;
using YiZuTai.Components;
using YiZuTai.Core;
using YiZuTai.Core.Utils;
using YiZuTai.Design;

//如果数据目录不存在，则创建
var dataDir = FolderUtils.GetDataDir();
if (!Directory.Exists(dataDir))
    Directory.CreateDirectory(dataDir);

TagManager.Instance.Init();
ZTFileManager.Instance.Init();
ControlManager.Instance.Init();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddBlazorDownloadFile();
builder.Services.AddFileReaderService();
builder.Services.AddBlazorContextMenu();
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
app.MapControllers();
app.Run();
