using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using YiZuTai.Components.Editors;
using YiZuTai.Control;
using YiZuTai.Core;

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
    public string Url
    {
        get { return _Url; }
        set
        {
            _Url = value;
            RaisePropertyChanged();
        }
    }


    private string _LocalFile;
    [Category("视频")]
    [DisplayName("本地文件")]
    [Editor(typeof(EditorInputFile), typeof(EditorInputFile))]
    [FileExtensions(Extensions = "*.mp4,*.webm,*.ogv,*.avi")]
    public string LocalFile
    {
        get { return _LocalFile; }
        set
        {
            _LocalFile = value;
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
    public new bool Controls
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
    
    private bool _VideoFill = true;
    [Category("视频")]
    [DisplayName("视频拉伸")]
    public bool VideoFill
    {
        get { return _VideoFill; }
        set
        {
            _VideoFill = value;
            RaisePropertyChanged();
        }
    }
}