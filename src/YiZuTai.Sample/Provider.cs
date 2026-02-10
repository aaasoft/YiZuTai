using YiZuTai.Core;

namespace YiZuTai.Sample;

public class Provider
{
    public static void Register()
    {
        ControlManager.Instance.RegisterControlShotcut(Project.Shotcuts);
        //使用示例控件
        ControlManager.Instance.RegisterControl<Controls.Panel.Model>();
        ControlManager.Instance.RegisterControl<Controls.GroupBox.Model>();
        ControlManager.Instance.RegisterControlShotcut(Controls.GroupBox.Model.Shotcuts);
        ControlManager.Instance.RegisterControl<Controls.Circle.Model>();
        ControlManager.Instance.RegisterControlShotcut(Controls.Circle.Model.Shotcuts);
        ControlManager.Instance.RegisterControl<Controls.Rectangle.Model>();
        ControlManager.Instance.RegisterControlShotcut(Controls.Rectangle.Model.Shotcuts);
        ControlManager.Instance.RegisterControl<Controls.Image.Model>();
        ControlManager.Instance.RegisterControl<Controls.TextBlock.Model>();
        ControlManager.Instance.RegisterControlShotcut(Controls.TextBlock.Model.Shotcuts);
        ControlManager.Instance.RegisterControl<Controls.Table.Model>();
        ControlManager.Instance.RegisterControlShotcut(Controls.Table.Model.Shotcuts);
        ControlManager.Instance.RegisterControl<Controls.Path.Model>();
        ControlManager.Instance.RegisterControl<Controls.Curve.Model>();
        ControlManager.Instance.RegisterControl<Controls.WaterCurrent.Model>();
        ControlManager.Instance.RegisterControl<Controls.TempAndHum.Model>();
        ControlManager.Instance.RegisterControl<Controls.Web.Model>();
        ControlManager.Instance.RegisterControl<Controls.Video.Model>();
        ControlManager.Instance.RegisterControl<Controls.TextCarousel.Model>();
        ControlManager.Instance.RegisterControlShotcut(Controls.TextCarousel.Model.Shotcuts);
        ControlManager.Instance.RegisterControl<Controls.TextArea.Model>();
        ControlManager.Instance.RegisterControlShotcut(Controls.TextArea.Model.Shotcuts);
        ControlManager.Instance.RegisterControl<Controls.Progress.HorizontalBar.Model>();
        ControlManager.Instance.RegisterControl<Controls.Progress.VerticalBar.Model>();
        ControlManager.Instance.RegisterControl<Controls.Progress.HalfRing.Model>();
        ControlManager.Instance.RegisterControlShotcut(Controls.Progress.HalfRing.Model.Shotcuts);
        ControlManager.Instance.RegisterControl<Controls.Progress.Ring.Model>();
        ControlManager.Instance.RegisterControlShotcut(Controls.Progress.Ring.Model.Shotcuts);
        ControlManager.Instance.RegisterControl<Controls.Charts.Pie.Model>();
        ControlManager.Instance.RegisterControlShotcut(Controls.Charts.Pie.Model.Shotcuts);
        ControlManager.Instance.RegisterControl<Controls.Charts.Line.Model>();
        ControlManager.Instance.RegisterControlShotcut(Controls.Charts.Line.Model.Shotcuts);
        ControlManager.Instance.RegisterControl<Controls.Charts.Column_Single.Model>();
        ControlManager.Instance.RegisterControlShotcut(Controls.Charts.Column_Single.Model.Shotcuts);
        ControlManager.Instance.RegisterControl<Controls.Charts.Column_Multi.Model>();
        ControlManager.Instance.RegisterControlShotcut(Controls.Charts.Column_Multi.Model.Shotcuts);
        ControlManager.Instance.RegisterControl<Controls.Charts.Column_Report.Model>();
        ControlManager.Instance.RegisterControlShotcut(Controls.Charts.Column_Report.Model.Shotcuts);

        SourceManager.Instance.RegisterSource<DataSources.Input>();
        SourceManager.Instance.RegisterSource<DataSources.Simulate>();
        SourceManager.Instance.RegisterSource<DataSources.DateTime>();
        SourceManager.Instance.RegisterSource<DataSources.DayOfWeek>();
        SourceManager.Instance.RegisterSource<DataSources.DayOfYear>();

        SourceManager.Instance.RegisterSource<QueryDataSources.Simulate>();

        SourceManager.Instance.RegisterSource<HyperLinkSources.Link>();
        SourceManager.Instance.RegisterSource<HyperLinkSources.MultiLink>();
    }
}
