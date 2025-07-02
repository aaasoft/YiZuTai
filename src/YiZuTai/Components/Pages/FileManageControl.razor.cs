using BlazorDownloadFile;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Quick.Blazor.Bootstrap;
using Quick.Blazor.Bootstrap.Utils;
using System.Text;
using Tewr.Blazor.FileReader;

namespace YiZuTai.Components.Pages
{
    public partial class FileManageControl
    {
        private readonly Utils.UnitStringConverting storageUSC = Utils.UnitStringConverting.StorageUnitStringConverting;
        private readonly Utils.UnitStringConverting timeUSC = Utils.UnitStringConverting.TimeUnitStringConverting;
        [Inject]
        private IBlazorDownloadFileService BlazorDownloadFileService { get; set; }

        [Parameter]
        public string ZTFileId { get; set; }
        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        private Core.ZTFile ZTFile { get; set; }
        private string BasePath;
        
        private DirectoryInfo CurrentDir;
        private DirectoryInfo[] Dirs;
        private FileInfo[] Files;
        private Object SelectedItem;

        private ModalLoading modalLoading;
        private ModalAlert modalAlert;
        private ModalPrompt modalPrompt;
        private ToastStack toastStack;
        private ElementReference inputFile;

        protected override void OnParametersSet()
        {
            ZTFile = Core.ZTFileManager.Instance.Get(ZTFileId);
            if (ZTFile == null)
                return;
            BasePath = ZTFile.ResourceFolder;
            CurrentDir = new DirectoryInfo(BasePath);
            CurrentPath = "/";
            refresh();
        }

        private void gotoDir(DirectoryInfo dir)
        {
            var fullPath = dir.FullName;
            if (!fullPath.StartsWith(BasePath))
                fullPath = BasePath;

            CurrentDir = new DirectoryInfo(fullPath);
            CurrentPath = CurrentDir.FullName.Substring(BasePath.Length).Replace("\\", "/");
            if (!CurrentPath.StartsWith("/"))
                CurrentPath = "/" + CurrentPath;
            refresh();
        }

        private string CurrentPath { get; set; }

        private void btnGoto_Click()
        {
            if (string.IsNullOrEmpty(CurrentPath))
                gotoDir(new DirectoryInfo(BasePath));
            else
                gotoDir(new DirectoryInfo(Path.Combine(BasePath, CurrentPath.Substring(1))));
        }

        private void btnGotoUpper_Click()
        {
            if (CurrentDir == null)
                return;
            var dir = CurrentDir.Parent;
            gotoDir(dir);
        }

        private void btnCreateFolder_Click()
        {
            modalPrompt.Show("请输入新建文件夹的名称", "新建文件夹", dir_name =>
            {
                try
                {
                    var newDirPath = Path.Combine(CurrentDir.FullName, dir_name);
                    Directory.CreateDirectory(newDirPath);
                    toastStack.AddToast("成功", $"创建文件夹[{dir_name}]成功", BackgroundTheme.success);
                    refresh();
                    InvokeAsync(StateHasChanged);
                }
                catch (Exception ex)
                {
                    toastStack.AddToast("失败", $"创建文件夹[{dir_name}]失败，原因：{ex.Message}", BackgroundTheme.danger);
                }
            }, () => { });
        }

        private async Task onInputFileChanged()
        {
            IFileInfo fileInfo = null;
            try
            {
                //1MB缓存
                var buffer = new byte[1 * 1024 * 1024];
                System.Threading.CancellationTokenSource cts = new System.Threading.CancellationTokenSource();

                foreach (var file in await fileReaderService.CreateReference(inputFile).EnumerateFilesAsync())
                {
                    modalLoading.Show($"上传文件", "正在获取上传文件信息...", false, cts.Cancel);
                    fileInfo = await file.ReadFileInfoAsync();
                    if (!CurrentDir.Exists)
                        CurrentDir.Create();
                    var tmpFile = Path.Combine(CurrentDir.FullName, fileInfo.Name);
                    if (File.Exists(tmpFile))
                        throw new IOException($"文件[{fileInfo.Name}]已经存在！");

                    var fileInfoStr = $"{fileInfo.Name} ({storageUSC.GetString(fileInfo.Size, 0, true)}B)";
                    modalLoading.Show($"上传文件", $"正在上传文件[{fileInfoStr}]...", false, cts.Cancel);
                    var stopwatch = new System.Diagnostics.Stopwatch();

                    stopwatch.Start();
                    DateTime lastDisplayTime = DateTime.MinValue;

                    long readTotalCount = 0;
                    using (Stream stream = await file.OpenReadAsync())
                    using (var fileStream = System.IO.File.OpenWrite(tmpFile))
                    {
                        while (true)
                        {
                            //如果已取消
                            if (cts.IsCancellationRequested)
                                break;
                            var ret = await stream.ReadAsync(buffer, 0, buffer.Length);
                            if (ret == 0)
                            {
                                await Task.Delay(100);
                                continue;
                            }
                            else
                            {
                                readTotalCount += ret;
                                fileStream.Write(buffer, 0, ret);
                                if (readTotalCount >= fileInfo.Size)
                                    break;

                                if ((DateTime.Now - lastDisplayTime).TotalSeconds > 0.5 && stopwatch.Elapsed.TotalSeconds > 0)
                                {
                                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                                    var speed = Convert.ToDecimal(readTotalCount / stopwatch.ElapsedMilliseconds);
                                    sb.Append("上传速度：" + storageUSC.GetString(speed * 1000, 1, true) + "B/s");
                                    sb.Append("，预计剩余时间：" + timeUSC.GetString2((fileInfo.Size - readTotalCount) / speed, 1, false));
                                    modalLoading.UpdateProgress(Convert.ToInt32(readTotalCount * 100 / fileInfo.Size), sb.ToString());
                                    await InvokeAsync(StateHasChanged);
                                    lastDisplayTime = DateTime.Now;
                                }
                            }
                        }
                    }
                    stopwatch.Stop();
                    if (cts.IsCancellationRequested)
                    {
                        toastStack.AddToast("上传已取消", $"已取消上传文件[{fileInfoStr}].", BackgroundTheme.warning);
                        File.Delete(tmpFile);
                        return;
                    }
                    toastStack.AddToast("上传成功", $"已上传文件[{fileInfoStr}].");
                }
            }
            catch (Exception ex)
            {
                toastStack.AddToast("上传失败", ex.Message, BackgroundTheme.danger);
            }
            finally
            {
                await fileReaderService.CreateReference(inputFile).ClearValue();
                modalLoading.Close();
                refresh();
                SelectedItem = fileInfo?.Name;
            }
        }

        private void refresh()
        {
            SelectedItem = null;
            if (CurrentDir.Exists)
            {
                try
                {
                    Dirs = CurrentDir.GetDirectories();
                    Files = CurrentDir.GetFiles();
                }
                catch (Exception ex)
                {
                    toastStack.AddToast("错误", $"获取文件夹[{CurrentDir.Name}]的内容时出错，原因：{ex.Message}", BackgroundTheme.danger);
                    Dirs = null;
                    Files = null;
                }
            }
            else
            {
                Dirs = null;
                Files = null;
                toastStack.AddToast("错误", $"文件夹[{CurrentDir.FullName}]不存在！", BackgroundTheme.danger);
            }
        }

        private async void btnDownload_Click()
        {
            var fileInfo = SelectedItem as FileInfo;
            if (fileInfo == null || !fileInfo.Exists)
                return;

            System.Threading.CancellationTokenSource cts = new System.Threading.CancellationTokenSource();
            modalLoading.Show("下载", fileInfo.Name, false, cts.Cancel);
            var stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();
            DateTime lastDisplayTime = DateTime.MinValue;
            byte[] buffer = new byte[24 * 1024];
            var fileSize = fileInfo.Length;
            long readTotalCount = 0;

            try
            {
                using (var fs = fileInfo.OpenRead())
                {
                    while (!cts.IsCancellationRequested)
                    {
                        var ret = fs.Read(buffer, 0, buffer.Length);
                        if (ret <= 0)
                            break;
                        readTotalCount += ret;

                        if ((DateTime.Now - lastDisplayTime).TotalSeconds > 0.5 && stopwatch.ElapsedMilliseconds > 0)
                        {
                            StringBuilder sb = new StringBuilder();
                            var speed = Convert.ToDouble(readTotalCount / stopwatch.ElapsedMilliseconds);
                            sb.Append("传输速度" + ": " + storageUSC.GetString(Convert.ToDecimal(speed * 1000), 1, true) + "B/s");
                            var remainingTime = TimeSpan.FromMilliseconds((fileSize - readTotalCount) / speed);
                            sb.Append(",剩余时间: " + remainingTime.ToString(@"hh\:mm\:ss"));
                            modalLoading.UpdateProgress(Convert.ToInt32(readTotalCount * 100 / fileSize), sb.ToString());
                            await InvokeAsync(StateHasChanged);
                            lastDisplayTime = DateTime.Now;
                        }
                        await BlazorDownloadFileService.AddBuffer(new ArraySegment<byte>(buffer, 0, ret), cts.Token);
                    }
                }
                if (cts.IsCancellationRequested)
                {
                    modalAlert.Show("下载", "已取消");
                    return;
                }
                var result = await BlazorDownloadFileService.DownloadBinaryBuffers(fileInfo.Name, cts.Token);

                if (!result.Succeeded)
                {
                    modalAlert.Show("下载", "已取消" + Environment.NewLine + result.ErrorName + Environment.NewLine + result.ErrorMessage);
                }
            }
            catch (TaskCanceledException)
            {
                modalAlert.Show("下载", "已取消");
            }
            catch (Exception ex)
            {
                modalAlert.Show("下载", "已取消" + Environment.NewLine + ExceptionUtils.GetExceptionMessage(ex));
            }
            finally
            {
                await BlazorDownloadFileService.ClearBuffers();
                stopwatch.Stop();
                modalLoading.Close();
            }
        }

        private void btnPreview_Click()
        {
            var fileInfo = SelectedItem as FileInfo;
            if (fileInfo == null)
                return;
            var url = ZTFile.GetStaticResourceUrl(fileInfo.FullName.Substring(BasePath.Length + 1));
            JSRuntime.InvokeVoidAsync("window.open", url, "_blank");
        }

        private void btnRename_Click()
        {
            if (SelectedItem == null)
                return;
            if (SelectedItem is DirectoryInfo)
            {
                var dir = (DirectoryInfo)SelectedItem;
                modalPrompt.Show($"请输入文件夹[{dir.Name}]的新名称", dir.Name, name =>
                {
                    try
                    {
                        var newPath = Path.Combine(CurrentDir.FullName, name);
                        dir.MoveTo(newPath);
                        toastStack.AddToast("成功", $"重命名文件夹[{name}]成功", BackgroundTheme.success);
                        refresh();
                        InvokeAsync(StateHasChanged);
                    }
                    catch (Exception ex)
                    {
                        toastStack.AddToast("失败", $"重命名文件夹[{name}]失败，原因：{ex.Message}", BackgroundTheme.danger);
                    }
                }, () => { });
            }
            else if (SelectedItem is FileInfo)
            {
                var file = (FileInfo)SelectedItem;
                modalPrompt.Show($"请输入文件[{file.Name}]的新名称", file.Name, name =>
                {
                    try
                    {
                        var newPath = Path.Combine(CurrentDir.FullName, name);
                        file.MoveTo(newPath);
                        toastStack.AddToast("成功", $"重命名文件[{name}]成功", BackgroundTheme.success);
                        refresh();
                        InvokeAsync(StateHasChanged);
                    }
                    catch (Exception ex)
                    {
                        toastStack.AddToast("失败", $"重命名文件[{name}]失败，原因：{ex.Message}", BackgroundTheme.danger);
                    }
                }, () => { });
            }
        }

        private void btnDelete_Click()
        {
            if (SelectedItem == null)
                return;
            if (SelectedItem is FileInfo)
            {
                var file = (FileInfo)SelectedItem;
                modalAlert.Show("确认删除", $"确定要删除文件[{file.Name}]", () =>
                {
                    modalLoading.Show("删除中", $"正在删除文件[{file.Name}]...", true, null);
                    try
                    {
                        file.Delete();
                        toastStack.AddToast("信息", $"删除文件[{file.Name}]成功", BackgroundTheme.success);
                    }
                    catch (Exception ex)
                    {
                        toastStack.AddToast("错误", $"删除文件[{file.Name}]时出错，原因：{ex.Message}", BackgroundTheme.danger);
                    }
                    refresh();
                    modalLoading.Close();
                    InvokeAsync(StateHasChanged);
                }, () => { });
            }
            else if (SelectedItem is DirectoryInfo)
            {
                var dir = (DirectoryInfo)SelectedItem;
                modalAlert.Show("确认删除", $"确定要删除文件夹[{dir.Name}]", () =>
                {
                    modalLoading.Show("删除中", $"正在删除文件夹[{dir.Name}]...", true, null);
                    try
                    {
                        dir.Delete(true);
                        toastStack.AddToast("信息", $"删除文件夹[{dir.Name}]成功", BackgroundTheme.success);
                    }
                    catch (Exception ex)
                    {
                        toastStack.AddToast("错误", $"删除文件夹[{dir.Name}]时出错，原因：{ex.Message}", BackgroundTheme.danger);
                    }
                    refresh();
                    modalLoading.Close();
                    InvokeAsync(StateHasChanged);
                }, () => { });
            }
        }
    }
}
