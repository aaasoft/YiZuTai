using Quick.Blazor.Bootstrap;
using Quick.Blazor.Bootstrap.Utils;
using YiZuTai.Core;

namespace YiZuTaiTest.Components.Pages
{
    public partial class TagManage
    {
        private ModalAlert modalAlert;
        private ModalPrompt modalPrompt;

        private void CreateTag()
        {
            modalPrompt.Show("创建标签", null, t =>
              {
                  var newGroupName = t?.Trim();
                  if (string.IsNullOrEmpty(newGroupName))
                  {
                      modalAlert.Show("创建标签失败", "未输入标签名称！");
                      return;
                  }
                  if (TagManager.Instance.Contains(newGroupName))
                  {
                      modalAlert.Show("创建标签失败", "输入的标签名已经存在！");
                      return;
                  }
                  try
                  {
                      TagManager.Instance.Add(newGroupName);
                  }
                  catch (Exception ex)
                  {
                      modalAlert.Show("创建标签时出错", ExceptionUtils.GetExceptionMessage(ex));
                  }
                  InvokeAsync(StateHasChanged);
              }, null);
        }
        private void MoveLeft(string tag)
        {
            try
            {
                TagManager.Instance.MoveLeft(tag);
                InvokeAsync(StateHasChanged);
            }
            catch (Exception ex)
            {
                modalAlert.Show("向左移动标签时出错", ExceptionUtils.GetExceptionMessage(ex));
            }
        }

        private void MoveRight(string tag)
        {
            try
            {
                TagManager.Instance.MoveRight(tag);
                InvokeAsync(StateHasChanged);
            }
            catch (Exception ex)
            {
                modalAlert.Show("向右移动标签时出错", ExceptionUtils.GetExceptionMessage(ex));
            }
        }

        private void DeleteTag(string tag)
        {
            modalAlert.Show("删除确认", $"确定要删除标签[{tag}]？", () =>
              {
                  try
                  {
                      TagManager.Instance.Delete(tag);
                      InvokeAsync(StateHasChanged);
                  }
                  catch (Exception ex)
                  {
                      Task.Delay(100).ContinueWith(t =>
                      {
                          modalAlert.Show("删除标签时出错", ExceptionUtils.GetExceptionMessage(ex));
                      });
                  }
              });
        }
    }
}
