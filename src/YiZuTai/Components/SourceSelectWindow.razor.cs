using System.ComponentModel;
using System.Reflection;
using Microsoft.AspNetCore.Components;
using Quick.Blazor.Bootstrap;
using YiZuTai.Core;
using YiZuTai.Core.ObjectConnection;
using YiZuTai.Control;
using YiZuTai.Core.Source;
using Quick.Blazor.Bootstrap.Utils;
using YiZuTai.Utils;
using Quick.Xml;
using Microsoft.JSInterop;

namespace YiZuTai.Components;

public partial class SourceSelectWindow : ComponentBase
{
    public class SourceInfo
    {
        public string Name { get; set; }
        public ISource Source { get; set; }
    }
    private ModalAlert modalAlert;

    public string[] SourceCategorys { get; set; }
    private string _SelectedCategory;
    public string SelectedCategory
    {
        get => _SelectedCategory;
        set
        {
            var attrs = sourceAttributeDict[value];
            var list = new List<ISource>();
            var currentCategoryContainsSource = false;
            foreach (var attr in attrs)
            {
                if (Source != null && Source.GetType().FullName == attr.Clazz)
                {
                    currentCategoryContainsSource = true;
                    list.Add(Source);
                }
                else
                {
                    list.Add((ISource)Activator.CreateInstance(attr.Type));
                }
            }
            Sources = list.ToArray();
            _SelectedCategory = value;
            if (!currentCategoryContainsSource)
                SelectedSourceId = Sources.FirstOrDefault()?.GetType().FullName;
        }
    }
    private List<SourceInfo> SourceInfoList { get; set; }
    private ISource[] Sources { get; set; }
    private Dictionary<string, SourceAttribute[]> sourceAttributeDict;
    private string SelectedSourceId
    {
        get { return Source?.GetType().FullName; }
        set
        {
            if (string.IsNullOrEmpty(value))
                Source = null;
            else
                Source = Sources?.FirstOrDefault(t => t.GetType().FullName == value);
        }
    }
    
    public ControlField PropertyGridSelectedField { get; set; }
    private string PropertyGridSelectedFieldDescriptionStyle => (PropertyGridSelectedField == null || string.IsNullOrEmpty(PropertyGridSelectedField.Description)) ? "display: none" : "display: inline";


    [Parameter]
    public ISource Source { get; set; }
    [Parameter]
    public Type SourceType { get; set; }
    [Parameter]
    public Action<ISource> OkAction { get; set; }


    [Inject]
    public IJSRuntime JSRuntime { get; set; }

    protected override void OnParametersSet()
    {
        sourceAttributeDict = SourceManager.Instance.GetItems(SourceType)
            .GroupBy(t => t.Category)
            .ToDictionary(t => t.Key, t => t.ToArray());
        SourceCategorys = sourceAttributeDict.Select(t => t.Key).ToArray();
        if (Source == null)
            SelectedCategory = SourceCategorys.FirstOrDefault();
        else
            SelectedCategory = SourceManager.Instance.GetItem(Source.GetType().FullName).Category;

        if (Source != null)
        {
            var info = ObjectConnectionManager.Instance.GetParent(Source);
            if (info?.NotifyObject != null)
            {
                SourceInfoList = new List<SourceInfo>();
                AddSourceInfoToList(info.NotifyObject, SourceInfoList);
                if (SourceInfoList.Count == 0)
                    SourceInfoList = null;
            }
        }
    }

    private async Task Copy()
    {
        try
        {
            var content = XmlConvert.Serialize(Source);
            await ClipboardUtils.WriteText(JSRuntime,content);
        }
        catch (Exception ex)
        {
            modalAlert.Show("错误", ExceptionUtils.GetExceptionMessage(ex));
        }
    }

    private async Task Paste()
    {
        try
        {
            
            var content = await ClipboardUtils.ReadText(JSRuntime);
            if (string.IsNullOrEmpty(content))
                throw new IOException("剪贴板为空");
            var obj = XmlConvert.Deserialize(content);
            var currentType = obj.GetType();

            if (!SourceType.IsAssignableFrom(currentType))
            {
                throw new IOException("剪贴板中的内容不是当前源类型");
            }
            var source = (ISource)obj;
            SelectedCategory = SourceManager.Instance.GetItem(currentType.FullName).Category;
            Source = source;
        }
        catch (Exception ex)
        {
            modalAlert.Show("错误", ExceptionUtils.GetExceptionMessage(ex));
        }
    }

    private void Ok()
    {
        OkAction?.Invoke(Source);
    }

    private void AddSourceInfoToList(object notifyObj, List<SourceInfo> list)
    {
        var notifyObjType = notifyObj.GetType();
        foreach (var pi in notifyObjType.GetProperties())
        {
            if (!typeof(ISource).IsAssignableFrom(pi.PropertyType))
                continue;
            var otherSource = pi.GetValue(notifyObj) as ISource;
            if (otherSource == null || otherSource == Source)
                continue;
            List<string> nameList = new List<string>();
            var categoryNameAttr = pi.GetCustomAttribute<CategoryAttribute>();
            if (categoryNameAttr != null)
                nameList.Add(categoryNameAttr.Category);
            var displayNameAttr = pi.GetCustomAttribute<DisplayNameAttribute>();
            if (displayNameAttr != null)
                nameList.Add(displayNameAttr.DisplayName);
            if (nameList.Count == 0)
                continue;
            list.Add(new SourceInfo()
            {
                Name = string.Join(" - ", nameList),
                Source = otherSource
            });
        }
    }
}
