using System;
using System.ComponentModel;
using Newtonsoft.Json;
using YiZuTai.Design;
using YiZuTai.Design.Editor;
using YiZuTai.Design.Enums;

namespace YiZuTai.Control.TextBlock;

[Control(
    Category = "常用",
    Name = "文本",
    Stretch = Stretch.None
    )]
public class Model : BaseControl_WithBackground
{
    //默认为黑色
    private string _Foreground = "#000000";
    [Category("文本")]
    [DisplayName("颜色")]
    [Description("文字颜色")]
    [Editor(typeof(EditorInputColor), typeof(string))]
    public virtual string Foreground
    {
        get { return _Foreground; }
        set
        {
            _Foreground = value;
            RaisePropertyChanged(nameof(Foreground));
        }
    }

    private string _Format = "{value}";
    [Category("文本")]
    [DisplayName("格式")]
    [Description(@"变量用大括号括起来。变量列表
------------------
value   =>  当前值")]
    [DefaultValue("{value}")]
    public string Format
    {
        get { return _Format; }
        set
        {
            _Format = value;
            RaisePropertyChanged(nameof(Format));
            //RaisePropertyChanged(nameof(Text));
        }
    }

    private double _FontSize = 24;
    [Category("文本")]
    [DisplayName("字号")]
    [Description("文本的字号")]
    [DefaultValue(24D)]
    public virtual double FontSize
    {
        get { return _FontSize; }
        set
        {
            _FontSize = value;
            RaisePropertyChanged(nameof(FontSize));
        }
    }

    //默认为宋体
    private string _FontFamily = "Microsoft YaHei";
    [Category("文本")]
    [DisplayName("字体")]
    [Description("文字的字体")]
    //[ItemsSource(typeof(FontFamilyItemsSource))]
    public virtual string FontFamily
    {
        get { return _FontFamily; }
        set
        {
            _FontFamily = value;
            RaisePropertyChanged(nameof(FontFamily));
        }
    }

    private TextAnchor _TextAnchor = TextAnchor.middle;
    [Category("文本")]
    [DisplayName("对齐")]
    [Description("文字的对齐方式")]
    //[ItemsSource(typeof(TextAnchorItemSource))]
    public virtual TextAnchor TextAnchor
    {
        get { return _TextAnchor; }
        set
        {
            _TextAnchor = value;
            RaisePropertyChanged(nameof(TextAnchor));
        }
    }

    private TextFontWeight _FontWeight = TextFontWeight.normal;
    [Category("文本")]
    [DisplayName("粗细")]
    [Description("文字的粗细")]
    //[ItemsSource(typeof(TextFontWeightItemSource))]
    public virtual TextFontWeight FontWeight
    {
        get { return _FontWeight; }
        set
        {
            _FontWeight = value;
            RaisePropertyChanged(nameof(FontWeight));
        }
    }

    private double _TextVerticalOffset = 0.5;
    [Category("文本")]
    [DisplayName("垂直偏移量")]
    [Description(@"当值在0~1之间时，偏移量为比例
当值大于1时，偏移量为像素")]
    public virtual double TextVerticalOffset
    {
        get { return _TextVerticalOffset; }
        set
        {
            if (value < 0)
                return;
            _TextVerticalOffset = value;
            RaisePropertyChanged(nameof(TextVerticalOffset));
        }
    }

    private string _Image;
    [Category("背景")]
    [DisplayName("背景图片")]
    [Description("背景上要显示的图片")]
    [Editor(typeof(Design.Editor.EditorInputImage), typeof(string))]
    [DefaultValue(null)]
    public string Image
    {
        get { return _Image; }
        set
        {
            _Image = value;
            RaisePropertyChanged(nameof(Image));
        }
    }

    /*
        private IDataSource _Value;
        [Category("文本")]
        [DisplayName("值")]
        [Description("文本块控件要显示的值")]
        [Editor(typeof(App.Core.TypeEditors.SourceTypeEditor), typeof(App.Core.TypeEditors.SourceTypeEditor))]
        public IDataSource Value
        {
            get { return _Value; }
            set
            {
                RaisePropertyChanging(nameof(Value));
                _Value = value;
                RaisePropertyChanged(nameof(Value));
                RaisePropertyChanged(nameof(Text));
            }
        }

        [JsonIgnore]
        [Browsable(false)]
        public string Text => XmlConvert.Escape(Format.Replace("{value}", Value?.Value ?? ""));
    */

    private double _RotateSpeed = 0;
    [Category("动画")]
    [DisplayName("旋转速度")]
    [Description("图片旋转的速度")]
    public double RotateSpeed
    {
        get { return _RotateSpeed; }
        set
        {
            _RotateSpeed = value;
            RaisePropertyChanged(nameof(RotateSpeed));
        }
    }

    private int _FlashInterval = 0;
    [Category("动画")]
    [DisplayName("闪烁间隔")]
    [Description("图片闪烁的间隔，单位：毫秒")]
    public int FlashInterval
    {
        get { return _FlashInterval; }
        set
        {
            _FlashInterval = value;
            RaisePropertyChanged(nameof(FlashInterval));
        }
    }

    [JsonIgnore]
    public double TextX
    {
        get
        {
            var cx = Width / 2;
            double textX = 0;
            switch (TextAnchor)
            {
                case TextAnchor.start:
                    textX = 0;
                    break;
                case TextAnchor.middle:
                    textX = cx;
                    break;
                case TextAnchor.end:
                    textX = Width;
                    break;
            }
            return textX;
        }
    }

    [JsonIgnore]
    public double TextY
    {
        get
        {
            var cy = Height / 2;
            double textY = 0;
            if (TextVerticalOffset > 1)
                textY = TextVerticalOffset;
            else
                textY = (Height - FontSize) * TextVerticalOffset + FontSize * 7 / 8;
            return textY;
        }
    }

    /*
        public override object GetExtraObject()
        {
            var cx = Width / 2;
            var cy = Height / 2;
            double textY = 0;
            if (TextVerticalOffset > 1)
                textY = TextVerticalOffset;
            else
                textY = (Height - FontSize) * TextVerticalOffset + FontSize * 7 / 8;
            double textX = 0;
            switch (TextAnchor)
            {
                case TextAnchor.start:
                    textX = 0;
                    break;
                case TextAnchor.middle:
                    textX = cx;
                    break;
                case TextAnchor.end:
                    textX = Width;
                    break;
            }
            return new
            {
                cx,
                cy,
                textX,
                textY,
                text = Text
            };
        }
        */

    /*
    public override void OnDataSourceValueChanged(AbstractAutoRefreshDataSource t, string propertyName)
    {
        if (CachedDisplay == null)
        {
            base.OnDataSourceValueChanged(t, propertyName);
            return;
        }
        RaisePropertyChanged(nameof(Text));
    }
    */

    /*
    public override void OnDoubleClick()
    {
        showDataSourceWindow(Value, t => Value = t);
    }
    */

    /*
    public override void OnDragDropCreate(Point location)
    {
        base.OnDragDropCreate(location);
        Value = new DataSource.Input() { Content = this.Name ?? Attribute.Name };
    }
    */
}