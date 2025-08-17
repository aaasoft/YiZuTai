using System.ComponentModel;
using Newtonsoft.Json;
using YiZuTai.Trigger;
using YiZuTai.DataSource;
using System.Reflection;

namespace YiZuTai.Control
{
    public partial class BaseControl
    {
        public virtual bool IsMultiControl => false;
        public virtual bool IsGroupControl => false;
        [Browsable(false)]
        public virtual string GroupName { get; set; }

        /// <summary>
        /// 是否能够旋转
        /// </summary>
        [JsonIgnore]
        [Browsable(false)]
        public bool CanRotate
        {
            get { return GetType().GetProperty(nameof(Angle)).GetCustomAttribute<JsonIgnoreAttribute>() == null && GroupName == null; }
        }

        [JsonIgnore]
        [Browsable(false)]
        public virtual bool IsContainer => false;

        //让控件左边距对齐到网格
        public void SetLeftToGrid(Project canvas, double newLeft)
        {
            newLeft += canvas.DesignGridSize / 2;
            if (newLeft >= 0)
                newLeft -= newLeft % canvas.DesignGridSize;
            else
                newLeft += newLeft % canvas.DesignGridSize;

            Left = newLeft;
        }
        //让控件上边距对齐到网格
        public void SetTopToGrid(Project canvas, double newTop)
        {
            newTop += canvas.DesignGridSize / 2;
            if (newTop >= 0)
                newTop -= newTop % canvas.DesignGridSize;
            else
                newTop += newTop % canvas.DesignGridSize;

            Top = newTop;
        }
        //让控件宽度对齐到网格
        public void SetWidthToGrid(Project canvas, double newWidth)
        {
            newWidth -= newWidth % canvas.DesignGridSize;
            if (newWidth <= 0)
                newWidth = canvas.DesignGridSize;

            Width = newWidth;
        }

        //让控件高度对齐到网格
        public void SetHeightToGrid(Project canvas, double newHeight)
        {
            newHeight -= newHeight % canvas.DesignGridSize;
            if (newHeight <= 0)
                newHeight = canvas.DesignGridSize;
            Height = newHeight;
        }

        [Browsable(false)]
        [JsonIgnore]
        public IControlView View { get; set; }

        /// <summary>
        /// 当数据源的值改变时
        /// </summary>
        public virtual void OnDataSourceValueChanged(IDataSource t, string propertyName)
        {
            if (View == null)
                return;
            View?.Refresh();
            if (View.ZTContext.DesignMode)
                return;
            var dict = GetDataSourceTriggerDict();
            if (dict == null || dict.Count == 0)
                return;
            if (!dict.TryGetValue(t, out var triggers))
                return;
            foreach (var trigger in triggers)
            {
                if (trigger.IsMatchValue(t.Value))
                    trigger.Execute(this);
            }
        }

        protected virtual IEnumerable<Tuple<IDataSource, AbstractTrigger[]>> GetDataSourceAndTriggers() => null;

        private Dictionary<IDataSource, AbstractTrigger[]> cachedDataSourceTriggerDict = null;
        private Dictionary<IDataSource, AbstractTrigger[]> GetDataSourceTriggerDict()
        {
            if (cachedDataSourceTriggerDict == null)
            {
                cachedDataSourceTriggerDict = new();
                var items = GetDataSourceAndTriggers();
                if (items != null)
                    foreach (var item in items)
                    {
                        if (item.Item1 == null || item.Item2.Length == 0)
                            continue;
                        cachedDataSourceTriggerDict[item.Item1] = item.Item2;
                    }
            }
            return cachedDataSourceTriggerDict;
        }


        /// <summary>
        /// 当双击时
        /// </summary>
        public void OnDoubleClick()
        {
            View.OnDoubleClick();
        }

        /// <summary>
        /// 当拖放创建时
        /// </summary>
        public virtual void OnDragDropCreate(Point location)
        {

        }

        public virtual string GetIcon()
        {
            var type = this.GetType();
            var assembly = type.Assembly;
            return $"../../../_content/{assembly.GetName().Name}/images/{type.Namespace}.svg";
        }
    }
}
