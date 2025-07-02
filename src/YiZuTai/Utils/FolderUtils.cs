using Microsoft.AspNetCore.Builder;

namespace YiZuTai.Core.Utils
{
    public static class FolderUtils
    {
        /// <summary>
        /// 获取程序目录
        /// </summary>
        /// <returns></returns>
        public static string GetProgramDir() => Path.GetDirectoryName(typeof(FolderUtils).Assembly.Location);
        /// <summary>
        /// 获取程序目录下的路径
        /// </summary>
        /// <param name="paths"></param>
        /// <returns></returns>
        public static string GetPathUnderProgramDir(params string[] paths)
        {
            return Path.Combine(new[] { GetProgramDir() }.Concat(paths).ToArray());
        }
        /// <summary>
        /// 获取工作目录
        /// </summary>
        /// <returns></returns>
        public static string GetWorkingDir() => Directory.GetCurrentDirectory();
        /// <summary>
        /// 获取工作目录下的路径
        /// </summary>
        /// <param name="paths"></param>
        /// <returns></returns>
        public static string GetPathUnderWorkingDir(params string[] paths)
        {
            return Path.Combine(new[] { GetWorkingDir() }.Concat(paths).ToArray());
        }

        /// <summary>
        /// 获取数据目录
        /// </summary>
        /// <returns></returns>
        public static string GetDataDir()
        {
#if DEBUG
            return GetPathUnderProgramDir(YiZuTaiMiddlewareExtensions.Options.DataFolder);
#else
            return GetPathUnderWorkingDir(YiZuTaiMiddlewareExtensions.Options.DataFolder);
#endif
        }

        /// <summary>
        /// 获取数据目录下的路径
        /// </summary>
        /// <param name="paths"></param>
        /// <returns></returns>
        public static string GetPathUnderDataDir(params string[] paths)
        {
            return Path.Combine(new[] { GetDataDir() }.Concat(paths).ToArray());
        }

        public static void CopyFile(string file, string folder)
        {
            var targetFile = Path.Combine(folder, Path.GetFileName(file));
            if (!File.Exists(file) || File.Exists(targetFile))
                return;
            File.Copy(file, targetFile);
        }

        public static void CopyFolder(string source, string target)
        {
            if (!Directory.Exists(source) || Directory.Exists(target))
                return;

            Directory.CreateDirectory(target);
            var files = Directory.GetFiles(source);
            //先复制文件
            foreach (var file in files)
                File.Copy(file, Path.Combine(target, Path.GetFileName(file)));
            //再复制目录
            var folders = Directory.GetDirectories(source);
            foreach (var folder in folders)
                CopyFolder(folder, Path.Combine(target, Path.GetFileName(folder)));
        }
    }
}
