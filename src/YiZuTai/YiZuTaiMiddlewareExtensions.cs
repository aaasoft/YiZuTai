using Microsoft.AspNetCore.Http;
using YiZuTai;
using YiZuTai.Core;
using YiZuTai.Design;
using YiZuTai.Source;

namespace Microsoft.AspNetCore.Builder
{
    public static class YiZuTaiMiddlewareExtensions
    {
        internal static YiZuTaiOptions Options { get; private set; }

        public static string GetUrlWithPrefix(string path) => $"{Options.Prefix}{path}";

        public static async Task GetResource(HttpContext httpContext, string id, string file)
        {
            var rep = httpContext.Response;
            var ztFile = ZTFileManager.Instance.Get(id);
            if (ztFile == null)
            {
                rep.StatusCode = 404;
                return;
            }

            var fullFile = Path.Combine(ztFile.Folder, ZTFile.ZTFILE_RESOURCE_FOLDER, file);
            if (!File.Exists(fullFile))
            {
                rep.StatusCode = 404;
                return;
            }
            await rep.SendFileAsync(fullFile);
        }

        public static IApplicationBuilder UseYiZuTai(this WebApplication app)
        {
            return UseYiZuTai(app, new());
        }

        public static WebApplication UseYiZuTai(this WebApplication app, YiZuTaiOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));
            Options = options;
            
            //如果数据目录不存在，则创建
            var dataDir = options.DataFolder;
            if (!Directory.Exists(dataDir))
                Directory.CreateDirectory(dataDir);
            TagManager.Instance.Init();
            ZTFileManager.Instance.Init();
            SourceManager.Instance.Init();
            
            if (options.UseDefaultControls)
                ControlManager.Instance.UseDefaultControls();

            app.MapGroup(options.Prefix).MapGet("/{id}/" + ZTFile.ZTFILE_RESOURCE_FOLDER + "/{*file}", GetResource);
            return app;
        }
    }
}