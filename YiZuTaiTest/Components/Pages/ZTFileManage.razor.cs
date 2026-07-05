using Microsoft.AspNetCore.Components;
using Quick.Blazor.Bootstrap;
using Quick.Blazor.Bootstrap.Admin.Utils;
using Quick.Utils;
using Tewr.Blazor.FileReader;
using YiZuTai.Core;
using YiZuTai.Model;
using YiZuTaiTest.Components.Controls;
using static Quick.Blazor.Bootstrap.Admin.Utils.FileUploadHelper;

namespace YiZuTaiTest.Components.Pages
{
    public partial class ZTFileManage : ComponentBase
    {
        private ModalLoading modalLoading;
        private ModalAlert modalAlert;
        private ModalPrompt modalPrompt;
        private ModalWindow modalWindow;

        private GroupData SelectedGroup;
        private List<GroupData> RootGroups;


        private string searchKeywords;


        public class GroupData
        {
            public ZTGroup Model { get; set; }
            public string Title => Model.Name;
            public List<GroupData> Childs { get; set; } = new List<GroupData>();
            public List<GroupData> ParentList { get; set; }
            public string Icon => "fa fa-folder";
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            RootGroups = ZTFileManager.Instance.GetGroups(null).Select(t => new GroupData()
            {
                Model = t,
                ParentList = RootGroups
            }).ToList();
        }

        public List<GroupData> GetGroupChildren(TreeNode x)
        {
            var group = (GroupData)x.DataItem;
            modalLoading.Show("加载中", "正在加载...", true);
            try
            {
                group.Childs = ZTFileManager.Instance.GetGroups(group.Model.Id)
                .Select(t => new GroupData()
                {
                    Model = t,
                    ParentList = group.Childs
                })
                .ToList();
                return group.Childs;
            }
            catch (Exception ex)
            {
                modalAlert.Show("加载失败", ExceptionUtils.GetExceptionMessage(ex));
                return new List<GroupData>();
            }
            finally
            {
                modalLoading.Close();
            }
        }

        private void onSelectedGroupChanged(TreeNode treeNode)
        {
            SelectedGroup = treeNode?.DataItem as GroupData;
        }

        private void createRootGroup()
        {
            modalPrompt.Show("添加根分组", null, t =>
            {
                if (string.IsNullOrEmpty(t))
                {
                    modalAlert.Show("错误", "请输入分组名称");
                    return;
                }
                try
                {
                    var newModel = new ZTGroup()
                    {
                        Id = Guid.NewGuid().ToString("N"),
                        Name = t
                    };
                    ZTFileManager.Instance.AddGroup(newModel);
                    RootGroups.Add(new GroupData()
                    {
                        Model = newModel,
                        ParentList = RootGroups
                    });
                    InvokeAsync(StateHasChanged);
                }
                catch (Exception ex)
                {
                    modalAlert.Show("错误", ExceptionUtils.GetExceptionMessage(ex));
                }
            });
        }

        private void createChildGroup()
        {
            var currentGroup = SelectedGroup;
            if (currentGroup == null)
                return;

            modalPrompt.Show("添加子分组", null, t =>
            {
                if (string.IsNullOrEmpty(t))
                {
                    modalAlert.Show("错误", "请输入分组名称");
                    return;
                }
                try
                {
                    var newModel = new ZTGroup()
                    {
                        Id = Guid.NewGuid().ToString("N"),
                        Name = t,
                        ParentId = currentGroup.Model.Id
                    };
                    ZTFileManager.Instance.AddGroup(newModel);
                    currentGroup.Childs.Add(new GroupData()
                    {
                        Model = newModel,
                        ParentList = currentGroup.Childs
                    });
                    InvokeAsync(StateHasChanged);
                }
                catch (Exception ex)
                {
                    modalAlert.Show("错误", ExceptionUtils.GetExceptionMessage(ex));
                }
            });
        }

        private void editGroup()
        {

            var currentGroup = SelectedGroup;
            if (currentGroup == null)
                return;
            modalPrompt.Show("编辑分组", currentGroup.Title, t =>
            {
                if (string.IsNullOrEmpty(t))
                {
                    modalAlert.Show("错误", "请输入分组名称");
                    return;
                }
                try
                {
                    currentGroup.Model.Name = t;
                    ZTFileManager.Instance.UpdateGroup(currentGroup.Model);
                    InvokeAsync(StateHasChanged);
                }
                catch (Exception ex)
                {
                    modalAlert.Show("错误", ExceptionUtils.GetExceptionMessage(ex));
                }
            });
        }

        private bool IsGroupCanDelete => SelectedGroup != null
            && ZTFileManager.Instance.Query(SelectedGroup.Model.Id, null).Length == 0
            && ZTFileManager.Instance.GetGroups(SelectedGroup.Model.Id).Length == 0;

        private void deleteGroup()
        {
            var currentGroup = SelectedGroup;
            if (currentGroup == null)
                return;
            modalAlert.Show("确认", $"是否删除分组[{currentGroup.Model.Name}]?", new ()
            {
                OkCallback = () =>
                {
                    SelectedGroup = null;
                    modalAlert.Close();
                    ZTFileManager.Instance.DeleteGroup(currentGroup.Model.Id);
                    var parentList = currentGroup.ParentList ?? RootGroups;
                    parentList.Remove(currentGroup);
                    InvokeAsync(StateHasChanged);
                }
            });
        }

        private ElementReference inputYztFile;
        [Inject]
        private IFileReaderService fileReaderService { get; set; }
        private CancellationTokenSource uploadCts;
        private async Task onInputYrtFileChanged()
        {
            var fileReaderRef = fileReaderService.CreateReference(inputYztFile );

            uploadCts = new CancellationTokenSource();
            UploadFileInfo uploadingFileInfo = default;
            string uploadingFileInfoStr = null;
            string uploadingFile = null;
            try
            {
                modalLoading.Show($"上传组态文件", "正在获取上传文件信息...", false, uploadCts.Cancel);
                var fileReference = (await fileReaderRef.EnumerateFilesAsync()).FirstOrDefault();
                uploadingFile = await FileUploadHelper.UploadFileAsync(fileReference,
                    fileInfo =>
                    {
                        uploadingFileInfo = fileInfo;
                        uploadingFileInfoStr = $"{fileInfo.Name} ({fileInfo.SizeString})";
                        modalLoading.Show($"上传组态文件", $"正在上传组态文件[{uploadingFileInfoStr}]...", false, uploadCts.Cancel);
                        return null;
                    },
                    progressInfo => modalLoading.UpdateProgress(progressInfo.Percent, progressInfo.Message),
                    uploadCts.Token);
                modalLoading.UpdateProgress(null, null);
                modalLoading.Show($"上传组态文件", $"正在加载组态文件[{uploadingFileInfoStr}]...", true, uploadCts.Cancel);

                ZTFileManager.Instance.Import(uploadingFile, SelectedGroup.Model.Id);
                await InvokeAsync(StateHasChanged);

            }
            catch (OperationCanceledException)
            {
                modalAlert?.Show("上传已取消", $"已取消上传运行库文件[{uploadingFileInfoStr}].");
            }
            catch (Exception ex)
            {
                modalAlert?.Show("上传失败", ExceptionUtils.GetExceptionString(ex));
            }
            finally
            {
                if (uploadingFile != null && File.Exists(uploadingFile))
                    try { File.Delete(uploadingFile); } catch { }
                await fileReaderRef.ClearValue();
                modalLoading?.Close();
            }
        }

        private void CreateZTFile()
        {
            modalWindow.Show("创建组态文件", new DialogParameters<CreateZTFileControl>
            {
                { x=> x.OkAction, t =>
                    {
                        try
                        {
                            var id  = t.Id;
                            if(string.IsNullOrEmpty(id))
                                id = Guid.NewGuid().ToString("N");
                            var ztFile = new ZTFile()
                            {
                                Id = id,
                                Name = t.Name,
                                Description = t.Description,
                                GroupId = SelectedGroup.Model.Id
                            };
                            ZTFileManager.Instance.Add(ztFile);
                            InvokeAsync(StateHasChanged);
                            modalWindow.Close();
                        }
                        catch(Exception ex)
                        {
                            modalAlert.Show("错误", ExceptionUtils.GetExceptionMessage(ex));
                        }
                    }}
            });
        }

        private void EditZTFile(ZTFile model)
        {
            modalWindow.Show("编辑组态文件", new DialogParameters<CreateZTFileControl>
            {
                { x => x.ZTFileInfo, model },
                { x => x.OkAction, t =>
                    {
                        try
                        {
                            if(string.IsNullOrEmpty(t.Id))
                                throw new IOException("必须输入编号");
                            if(model.Id != t.Id)
                            {
                                ZTFileManager.Instance.ChangeId(model.Id,t.Id);
                                model.Id = t.Id;
                            }
                            model.Name = t.Name;
                            model.Description = t.Description;
                            model.Save();
                            InvokeAsync(StateHasChanged);
                            modalWindow.Close();
                        }
                        catch(Exception ex)
                        {
                            modalAlert.Show("错误", ExceptionUtils.GetExceptionMessage(ex));
                        }
                    }
                }
            });
        }

        private void DeleteZTFile(ZTFile model)
        {
            modalAlert.Show("删除确认", $"确定要删除组态文件[{model.Name}]？", new ()
            {
                OkCallback = () =>
                {
                    try
                    {
                        ZTFileManager.Instance.Delete(model.Id);
                    }
                    catch (Exception ex)
                    {
                        Task.Delay(100).ContinueWith(t =>
                        {
                            modalAlert.Show("删除组态文件时出错", ExceptionUtils.GetExceptionMessage(ex));
                        });
                    }
                    InvokeAsync(StateHasChanged);
                }
            });
        }
    }
}
