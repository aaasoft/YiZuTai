using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YiZuTai;
using YiZuTai.Core;
using YiZuTai.Design;

namespace Microsoft.AspNetCore.Builder
{
    public static class YiZuTaiMiddlewareExtensions
    {
        internal static YiZuTaiOptions Options { get; private set; }

        public static async Task GetResource(HttpContext httpContext,string id, string file)
        {
            var ztFile = ZTFileManager.Instance.Get(id);
            if (ztFile == null)
                return;
            //return NotFound($"未找到编号为[{id}]的组态文件！");

            var fullFile = Path.Combine(ztFile.Folder, ZTFile.ZTFILE_RESOURCE_FOLDER, file);
            if (!System.IO.File.Exists(fullFile))
                return;
                //return new NotFoundResult();
                //return NotFound($"在组态文件[{id}]目录未找到文件[{file}]！");
                var contentType = MimeMapping.MimeUtility.GetMimeMapping(fullFile);

            await httpContext.Response.SendFileAsync(fullFile);
            //return new PhysicalFileResult(fullFile, contentType);
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
            var dataDir = Options.DataFolder;
            if (!Directory.Exists(dataDir))
                Directory.CreateDirectory(dataDir);
            TagManager.Instance.Init();
            ZTFileManager.Instance.Init();
            ControlManager.Instance.Init();

            app.MapGroup(options.Prefix).MapGet("/{id}/" + ZTFile.ZTFILE_RESOURCE_FOLDER + "/{*file}", GetResource);
/*
                                    app.UseRouting();
                                    app.UseEndpoints(configure =>
                                    {
                                        configure.MapGet(options.Prefix + );
                                    });
                        */
            return app;
        }
    }
}