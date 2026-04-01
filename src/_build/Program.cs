
using Quick.Build;
using SharpCompress.Archives;
using SharpCompress.Archives.Zip;
using SharpCompress.Common;

//版本号
var version = "1.0." + DateTime.Now.ToString("yyyy.Mdd");
var productDir = "YiZuTaiTest";

//准备目录变量
var appFolder = QbFolder.GetAppFolder();
if (appFolder == Environment.CurrentDirectory)
    Environment.CurrentDirectory = Path.GetFullPath("../../../../../");
var baseFolder = Environment.CurrentDirectory;
var outFolder = Path.GetFullPath("bin");
if (!Directory.Exists(outFolder))
    Directory.CreateDirectory(outFolder);
var productName = QbJson.ReadString(Path.Combine($"src/{productDir}/YiQiDong.Image.json"), "Name");

//开始
Console.WriteLine("----------------------------------");
Console.WriteLine($"  欢迎使用[{productName}]发布脚本");
Console.WriteLine("----------------------------------");
Console.WriteLine();

var selectArchs = new[] { "any" };
foreach (var rid in selectArchs)
{
    Console.WriteLine($"开始编译[{rid}]...");

    var publishFolder = string.Empty;
    var outFile = string.Empty;
    if (rid == "any")
    {
        publishFolder = $"src/{productDir}/bin/Release/publish";
        outFile = Path.Combine(outFolder, $"{productName}-{version}.ymg");
    }
    else
    {
        publishFolder = $"src/{productDir}/bin/Release/{rid}/publish";
        outFile = Path.Combine(outFolder, $"{productName}-{version}-{rid}.ymg");
    }

    Console.WriteLine("正在删除Release目录...");
    //先删除Release目录
    QbFolder.DeleteFolders("src", "Release", SearchOption.AllDirectories);
    //再删除ymg文件
    QbFile.Delete(outFile);

    Console.WriteLine("正在发布软云项目...");
    if (rid == "any")
    {
        QbCommand.Run("dotnet", $"publish src/{productDir} -c Release");
        QbDotNet.KeepPublishRuntimes(publishFolder, "win", "unix", "win-x64", "linux-x64", "linux-arm64");
    }
    else
    {
        QbCommand.Run("dotnet", $"publish src/{productDir} -c Release -r {rid} --self-contained -p:PublishSingleFile=true -p:PublishTrimmed=true");
    }
    //复制文件
    QbFile.CopyFiles($"src/{productDir}", publishFolder, "YiQiDong.Image.*", true);
    //修改容器信息文件中的版本号
    QbJson.WriteString(Path.Combine(publishFolder, "YiQiDong.Image.json"), "Version", version);
    if (rid != "any")
    {
        QbJson.Write(Path.Combine(publishFolder, "YiQiDong.Image.json"), "Runtime", new string[0]);
    }
    //修改平台的值
    QbJson.Write(Path.Combine(publishFolder, "YiQiDong.Image.json"), "Platform", new string[] { rid });

    Console.WriteLine("正在制作易启动镜像...");
    using (var archive = ZipArchive.CreateArchive())
    {
        archive.AddAllFromDirectory(publishFolder);
        archive.SaveTo(outFile, CompressionType.LZMA);
    }
}
Console.WriteLine("完成");
QbGui.OpenFolder("bin");