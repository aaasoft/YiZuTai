using System.ComponentModel;
using Newtonsoft.Json;
using YiZuTai.Control;

namespace YiZuTai.Sample.Controls.Web;

[Category("常用")]
[DisplayName("网页")]
public class Model : BaseControl
{
    protected override Type GetViewType() => typeof(View);
    protected override Type GetHtmlViewType() => typeof(HtmlView);

    [JsonIgnore]
    [Browsable(false)]
    public override double Angle { get => base.Angle; set { } }

    private string _Url;
    [Category("数据")]
    [DisplayName("链接")]
    public virtual string Url
    {
        get { return _Url; }
        set
        {
            _Url = value;
            RaisePropertyChanged();
        }
    }

    private double _Zoom = 1;
    [Category("外观")]
    [DisplayName("缩放比例")]
    [Description("如果想要显示更多的网页内容，就设置为大于1；如果想要显示更少的网页内容，就设置为0~1之间的值")]
    public double Zoom
    {
        get { return _Zoom; }
        set
        {
            if (value <= 0)
                return;
            _Zoom = value;
            RaisePropertyChanged();
        }
    }

    public override void OnDragDropCreate(Point location)
    {
        Width = 400;
        Height = 300;
        Url = "http://www.baidu.com";
    }
}
