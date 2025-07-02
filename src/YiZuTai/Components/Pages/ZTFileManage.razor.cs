using Microsoft.AspNetCore.Components;
using Quick.Blazor.Bootstrap;
using Quick.Blazor.Bootstrap.Utils;
using YiZuTai.Core;

namespace YiZuTai.Components.Pages
{
    public partial class ZTFileManage : ComponentBase
    {
        private ModalAlert modalAlert;
        private ModalWindow modalWindow;

        private string searchTag;
        private string searchKeywords;

        private void ShowTagManager()
        {
            modalWindow.Show<TagManage>("标签管理");
        }

        private void CreateZTFile()
        {
            modalWindow.Show<CreateZTFileControl>("创建组态文件",
                CreateZTFileControl.PrepareParameter(
                    null,
                    t =>
                    {
                        var ztFile = new Core.ZTFile()
                        {
                            Id = Guid.NewGuid().ToString("N"),
                            Name = t.Name,
                            Description = t.Description,
                            Tags = t.Tags
                        };
                        ZTFileManager.Instance.Add(ztFile);
                        InvokeAsync(StateHasChanged);
                        modalWindow.Close();
                    })
                );
        }

        private void EditZTFile(Core.ZTFile model)
        {
            modalWindow.Show<CreateZTFileControl>("编辑组态文件",
                CreateZTFileControl.PrepareParameter(
                    model,
                    t =>
                    {
                        model.Name = t.Name;
                        model.Description = t.Description;
                        model.Tags = t.Tags;
                        model.Save();
                        InvokeAsync(StateHasChanged);
                        modalWindow.Close();
                    })
                );
        }

        private void DeleteZTFile(Core.ZTFile model)
        {
            modalAlert.Show("删除确认", $"确定要删除组态文件[{model.Name}]？", () =>
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
            });
        }
    }
}
