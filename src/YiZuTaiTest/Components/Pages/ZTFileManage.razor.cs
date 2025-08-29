using Microsoft.AspNetCore.Components;
using Quick.Blazor.Bootstrap;
using Quick.Blazor.Bootstrap.Utils;
using YiZuTai.Core;
using YiZuTai.Model;
using YiZuTaiTest.Components.Controls;

namespace YiZuTaiTest.Components.Pages
{
    public partial class ZTFileManage : ComponentBase
    {
        private ModalAlert modalAlert;
        private ModalWindow modalWindow;
        private string searchKeywords;

        private void CreateZTFile()
        {
            modalWindow.Show("创建组态文件", new DialogParameters<CreateZTFileControl>
            {
                { x=> x.OkAction, t =>
                    {
                        var id  = t.Id;
                        if(string.IsNullOrEmpty(id))
                            id = Guid.NewGuid().ToString("N");
                        var ztFile = new ZTFile()
                        {
                            Id = id,
                            Name = t.Name,
                            Description = t.Description
                        };
                        ZTFileManager.Instance.Add(ztFile);
                        InvokeAsync(StateHasChanged);
                        modalWindow.Close();
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
                        model.Name = t.Name;
                        model.Description = t.Description;
                        model.Save();
                        InvokeAsync(StateHasChanged);
                        modalWindow.Close();
                    }
                }
            });
        }

        private void DeleteZTFile(ZTFile model)
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
