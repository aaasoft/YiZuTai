using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Newtonsoft.Json;
using YiZuTai.HyperLinkSource;

namespace YiZuTai.Control;

public class MultiControl : BaseControl
{
    private int GridSize;
    [JsonIgnore]
    [Browsable(false)]
    public BaseControl Container { get; private set; }
    public override ObservableCollection<BaseControl> Controls { get; set; } = new();

    private bool isGroup = false;

    public override bool IsMultiControl => true;
    public override bool IsGroupControl => isGroup;

    public MultiControl() { }
    public MultiControl(BaseControl container, int gridSize, IEnumerable<BaseControl> items)
    {
        Container = container;
        GridSize = gridSize;
        foreach (var item in items)
            AddControl(item);
        onItemsChanged();
    }

    private void onItemsChanged()
    {
        base.Left = Controls.Min(t => t.Left);
        base.Top = Controls.Min(t => t.Top);
        base.Width = Controls.Max(t => t.RightTopPointLocation.X) - base.Left;
        base.Height = Controls.Max(t => t.RightBottomPointLocation.Y) - base.Top;

        var groups = Controls.GroupBy(t => t.GroupName).ToArray();
        isGroup = groups.Length == 1 && groups[0].Key != null;
        RaisePropertyChanged(nameof(IsGroupControl));
    }

    public void AddControl(BaseControl item)
    {
        if (Controls.Contains(item))
            return;
        Controls.Add(item);
        RaisePropertyChanged(nameof(Name));
        onItemsChanged();
    }

    public void RemoveControl(BaseControl item)
    {
        if (!Controls.Contains(item))
            return;
        Controls.Remove(item);
        RaisePropertyChanged(nameof(Name));
        onItemsChanged();
    }

    [JsonIgnore]
    [Browsable(false)]
    public override string Name
    {
        get
        {
            if (isGroup)
                return $"组合[{Controls.Count}个控件]";
            else
                return $"选中{Controls.Count}个控件";
        }
        set { }
    }

    [JsonIgnore]
    [Browsable(false)]
    public override IHyperLinkSource HyperLinkSource { get => base.HyperLinkSource; set => base.HyperLinkSource = value; }

    protected override Type GetViewType() { throw new NotImplementedException(); }

    public void TravelControls(Action<BaseControl> handler)
    {
        foreach (var item in Controls)
            handler(item);
    }

    public override double Angle
    {
        get => base.Angle;
        set
        {
            base.Angle = value;
            TravelControls(item => item.Angle = value);
        }
    }

    public override double Opacity
    {
        get => base.Opacity;
        set
        {
            base.Opacity = value;
            TravelControls(item => item.Opacity = value);
        }
    }

    public override bool Visiable
    {
        get => base.Visiable;
        set
        {
            base.Visiable = value;
            TravelControls(item => item.Visiable = value);
        }
    }

    public override bool IsLocked
    {
        get => base.IsLocked;
        set
        {
            base.IsLocked = value;
            TravelControls(item => item.IsLocked = value);
        }
    }

    [JsonIgnore]
    [Browsable(false)]
    public override double Left
    {
        get => base.Left;
        set
        {
            var offset = value - base.Left;
            base.Left = value;
            TravelControls(item => item.SetLeftToGrid(GridSize, item.Left + offset));
        }
    }

    [JsonIgnore]
    [Browsable(false)]
    public override double Top
    {
        get => base.Top;
        set
        {
            var offset = value - base.Top;
            base.Top = value;
            TravelControls(item => item.SetTopToGrid(GridSize, item.Top + offset));
        }
    }
    [JsonIgnore]
    [Browsable(false)]
    public override double Width
    {
        get => base.Width;
        set
        {
            var offset = value - base.Width;
            base.Width = value;
            TravelControls(item => item.SetWidthToGrid(GridSize, item.Width + offset));
        }
    }
    [JsonIgnore]
    [Browsable(false)]
    public override double Height
    {
        get => base.Height;
        set
        {
            var offset = value - base.Height;
            base.Height = value;
            TravelControls(item => item.SetHeightToGrid(GridSize, item.Height + offset));
        }
    }


    public void AlignToLeft()
    {
        var left = Controls.Min(t => t.Left);
        TravelControls(t => t.Left = left);
        onItemsChanged();
    }

    public void AlignToRight()
    {
        var right = Controls.Max(t => t.Left + t.Width);
        TravelControls(t => t.Left = right - t.Width);
        onItemsChanged();
    }

    public void AlignToTop()
    {
        var top = Controls.Min(t => t.Top);
        TravelControls(t => t.Top = top);
        onItemsChanged();
    }

    public void AlignToBottom()
    {
        var bottom = Controls.Max(t => t.Top + t.Height);
        TravelControls(t => t.Top = bottom - t.Height);
        onItemsChanged();
    }
}
