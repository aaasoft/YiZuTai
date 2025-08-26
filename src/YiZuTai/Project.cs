using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using Newtonsoft.Json;
using YiZuTai.Components.Editors;
using YiZuTai.Control;
using YiZuTai.Core;
using YiZuTai.Enums;
using YiZuTai.HyperLinkSource;
using YiZuTai.Model;
using YiZuTai.Utils;

namespace YiZuTai
{
    public class Project : BaseControl_WithBackground
    {
        public override bool IsContainer => true;
        public override ObservableCollection<BaseControl> Controls { get; set; } = new ObservableCollection<BaseControl>();
        [JsonIgnore]
        [Browsable(false)]
        public override string Name => "画布";

        [JsonIgnore]
        [Browsable(false)]
        public override double Left
        {
            get { return 0; }
            set { }
        }

        [JsonIgnore]
        [Browsable(false)]
        public override double Top
        {
            get { return 0; }
            set { }
        }

        [JsonIgnore]
        [Browsable(false)]
        public override double Angle
        {
            get { return 0; }
            set { }
        }

        [JsonIgnore]
        [Browsable(false)]
        public override bool IsLocked
        {
            get { return false; }
            set { }
        }

        [JsonIgnore]
        [Browsable(false)]
        public override IHyperLinkSource HyperLinkSource { get; set; }

        //前景
        private string _BodyColor = "#A9A9A9";
        [Category("网页")]
        [DisplayName("颜色")]
        [Editor(typeof(EditorInputColor), typeof(EditorInputColor))]
        public virtual string BodyColor
        {
            get { return _BodyColor; }
            set
            {
                _BodyColor = value;
                RaisePropertyChanged();
            }
        }

        private string _Image;
        [Category("背景")]
        [DisplayName("图片")]
        [Description("画布上要显示的图片")]
        [Editor(typeof(EditorInputImage), typeof(EditorInputImage))]
        public string Image
        {
            get { return _Image; }
            set
            {
                _Image = value;
                RaisePropertyChanged();
            }
        }

        private Stretch _Stretch = Stretch.UniformToFill;
        [Category("公共")]
        [DisplayName("填充方式")]
        [Description(@"
[居中]内容保持其原始大小。
[拉伸]调整内容大小以填充目标尺寸。 不保留纵横比。
[适应]在保留内容原有纵横比的同时调整内容的大小，以适合目标尺寸。
[填充]在保留内容原有纵横比的同时调整内容的大小，以填充目标尺寸。 如果目标矩形的纵横比不同于源矩形的纵横比，则对源内容进行剪裁以适合目标尺寸。")]
        public Stretch Stretch
        {
            get { return _Stretch; }
            set
            {
                _Stretch = value;
                RaisePropertyChanged();
            }
        }
        /// <summary>
        /// 设计网格大小
        /// </summary>
        [Browsable(false)]
        public int DesignGridSize { get; set; } = 1;
        /// <summary>
        /// 设计网格的ViewPort
        /// </summary>
        [Browsable(false)]
        [JsonIgnore]
        public Rect DesignGridViewPort => new Rect(DesignGridSize, DesignGridSize, DesignGridSize, DesignGridSize);

        [Browsable(false)]
        [JsonIgnore]
        public string DesignGridColor
        {
            get
            {
                var foreground = ColorUtils.ParseColor(BodyColor);
                return ColorUtils.GetRGBString(Color.FromArgb(
                        0,
                        (byte)(255 - foreground.R),
                        (byte)(255 - foreground.G),
                        (byte)(255 - foreground.B)
                ));
            }
        }

        public Project()
        {
            Width = 800;
            Height = 600;

            BodyColor = "#000000";
            Background = "#0F0F34";
            BackgroundOpacity = 1;
        }

        protected override Type GetViewType()
        {
            throw new NotImplementedException();
        }

        private IEnumerable<BaseControl> GetControlAllControls(BaseControl control)
        {
            if (control.Controls != null)
            {
                foreach (var subControl in control.Controls)
                {
                    yield return subControl;
                    foreach (var tempControl in GetControlAllControls(subControl))
                        yield return tempControl;
                }
            }
        }

        public IEnumerable<BaseControl> GetAllControls() => GetControlAllControls(this);

        public IEnumerable<string> GetAllReferenceJsFiles()
        {
            var fileHashSet = new HashSet<string>();
            foreach (var control in GetAllControls())
            {
                var files = control.RefrenceJsFiles;
                if (files == null)
                    continue;
                foreach (var file in files)
                {
                    if (fileHashSet.Contains(file))
                        continue;
                    fileHashSet.Add(file);
                }
            }
            return fileHashSet;
        }



        public void CheckAndModifyControlName(BaseControl control)
        {
            var controlAttr = ControlManager.Instance.GetItem(control.GetType().FullName);
            var index = 0;
            var controls = GetAllControls().ToArray();
            do
            {
                index++;
                var name = controlAttr.Name + index;
                if (controls.Any(t => t.Name == name))
                    continue;
                control.Name = name;
                break;
            }
            while (true);
        }

        [Browsable(false)]
        [JsonIgnore]
        public bool IsProjectLoaded { get; set; } = false;

        private string _StyleShotcut = "样式-深色";
        [Browsable(false)]
        public string StyleShotcut
        {
            get { return _StyleShotcut; }
            set
            {
                _StyleShotcut = value;
                RaisePropertyChanged(nameof(StyleShotcut));
                if (IsProjectLoaded)
                    ApplyStyleShotcut();
            }
        }

        public void ApplyStyleShotcut()
        {
            var list = new List<BaseControl>();
            list.Add(this);
            list.AddRange(GetAllControls());
            foreach (var control in list)
                ApplyStyleShotcut(control);
        }

        public void ApplyStyleShotcut(BaseControl control)
        {
            var shotcut = control.Shotcuts?.FirstOrDefault(t => t.Name == StyleShotcut);
            if (shotcut == null)
                return;
            shotcut.ExecuteCommand.Execute(null);
        }

        public override void OnLoad()
        {
            foreach (var control in GetAllControls())
                control.OnLoad();
        }

        public override void OnUnload()
        {
            foreach (var control in GetAllControls())
                control.OnUnload();
        }

        public override IControlShotcut[] Shotcuts =>
        [
            new ControlShotcut("样式-浅色", ()=>
            {
                Background = "#FFFFFF";
                BodyColor = "#A9A9A9";
            }),
            new ControlShotcut("样式-深色", ()=>
            {
                Background = "#0F0F34";
                BodyColor = "#000000";
            }),
            new ControlShotcut("尺寸-小(800*600)", ()=>
            {
                Width = 800;
                Height = 600;
            }),
            new ControlShotcut("尺寸-中(1200*600)", ()=>
            {
                Width = 1200;
                Height = 600;
            }),
            new ControlShotcut("尺寸-大(1920*1080)", ()=>
            {
                Width = 1920;
                Height = 1080;
            })
        ];
    }
}
