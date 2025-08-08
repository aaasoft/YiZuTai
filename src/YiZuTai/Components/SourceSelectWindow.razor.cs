using System.ComponentModel;
using System.Reflection;
using Microsoft.AspNetCore.Components;
using Quick.Blazor.Bootstrap;
using YiZuTai.Core;
using YiZuTai.Core.ObjectConnection;
using YiZuTai.Design;
using YiZuTai.Source;

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
            foreach (var attr in attrs)
            {
                if (Source != null && Source.GetType().FullName == attr.Clazz)
                    list.Add(Source);
                else
                    list.Add((ISource)Activator.CreateInstance(attr.Type));
            }
            Sources = list.ToArray();
            _SelectedCategory = value;
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
