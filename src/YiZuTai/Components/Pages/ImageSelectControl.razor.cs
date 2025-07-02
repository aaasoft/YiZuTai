using BlazorDownloadFile;
using Microsoft.AspNetCore.Components;
using Quick.Blazor.Bootstrap;
using Quick.Blazor.Bootstrap.Utils;
using System.Text;
using Tewr.Blazor.FileReader;

namespace YiZuTai.Components.Pages
{
    public partial class ImageSelectControl
    {
        private static HashSet<string> imageFileSuffixHashSet = new HashSet<string> { ".jpg", ".png", ".svg", ".gif", ".tif", ".bmp" };
        private readonly YiZuTai.Utils.UnitStringConverting storageUSC = YiZuTai.Utils.UnitStringConverting.StorageUnitStringConverting;
        private readonly YiZuTai.Utils.UnitStringConverting timeUSC = YiZuTai.Utils.UnitStringConverting.TimeUnitStringConverting;
        [Inject]
        private IBlazorDownloadFileService BlazorDownloadFileService { get; set; }

        [Parameter]
        public string ZTFileId { get; set; }
        [Parameter]
        public string SelectedItem { get; set; }
        [Parameter]
        public Action<string> OkAction { get; set; }
        private string[] Images { get; set; }
        private string CurrentPath { get; set; }

        private ModalLoading modalLoading;
        private ModalAlert modalAlert;
        private ModalPrompt modalPrompt;
        private ToastStack toastStack;
        private ElementReference inputFile;

        protected override void OnParametersSet()
        {
            var ztFile = Core.ZTFileManager.Instance.Get(ZTFileId);
            if (ztFile == null)
                return;
            CurrentPath = ztFile.ResourceFolder;
            refresh();
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
                    if (!Directory.Exists(CurrentPath))
                        Directory.CreateDirectory(CurrentPath);
                    var tmpFile = Path.Combine(CurrentPath, fileInfo.Name);
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

        private string getImageSize(string id)
        {
            var file = Path.Combine(CurrentPath, id);
            var fileInfo = new FileInfo(file);
            if (!fileInfo.Exists)
                return "";
            return storageUSC.GetString(fileInfo.Length, 0, true) + "B";
        }

        private void refresh()
        {
            List<string> list = new List<string>();
            if (Directory.Exists(CurrentPath))
            {
                foreach (var file in Directory.GetFiles(CurrentPath, "*", SearchOption.AllDirectories))
                {
                    var extension = Path.GetExtension(file);
                    if (!imageFileSuffixHashSet.Contains(extension))
                        continue;
                    var id = file.Substring(CurrentPath.Length);
                    id = id.Replace(@"\", "/");
                    while (id.StartsWith("/"))
                        id = id.Substring(1);
                    list.Add(id);
                }
            }
            Images = list.ToArray();
        }

        private async void btnDownload_Click()
        {
            var file = Path.Combine(CurrentPath, SelectedItem);
            var fileInfo = new FileInfo(file);
            if (!fileInfo.Exists)
                return;

            System.Threading.CancellationTokenSource cts = new System.Threading.CancellationTokenSource();
            modalLoading.Show("下载", fileInfo.Name, false, cts.Cancel);
            var stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();
            DateTime lastDisplayTime = DateTime.MinValue;
            byte[] buffer = new byte[24 * 1024];
            var fileSize = file.Length;
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

        private void btnRename_Click()
        {
            var file = Path.Combine(CurrentPath, SelectedItem);
            var fileInfo = new FileInfo(file);
            if (!fileInfo.Exists)
                return;

            modalPrompt.Show(string.Format("请输入文件[{0}]的新名称：", fileInfo.Name), fileInfo.Name, name =>
            {
                try
                {
                    var newPath = Path.Combine(CurrentPath, name);
                    fileInfo.MoveTo(newPath);
                    modalAlert.Show("重命名", "成功");
                    refresh();
                    InvokeAsync(StateHasChanged);
                }
                catch (Exception ex)
                {
                    modalAlert.Show("重命名", "失败" + Environment.NewLine + ExceptionUtils.GetExceptionMessage(ex));
                }
            }, () => { });
        }

        private void btnDelete_Click()
        {
            if (SelectedItem == null)
                return;

            var file = Path.Combine(CurrentPath, SelectedItem);
            if (!File.Exists(file))
            {
                modalAlert.Show("错误", $"文件[{SelectedItem}]不存在！");
                return;
            }
            modalAlert.Show("删除", $"确定要删除图片[{SelectedItem}]？", () =>
             {
                 modalLoading.Show("正在删除", SelectedItem, true, null);
                 try
                 {
                     File.Delete(file);
                     toastStack.AddToast("删除", $"删除图片[{SelectedItem}]成功");
                     SelectedItem = null;
                 }
                 catch (Exception ex)
                 {
                     toastStack.AddToast("删除", $"删除图片[{SelectedItem}]失败" + Environment.NewLine + ExceptionUtils.GetExceptionMessage(ex));
                 }
                 refresh();
                 modalLoading.Close();
                 InvokeAsync(StateHasChanged);
             }, () => { });
        }

        private void btnOk_Click()
        {
            OkAction?.Invoke(SelectedItem);
        }
    }
}
