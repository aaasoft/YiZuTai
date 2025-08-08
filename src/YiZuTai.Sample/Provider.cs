using YiZuTai.Design;
using YiZuTai.Source;

namespace YiZuTai.Sample;

public class Provider
{
    public static void Register()
    {
        //使用示例控件
        ControlManager.Instance.RegisterControl<Controls.Circle.Model>();
        ControlManager.Instance.RegisterControl<Controls.Rectangle.Model>();
        ControlManager.Instance.RegisterControl<Controls.Image.Model>();
        ControlManager.Instance.RegisterControl<Controls.TextBlock.Model>();

        SourceManager.Instance.RegisterSource<DataSources.Input>();
        SourceManager.Instance.RegisterSource<DataSources.Simulate>();
        SourceManager.Instance.RegisterSource<DataSources.DateTime>();
    }
}
