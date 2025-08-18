using System;
using System.ComponentModel;
using Newtonsoft.Json;
using YiZuTai.HyperLinkSource;

namespace YiZuTai.Control;

public class MultiControl : BaseControl
{
    private int GridSize;
    [Browsable(false)]
    public BaseControl Container { get; private set; }

    [Browsable(false)]
    public List<BaseControl> Items { get; set; } = new List<BaseControl>();
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
        base.Left = Items.Min(t => t.Left);
        base.Top = Items.Min(t => t.Top);
        base.Width = Items.Max(t => t.RightTopPointLocation.X) - base.Left;
        base.Height = Items.Max(t => t.RightBottomPointLocation.Y) - base.Top;

        var groups = Items.GroupBy(t => t.GroupName).ToArray();
        isGroup = groups.Length == 1 && groups[0].Key != null;
        RaisePropertyChanged(nameof(IsGroupControl));
    }

    public void AddControl(BaseControl item)
    {
        if (Items.Contains(item))
            return;
        Items.Add(item);
        RaisePropertyChanged(nameof(Name));
        onItemsChanged();
    }

    public void RemoveControl(BaseControl item)
    {
        if (!Items.Contains(item))
            return;
        Items.Remove(item);
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
                return $"组合[{Items.Count}个控件]";
            else
                return $"选中{Items.Count}个控件";
        }
        set { }
    }

    [JsonIgnore]
    [Browsable(false)]
    public override IHyperLinkSource HyperLinkSource { get => base.HyperLinkSource; set => base.HyperLinkSource = value; }

    protected override Type GetViewType() { throw new NotImplementedException(); }

    private void travelItems(Action<BaseControl> handler)
    {
        foreach (var item in Items)
            handler(item);
    }

    public override double Angle
    {
        get => base.Angle;
        set
        {
            base.Angle = value;
            travelItems(item => item.Angle = value);
        }
    }

    public override double Opacity
    {
        get => base.Opacity;
        set
        {
            base.Opacity = value;
            travelItems(item => item.Opacity = value);
        }
    }

    public override bool Visiable
    {
        get => base.Visiable;
        set
        {
            base.Visiable = value;
            travelItems(item => item.Visiable = value);
        }
    }

    public override bool IsLocked
    {
        get => base.IsLocked;
        set
        {
            base.IsLocked = value;
            travelItems(item => item.IsLocked = value);
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
            travelItems(item => item.SetLeftToGrid(GridSize, item.Left + offset));
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
            travelItems(item => item.SetTopToGrid(GridSize, item.Top + offset));
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
            travelItems(item => item.SetWidthToGrid(GridSize, item.Width + offset));
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
            travelItems(item => item.SetHeightToGrid(GridSize, item.Height + offset));
        }
    }
}
