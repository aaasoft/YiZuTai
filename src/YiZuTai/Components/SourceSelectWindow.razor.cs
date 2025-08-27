using Microsoft.AspNetCore.Components;
using Quick.Blazor.Bootstrap;
using YiZuTai.Core;
using YiZuTai.Core.Source;
using Quick.Blazor.Bootstrap.Utils;
using Quick.Xml;

namespace YiZuTai.Components;

public partial class SourceSelectWindow : ComponentBase
{
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
                if (initSource != null && initSource.GetType().FullName == attr.Clazz)
                {
                    currentCategoryContainsSource = true;
                    list.Add(initSource);
                }
                else
                {
                    list.Add((ISource)Activator.CreateInstance(attr.Type));
                }
            }
            Sources = list.ToArray();
            _SelectedCategory = value;
            if (currentCategoryContainsSource)
                SelectedSourceId = initSource.GetType().FullName;
            else
                SelectedSourceId = Sources.FirstOrDefault()?.GetType().FullName;
        }
    }
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

    private ISource initSource;
    [Parameter]
    public ISource Source { get; set; }
    [Parameter]
    public Type SourceType { get; set; }
    [Parameter]
    public Action<ISource> OkAction { get; set; }
    [Parameter]
    public IClipboard Clipboard { get; set; }
    
    protected override void OnParametersSet()
    {
        sourceAttributeDict = SourceManager.Instance.GetItems(SourceType)
            .GroupBy(t => t.Category)
            .ToDictionary(t => t.Key, t => t.ToArray());
        SourceCategorys = sourceAttributeDict.Select(t => t.Key).ToArray();
        initSource = Source;
        if (Source == null)
            SelectedCategory = SourceCategorys.FirstOrDefault();
        else
            SelectedCategory = SourceManager.Instance.GetItem(Source.GetType().FullName).Category;
    }

    private void Copy()
    {
        try
        {
            var content = XmlConvert.Deserialize(XmlConvert.Serialize(Source));
            Clipboard.SetContent(content);
        }
        catch (Exception ex)
        {
            modalAlert.Show("错误", ExceptionUtils.GetExceptionMessage(ex));
        }
    }

    private bool IsClipboardContentMatch()
    {
        var obj = Clipboard.GetContent();
        if (obj == null)
            return false;
        var currentType = obj.GetType();
        return SourceType.IsAssignableFrom(currentType);
    }

    private void Paste()
    {
        try
        {
            var obj = Clipboard.GetContent();
            if (obj == null)
                throw new IOException("剪贴板为空");
            var currentType = obj.GetType();
            if (!SourceType.IsAssignableFrom(currentType))
            {
                throw new IOException("剪贴板中的内容不是当前源类型");
            }
            var source = (ISource)XmlConvert.Deserialize(XmlConvert.Serialize(obj));
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
}
