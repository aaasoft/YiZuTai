using Microsoft.AspNetCore.Components;
using YiZuTai.Control;
using YiZuTai.Model;

namespace YiZuTai.Components.Editors
{
    public abstract class EditorBase : ComponentBase
    {
        /// <summary>
        /// 组态文件模型
        /// </summary>
        [Parameter]
        public ZTFile ZTFile { get; set; }
        /// <summary>
        /// 控件字段
        /// </summary>
        [Parameter]
        public ControlField Field { get; set; }
    }

    public abstract class EditorBase<T> : EditorBase
    {
        public virtual T Value
        {
            get
            {
                var obj = Field.Value;
                return (T)obj;
            }
            set { Field.Value = value; }
        }
    }
}
