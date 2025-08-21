using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using YiZuTai.Components.Editors;
using YiZuTai.Control;
using YiZuTai.DataSource;
using YiZuTai.Model;

namespace YiZuTai.Sample.Controls.Progress
{
    public abstract class AbstractControl : BaseControl_WithBackground
    {
        private double _MaxValue = 100;
        [Category("数据")]
        [DisplayName("最大值")]
        public double MaxValue
        {
            get { return _MaxValue; }
            set
            {
                _MaxValue = value;
                RaisePropertyChanged();
            }
        }

        private double _MinValue = 0;
        [Category("数据")]
        [DisplayName("最小值")]
        public double MinValue
        {
            get { return _MinValue; }
            set
            {
                _MinValue = value;
                RaisePropertyChanged();
            }
        }

        private IDataSource _Value;
        [Category("数据")]
        [DisplayName("值")]
        public IDataSource Value
        {
            get { return _Value; }
            set
            {
                RaisePropertyChanging();
                _Value = value;
                RaisePropertyChanged();
            }
        }

        private string _ProgressForeground = "#35AA47";
        [Category("前景")]
        [DisplayName("颜色")]
        [Editor(typeof(EditorInputColor), typeof(EditorInputColor))]
        public virtual string ProgressForeground
        {
            get { return _ProgressForeground; }
            set
            {
                _ProgressForeground = value;
                RaisePropertyChanged(nameof(ProgressForeground));
            }
        }

        private double _ProgressForegroundOpacity = 1;
        [Category("前景")]
        [DisplayName("透明度")]
        [Range(0, 1)]
        [StringLength(100)]
        public double ProgressForegroundOpacity
        {
            get { return _ProgressForegroundOpacity; }
            set
            {
                _ProgressForegroundOpacity = value;
                RaisePropertyChanged();
            }
        }

        private bool _TextVisiable = true;
        [Category("文本")]
        [DisplayName("显示")]
        public bool TextVisiable
        {
            get { return _TextVisiable; }
            set
            {
                _TextVisiable = value;
                RaisePropertyChanged();
            }
        }

        private string _TextFormat = "{value}";
        [Category("文本")]
        [DisplayName("格式")]
        [Description(@"变量用大括号括起来。变量列表
------------------
value   =>  当前值")]
        public string TextFormat
        {
            get { return _TextFormat; }
            set
            {
                _TextFormat = value;
                RaisePropertyChanged();
            }
        }

        private double _TextFontSize = 24;
        [Category("文本")]
        [DisplayName("字号")]
        public virtual double TextFontSize
        {
            get { return _TextFontSize; }
            set
            {
                _TextFontSize = value;
                RaisePropertyChanged();
            }
        }

        //默认为黑色
        private string _TextForeground = "#000000";
        [Category("文本")]
        [DisplayName("颜色")]
        [Editor(typeof(EditorInputColor), typeof(EditorInputColor))]
        public virtual string TextForeground
        {
            get { return _TextForeground; }
            set
            {
                _TextForeground = value;
                RaisePropertyChanged(nameof(TextForeground));
            }
        }

        private double _TextForegroundOpacity = 1;
        [Category("文本")]
        [DisplayName("透明度")]
        [Editor(typeof(EditorInputRange), typeof(EditorInputRange))]
        [Range(0, 1)]
        [StringLength(100)]
        public double TextForegroundOpacity
        {
            get { return _TextForegroundOpacity; }
            set
            {
                _TextForegroundOpacity = value;
                RaisePropertyChanged();
            }
        }

        //默认为宋体
        private string _TextFontFamily;
        [Category("文本")]
        [DisplayName("字体")]
        public virtual string TextFontFamily
        {
            get { return _TextFontFamily; }
            set
            {
                _TextFontFamily = value;
                RaisePropertyChanged();
            }
        }

        public override void OnDragDropCreate(Point location)
        {
            base.OnDragDropCreate(location);
            BackgroundOpacity = 1;
            BorderThickness = 0;
            Value = new DataSources.Simulate() { MaxValue = 100, MinValue = 0, Period = 1 };
        }

        [Browsable(false)]
        [JsonIgnore]
        public double NumberValue
        {
            get
            {
                double value = MinValue;
                double.TryParse(Value?.Value, out value);
                if (value > MaxValue)
                    value = MaxValue;
                if (value < MinValue)
                    value = MinValue;
                return value;
            }
        }

        [Browsable(false)]
        [JsonIgnore]
        public double CenterX => Width / 2;
        [Browsable(false)]
        [JsonIgnore]
        public double CenterY => Height / 2;
        [Browsable(false)]
        [JsonIgnore]
        public double TextX => CenterX;
        [Browsable(false)]
        [JsonIgnore]
        public double TextY => CenterY + (TextFontSize / 2) - (TextFontSize / 8);
        [Browsable(false)]
        [JsonIgnore]
        public string Text => TextFormat.Replace("{value}", Value?.Value ?? "");
    }
}
