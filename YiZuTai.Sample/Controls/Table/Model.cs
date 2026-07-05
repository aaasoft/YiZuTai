using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using YiZuTai.Components.Editors;
using YiZuTai.Control;

namespace YiZuTai.Sample.Controls.Table;

[Category("图形")]
[DisplayName("表格")]
public class Model : BaseControl_WithBackground
{
    protected override Type GetViewType() => typeof(View);
    private int _RowCount = 2;
    [Category("表格")]
    [DisplayName("行数")]
    public int RowCount
    {
        get { return _RowCount; }
        set
        {
            _RowCount = value;
            RaisePropertyChanged(nameof(RowCount));
        }
    }

    private int _ColumnCount = 2;
    [Category("表格")]
    [DisplayName("列数")]
    public int ColumnCount
    {
        get { return _ColumnCount; }
        set
        {
            _ColumnCount = value;
            RaisePropertyChanged(nameof(ColumnCount));
        }
    }


    private bool _ColumnVisiable = true;
    [Category("列")]
    [DisplayName("可见")]
    public bool ColumnVisiable
    {
        get { return _ColumnVisiable; }
        set
        {
            _ColumnVisiable = value;
            RaisePropertyChanged(nameof(ColumnVisiable));
        }
    }

    private bool _FirstColumnVisiable = true;
    [Category("列")]
    [DisplayName("首列可见")]
    public bool FirstColumnVisiable
    {
        get { return _FirstColumnVisiable; }
        set
        {
            _FirstColumnVisiable = value;
            RaisePropertyChanged(nameof(FirstColumnVisiable));
        }
    }

    private bool _LastColumnVisiable = true;
    [Category("列")]
    [DisplayName("末列可见")]
    public bool LastColumnVisiable
    {
        get { return _LastColumnVisiable; }
        set
        {
            _LastColumnVisiable = value;
            RaisePropertyChanged(nameof(LastColumnVisiable));
        }
    }

    private double _ColumnWidth = 1;
    [Category("列")]
    [DisplayName("线条宽度")]
    public double ColumnWidth
    {
        get { return _ColumnWidth; }
        set
        {
            _ColumnWidth = value;
            RaisePropertyChanged(nameof(ColumnWidth));
        }
    }

    private string _ColumnLineColor = "#000000";
    [Category("列")]
    [DisplayName("线条颜色")]
    [Editor(typeof(EditorInputColor), typeof(EditorInputColor))]
    public virtual string ColumnLineColor
    {
        get { return _ColumnLineColor; }
        set
        {
            _ColumnLineColor = value;
            RaisePropertyChanged(nameof(ColumnLineColor));
        }
    }

    private double _ColumnLineOpacity = 1;
    [Category("列")]
    [DisplayName("线条透明")]
    [Editor(typeof(EditorInputRange), typeof(EditorInputRange))]
    [Range(0, 1)]
    [StringLength(100)]
    public double ColumnLineOpacity
    {
        get { return _ColumnLineOpacity; }
        set
        {
            _ColumnLineOpacity = value;
            RaisePropertyChanged();
        }
    }


    private bool _RowVisiable = true;
    [Category("行")]
    [DisplayName("可见")]
    public bool RowVisiable
    {
        get { return _RowVisiable; }
        set
        {
            _RowVisiable = value;
            RaisePropertyChanged(nameof(RowVisiable));
        }
    }


    private bool _FirstRowVisiable = true;
    [Category("行")]
    [DisplayName("首行可见")]
    public bool FirstRowVisiable
    {
        get { return _FirstRowVisiable; }
        set
        {
            _FirstRowVisiable = value;
            RaisePropertyChanged(nameof(FirstRowVisiable));
        }
    }

    private bool _LastRowVisiable = true;
    [Category("行")]
    [DisplayName("末行可见")]
    public bool LastRowVisiable
    {
        get { return _LastRowVisiable; }
        set
        {
            _LastRowVisiable = value;
            RaisePropertyChanged(nameof(LastRowVisiable));
        }
    }

    private double _RowWidth = 1;
    [Category("行")]
    [DisplayName("线条宽度")]
    public double RowWidth
    {
        get { return _RowWidth; }
        set
        {
            _RowWidth = value;
            RaisePropertyChanged(nameof(RowWidth));
        }
    }

    private string _RowLineColor = "#000000";
    [Category("行")]
    [DisplayName("线条颜色")]
    [Editor(typeof(EditorInputColor), typeof(EditorInputColor))]
    public virtual string RowLineColor
    {
        get { return _RowLineColor; }
        set
        {
            _RowLineColor = value;
            RaisePropertyChanged(nameof(RowLineColor));
        }
    }

    private double _RowLineOpacity = 1;
    [Category("行")]
    [DisplayName("线条透明")]
    [Editor(typeof(EditorInputRange), typeof(EditorInputRange))]
    [Range(0, 1)]
    [StringLength(100)]
    public double RowLineOpacity
    {
        get { return _RowLineOpacity; }
        set
        {
            _RowLineOpacity = value;
            RaisePropertyChanged();
        }
    }

    public static IControlShotcut[] Shotcuts =>
    [
        new ControlShotcut<Model>("样式-浅色", (context, t) =>
        {
            t.ColumnLineColor = t.RowLineColor = "#000000";
        }),
        new ControlShotcut<Model>("样式-深色", (context, t) =>
        {
            t.ColumnLineColor = t.RowLineColor = "#FFFFFF";
        })
    ];
}
