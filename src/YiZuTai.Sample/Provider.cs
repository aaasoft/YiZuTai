using YiZuTai.Core;

namespace YiZuTai.Sample;

public class Provider
{
    public static void Register()
    {
        //使用示例控件
        ControlManager.Instance.RegisterControl<Controls.Panel.Model>();
        ControlManager.Instance.RegisterControl<Controls.GroupBox.Model>();
        ControlManager.Instance.RegisterControl<Controls.Circle.Model>();
        ControlManager.Instance.RegisterControl<Controls.Rectangle.Model>();
        ControlManager.Instance.RegisterControl<Controls.Image.Model>();
        ControlManager.Instance.RegisterControl<Controls.TextBlock.Model>();
        ControlManager.Instance.RegisterControl<Controls.Table.Model>();
        ControlManager.Instance.RegisterControl<Controls.Path.Model>();
        ControlManager.Instance.RegisterControl<Controls.Curve.Model>();
        ControlManager.Instance.RegisterControl<Controls.WaterCurrent.Model>();
        ControlManager.Instance.RegisterControl<Controls.TempAndHum.Model>();
        ControlManager.Instance.RegisterControl<Controls.Web.Model>();
        ControlManager.Instance.RegisterControl<Controls.Video.Model>();
        ControlManager.Instance.RegisterControl<Controls.TextCarousel.Model>();
        ControlManager.Instance.RegisterControl<Controls.TextArea.Model>();
        ControlManager.Instance.RegisterControl<Controls.Progress.HorizontalBar.Model>();
        ControlManager.Instance.RegisterControl<Controls.Progress.VerticalBar.Model>();
        ControlManager.Instance.RegisterControl<Controls.Progress.HalfRing.Model>();

        SourceManager.Instance.RegisterSource<DataSources.Input>();
        SourceManager.Instance.RegisterSource<DataSources.Simulate>();
        SourceManager.Instance.RegisterSource<DataSources.DateTime>();
        SourceManager.Instance.RegisterSource<DataSources.DayOfWeek>();
        SourceManager.Instance.RegisterSource<DataSources.DayOfYear>();

        SourceManager.Instance.RegisterSource<HyperLinkSources.Link>();
    }
}
