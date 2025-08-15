using System.ComponentModel;
using YiZuTai.Control;

namespace YiZuTai.Sample.Controls.Video;

[Category("常用")]
[DisplayName("视频")]
public class Model : BaseControl
{
    protected override Type GetViewType() => typeof(View);
    protected override Type GetHtmlViewType() => typeof(HtmlView);

    private string _Url;
    [Category("视频")]
    [DisplayName("URL")]
    [Description("IE浏览器只支持mp4格式的视频，Chrome浏览器支持mp4,webm,ogg等多种格式的视频。")]
    public string Url
    {
        get { return _Url; }
        set
        {
            _Url = value;
            RaisePropertyChanged();
        }
    }

    private bool _Muted = true;
    [Category("视频")]
    [DisplayName("静音")]
    public bool Muted
    {
        get { return _Muted; }
        set
        {
            _Muted = value;
            RaisePropertyChanged();
        }
    }

    private bool _Loop = true;
    [Category("视频")]
    [DisplayName("循环")]
    public bool Loop
    {
        get { return _Loop; }
        set
        {
            _Loop = value;
            RaisePropertyChanged();
        }
    }

    private bool _Controls = false;
    [Category("视频")]
    [DisplayName("允许控制")]
    public bool Controls
    {
        get { return _Controls; }
        set
        {
            _Controls = value;
            RaisePropertyChanged();
        }
    }

    private bool _Autoplay = true;
    [Category("视频")]
    [DisplayName("自动播放")]
    public bool Autoplay
    {
        get { return _Autoplay; }
        set
        {
            _Autoplay = value;
            RaisePropertyChanged();
        }
    }
}