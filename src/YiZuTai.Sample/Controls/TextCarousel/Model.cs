using System.Collections.ObjectModel;
using System.ComponentModel;
using Newtonsoft.Json;
using YiZuTai.Control;
using YiZuTai.Model;

namespace YiZuTai.Sample.Controls.TextCarousel;

[Category("常用")]
[DisplayName("文本轮播")]
public class Model : BaseControl_WithBackground
{
    protected override Type GetViewType() => typeof(View);
    private ObservableCollection<PageInfo> _Pages = new ObservableCollection<PageInfo>();
    [Category("数据")]
    [DisplayName("页面项")]
    public ObservableCollection<PageInfo> Pages
    {
        get { return _Pages; }
        set
        {
            _Pages = value;
            RaisePropertyChanged(nameof(Pages));
        }
    }

    private int _Padding = 10;
    [Category("控件")]
    [DisplayName("内边距")]
    public int Padding
    {
        get { return _Padding; }
        set
        {
            _Padding = value;
            RaisePropertyChanged(nameof(Padding));
        }
    }

    private int _PagerWidth = 10;
    [Category("分页器")]
    [DisplayName("宽度")]
    public int PagerWidth
    {
        get { return _PagerWidth; }
        set
        {
            _PagerWidth = value;
            RaisePropertyChanged(nameof(PagerWidth));
        }
    }

    private int _PagerHeight = 2;
    [Category("分页器")]
    [DisplayName("高度")]
    public int PagerHeight
    {
        get { return _PagerHeight; }
        set
        {
            _PagerHeight = value;
            RaisePropertyChanged(nameof(PagerHeight));
        }
    }

    private int _PagerMarginInner = 9;
    [Category("分页器")]
    [DisplayName("间隔")]
    public int PagerMarginInner
    {
        get { return _PagerMarginInner; }
        set
        {
            _PagerMarginInner = value;
            RaisePropertyChanged(nameof(PagerMarginInner));
        }
    }

    private int _PagerMarginBottom = 8;
    [Category("分页器")]
    [DisplayName("下边距")]
    public int PagerMarginBottom
    {
        get { return _PagerMarginBottom; }
        set
        {
            _PagerMarginBottom = value;
            RaisePropertyChanged(nameof(PagerMarginBottom));
        }
    }

    private string _ActivePagerForeground = "#FEAB2B";
    [Category("分页器")]
    [DisplayName("焦点颜色")]
    public virtual string ActivePagerForeground
    {
        get { return _ActivePagerForeground; }
        set
        {
            _ActivePagerForeground = value;
            RaisePropertyChanged(nameof(ActivePagerForeground));
        }
    }

    private string _OtherPagerForeground = "#CFD0D2";
    [Category("分页器")]
    [DisplayName("其他颜色")]
    public virtual string OtherPagerForeground
    {
        get { return _OtherPagerForeground; }
        set
        {
            _OtherPagerForeground = value;
            RaisePropertyChanged();
        }
    }

    [JsonIgnore]
    [Browsable(false)]
    public int CurrentPageIndex { get; set; }
    [JsonIgnore]
    [Browsable(false)]
    public PageInfo CurrentPage { get; set; }
    public event EventHandler CurrentPageChanged;

    [JsonIgnore]
    [Browsable(false)]
    public DateTime LastChangePageTime { get; set; } = DateTime.Now;

    private CancellationTokenSource cts;

    public void SetCurrentPageIndex(int pageIndex)
    {
        CurrentPageIndex = pageIndex;
        CurrentPage = Pages.Skip(pageIndex).FirstOrDefault();
        CurrentPageChanged?.Invoke(this, EventArgs.Empty);
    }

    public override void OnLoad()
    {
        cts = new();
        var cancellationToken = cts.Token;
        SetCurrentPageIndex(0);

        Task.Run(async () =>
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                if (CurrentPage == null)
                {
                    SetCurrentPageIndex(0);
                    await Task.Delay(1000);
                    continue;
                }
                await Task.Delay(CurrentPage.Timeout);
                CurrentPageIndex++;
                if (CurrentPageIndex >= Pages.Count)
                    CurrentPageIndex = 0;
                SetCurrentPageIndex(CurrentPageIndex);
            }
        });
    }

    public override void OnUnload()
    {
        cts?.Cancel();
    }

    public override void OnDragDropCreate(Point location)
    {
        Width = 120;
        Height = 120;
        Background = "#09214B";
        BackgroundOpacity = 1;
        Pages = new ObservableCollection<PageInfo>()
        {
            new PageInfo()
            {
                    Items=new ObservableCollection<ItemInfo>()
                    {
                        new ItemInfo()
                        {
                            Format = "指标1: {value}",
                            Value =new DataSources.Simulate(){ MinValue=0,MaxValue = 100, Period=1 }
                        },
                        new ItemInfo()
                        {
                            Format = "指标2: {value}",
                            Value =new DataSources.Simulate(){ MinValue=0,MaxValue = 100, Period=1 }
                        },
                        new ItemInfo()
                        {
                            Format = "指标3: {value}",
                            Value =new DataSources.Simulate(){ MinValue=0,MaxValue = 100, Period=1 }
                        }
                    }
            },
            new PageInfo()
            {
                    Items=new ObservableCollection<ItemInfo>()
                    {
                        new ItemInfo()
                        {
                            Format = "指标4: {value}",
                            Value =new DataSources.Simulate(){ MinValue=0,MaxValue = 100, Period=1 }
                        },
                        new ItemInfo()
                        {
                            Format = "指标5: {value}",
                            Value =new DataSources.Simulate(){ MinValue=0,MaxValue = 100, Period=1 }
                        }
                    }
            },
            new PageInfo()
            {
                    Items=new ObservableCollection<ItemInfo>()
                    {
                        new ItemInfo()
                        {
                            Format = "指标6: {value}",
                            Value =new DataSources.Simulate(){ MinValue=0,MaxValue = 100, Period=1 }
                        }
                    }
            }
        };
    }

    public static IControlShotcut[] Shotcuts =>
    [
        new ControlShotcut<Model>("样式-浅色", t =>
        {
            t.Background = "#F8F8F8";
            foreach (var page in t.Pages)
                foreach (var item in page.Items)
                    item.Foreground = "#000000";
        }),
        new ControlShotcut<Model>("样式-深色", t =>
        {
            t.Background = "#09214B";
            foreach (var page in t.Pages)
                foreach (var item in page.Items)
                    item.Foreground = "#FFFFFF";
        })
    ];
}
