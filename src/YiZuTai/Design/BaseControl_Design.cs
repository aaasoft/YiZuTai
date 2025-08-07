using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using YiZuTai.Components;
using YiZuTai.Design.Trigger;
using YiZuTai.Source.DataSource;

namespace YiZuTai.Design
{
    public partial class BaseControl
    {
        //让控件左边距对齐到网格
        public void SetLeftToGrid(Canvas canvas, double newLeft)
        {
            newLeft += canvas.DesignGridSize / 2;
            if (newLeft >= 0)
                newLeft -= newLeft % canvas.DesignGridSize;
            else
                newLeft += newLeft % canvas.DesignGridSize;

            Left = newLeft;
        }
        //让控件上边距对齐到网格
        public void SetTopToGrid(Canvas canvas, double newTop)
        {
            newTop += canvas.DesignGridSize / 2;
            if (newTop >= 0)
                newTop -= newTop % canvas.DesignGridSize;
            else
                newTop += newTop % canvas.DesignGridSize;

            Top = newTop;
        }
        //让控件宽度对齐到网格
        public void SetWidthToGrid(Canvas canvas, double newWidth)
        {
            newWidth -= newWidth % canvas.DesignGridSize;
            if (newWidth <= 0)
                newWidth = canvas.DesignGridSize;

            Width = newWidth;
        }

        //让控件高度对齐到网格
        public void SetHeightToGrid(Canvas canvas, double newHeight)
        {
            newHeight -= newHeight % canvas.DesignGridSize;
            if (newHeight <= 0)
                newHeight = canvas.DesignGridSize;
            Height = newHeight;
        }

        /// <summary>
        /// 与Display属性有关系的属性列表
        /// </summary>
        /// <returns></returns>
        public virtual string[] GetHasBussinesToDispayProperties() => [nameof(Opacity)];

        [Browsable(false)]
        [JsonIgnore]
        public IControlView View { get; set; }

        /// <summary>
        /// 当数据源的值改变时
        /// </summary>
        public virtual void OnDataSourceValueChanged(Source.DataSource.AbstractAutoRefreshDataSource t, string propertyName)
        {
            if (View == null)
                return;
            View?.Refresh();
            if (View.DesignMode)
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

        protected virtual Dictionary<IDataSource, ControlDataTrigger[]> GetDataSourceTriggerDict() => null;

        /// <summary>
        /// 当拖放创建时
        /// </summary>
        public virtual void OnDragDropCreate(Point location)
        {

        }
    }
}
