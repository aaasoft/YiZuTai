using YiZuTai.Core;

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
        ControlManager.Instance.RegisterControl<Controls.Table.Model>();
        ControlManager.Instance.RegisterControl<Controls.Path.Model>();

        SourceManager.Instance.RegisterSource<DataSources.Input>();
        SourceManager.Instance.RegisterSource<DataSources.Simulate>();
        SourceManager.Instance.RegisterSource<DataSources.DateTime>();

        SourceManager.Instance.RegisterSource<HyperLinkSources.Link>();
    }
}
