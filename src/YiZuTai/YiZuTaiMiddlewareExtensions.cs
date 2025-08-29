using Microsoft.AspNetCore.Http;
using YiZuTai;
using YiZuTai.Core;
using YiZuTai.Model;

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
            rep.ContentType = getMimeMapping(fullFile);
            rep.ContentLength = new FileInfo(fullFile).Length;
            using (var fs = File.OpenRead(fullFile))
                await fs.CopyToAsync(rep.Body);
        }

        private static string getMimeMapping(string file)
        {
            var extension = Path.GetExtension(file).ToLower();
            switch (extension)
            {
                case ".jpg":
                case ".jpeg":
                    return "image/jpeg";
                case ".png":
                    return "image/png";
                case ".svg":
                    return "image/svg+xml";
                case ".ogv":
                    return "video/ogg";
                case ".webm":
                    return "video/webm";
                case ".mp4":
                    return "video/mp4";
                case ".avi":
                    return "video/avi";
                case ".htm":
                case ".html":
                    return "text/html";
                default:
                    return "application/octet-stream";
            }
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
            ZTFileManager.Instance.Init(dataDir);
            SourceManager.Instance.Init();

            app.MapGroup(options.Prefix).MapGet("/{id}/" + ZTFile.ZTFILE_RESOURCE_FOLDER + "/{*file}", GetResource);
            return app;
        }
    }
}