using System.ComponentModel;
using YiZuTai.Components.Editors;
using YiZuTai.Control;
using YiZuTai.DataSource;
using YiZuTai.Trigger;

namespace YiZuTai.Sample.Controls.Image;

public class Trigger : BaseControl_WithBackgroundAndAnimateTrigger
{
    private string _Image;
    [Category("背景")]
    [DisplayName("背景图片")]
    [Description("背景上要显示的图片")]
    [Editor(typeof(EditorInputImage), typeof(EditorInputImage))]
    [DefaultValue(null)]
    public string Image
    {
        get { return _Image; }
        set
        {
            _Image = value;
            RaisePropertyChanged(nameof(Image));
        }
    }


    public override void Execute(BaseControl controlModel)
    {
        base.Execute(controlModel);

        var model = (Model)controlModel;
        if (!string.IsNullOrEmpty(Image))
            model.Image = Image;
    }
}

public class DataSourceAndTrigger : DataSourceAndTrigger<Trigger> { }