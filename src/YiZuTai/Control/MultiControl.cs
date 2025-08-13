using System;
using System.ComponentModel;
using Newtonsoft.Json;
using YiZuTai.HyperLinkSource;

namespace YiZuTai.Control;

public class MultiControl : BaseControl
{
    private Canvas canvas;
    [Browsable(false)]
    public List<BaseControl> Items { get; set; } = new List<BaseControl>();
    private bool isFromXml;
    private bool isGroup = false;

    public override bool IsMultiControl => true;
    public override bool IsGroupControl => isGroup;

    public MultiControl() { isFromXml = true; }
    public MultiControl(Canvas canvas, IEnumerable<BaseControl> items)
    {
        isFromXml = false;
        this.canvas = canvas;
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

/*
    public override double Angle
    {
        get => (double)getValue(nameof(Angle), 0D);
        set => setValue(nameof(Angle), value);
    }

    public override bool Visiable
    {
        get => (bool)getValue(nameof(Visiable), true);
        set => setValue(nameof(Visiable), value);
    }

    public override double Opacity
    {
        get => (double)getValue(nameof(Opacity), 1D);
        set => setValue(nameof(Opacity), value);
    }

    public override bool IsLocked
    {
        get => (bool)getValue(nameof(IsLocked), false);
        set => setValue(nameof(IsLocked), value);
    }
    */

    protected override Type GetViewType() { throw new NotImplementedException(); }
        [JsonIgnore]
    [Browsable(false)]
    public override double Left
    {
        get => base.Left;
        set
        {
            var offset = value - base.Left;
            base.Left = value;
            foreach (var item in Items)
                item.SetLeftToGrid(canvas,item.Left + offset);
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
            foreach (var item in Items)
                item.SetTopToGrid(canvas,item.Top + offset);
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
            foreach (var item in Items)
                item.SetWidthToGrid(canvas,item.Width + offset);
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
            foreach (var item in Items)
                item.SetHeightToGrid(canvas,item.Height + offset);
        }
    }
}
