using BlazorDownloadFile;
using Tewr.Blazor.FileReader;
using YiZuTaiTest.Components;

YiZuTai.Sample.Provider.Register();


WebApplicationOptions webApplicationOptions;
var imageFolder = Environment.GetEnvironmentVariable("IMAGE_DIR");
if (string.IsNullOrEmpty(imageFolder))
    webApplicationOptions = new WebApplicationOptions();
else
    webApplicationOptions = new WebApplicationOptions() { ContentRootPath = imageFolder };
var builder = WebApplication.CreateBuilder(webApplicationOptions);

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
app.UseYiZuTai();
app.Run();
