using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Quick.Blazor.Bootstrap;
using YiZuTai.Components.Editors;

namespace YiZuTai.Sample.Controls.Charts.Common.Model;

[DisplayName("颜色对象")]
public class ColorItem : PropertyNotifyModel
{
    private string _Value;
    [DisplayName("值")]
    [Editor(typeof(EditorInputColor), typeof(EditorInputColor))]
    public string Value
    {
        get { return _Value; }
        set
        {
            _Value = value;
            RaisePropertyChanged(nameof(Value));
        }
    }

    private double _ValueOpacity = 1;

    [DisplayName("透明度")]
    [Range(0, 1)]
    [StringLength(100)]
    public double ValueOpacity
    {
        get { return _ValueOpacity; }
        set
        {
            _ValueOpacity = value;
            RaisePropertyChanged();
        }
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}
