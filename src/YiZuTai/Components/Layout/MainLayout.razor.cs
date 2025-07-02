using Microsoft.AspNetCore.Components;
using Quick.Blazor.Bootstrap;

namespace YiZuTai.Components.Layout;

public partial class MainLayout : LayoutComponentBase, IPageNavigater
{
    private NavMenu navMenu;
    public string Message { get; private set; }

    public string ActiveKey => throw new NotImplementedException();

    public void Navigate(string activeKey, Type componentType, Dictionary<string, object> parameterDict)
    {
        //更改菜单选中状态
        if (!string.IsNullOrEmpty(activeKey))
            navMenu.ChangeActiveKey(activeKey);
        //跳转到页面
        parameterDict[nameof(IPageNavigater)] = this;
        Body = Quick.Blazor.Bootstrap.Utils.BlazorUtils.GetRenderFragment(componentType, parameterDict);
        StateHasChanged();
    }

    private void Show<T>()
    {
        Body = Quick.Blazor.Bootstrap.Utils.BlazorUtils.GetRenderFragment<T>(new Dictionary<string, object>()
        {
            [nameof(IPageNavigater)] = this
        });
    }
}
