using System.ComponentModel;
using YiZuTai.Design;
using YiZuTai.Design.Editor;
using YiZuTai.Design.Trigger;
using YiZuTai.Source.DataSource;

namespace YiZuTai.Control.TextBlock;

public class Trigger : BaseControl_WithBackgroundAndAnimateTrigger
{
    private string _Format;
    [Category("文本")]
    [DisplayName("格式")]
    [Description(@"变量用大括号括起来。变量列表
------------------
value   =>  当前值")]
    [DefaultValue(null)]
    public string Format
    {
        get { return _Format; }
        set
        {
            _Format = value;
            RaisePropertyChanged(nameof(Format));
        }
    }

    private string _Foreground;
    [Category("文本")]
    [DisplayName("颜色")]
    [Description("文字颜色")]
    [Editor(typeof(EditorInputColor), typeof(string))]
    public virtual string Foreground
    {
        get { return _Foreground; }
        set
        {
            _Foreground = value;
            RaisePropertyChanged(nameof(Foreground));
        }
    }

    public override void Execute(BaseControl controlModel)
    {
        base.Execute(controlModel);

        var model = (Model)controlModel;
        if (!string.IsNullOrEmpty(Format))
            model.Format = Format;
        if (!string.IsNullOrEmpty(Foreground))
            model.Foreground = Foreground;
    }
}

public class DataSourceAndTrigger : DataSourceAndTrigger<Trigger> { }