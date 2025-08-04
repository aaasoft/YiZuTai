using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YiZuTai.Design.Editor
{
    public abstract class EditorBase : ComponentBase
    {
        /// <summary>
        /// 组态文件模型
        /// </summary>
        [Parameter]
        public Core.ZTFile ZTFile { get; set; }
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
