using Microsoft.AspNetCore.Components;
using System.ComponentModel;
using System.Reflection;
using YiZuTai.Components.Editors;
using YiZuTai.Core;

namespace YiZuTai.Design
{
    /// <summary>
    /// 控件字段
    /// </summary>
    public class ControlField
    {
        /// <summary>
        /// 字段名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 属性信息
        /// </summary>
        public PropertyInfo PropertyInfo { get; set; }
        /// <summary>
        /// 属性宿主
        /// </summary>
        public object PropertyHost { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public object Value
        {
            get { return PropertyInfo.GetValue(PropertyHost); }
            set { PropertyInfo.SetValue(PropertyHost, value); }
        }

        public RenderFragment GetEditorRenderFragment(Core.ZTFile ztFile)
        {
            var editorAttribute = PropertyInfo.GetCustomAttribute<EditorAttribute>();
            Type editorType = null;
            if (editorAttribute != null)
                editorType = Type.GetType(editorAttribute.EditorTypeName);
            else
                editorType = EditorManager.Instance.GetEditorType(PropertyInfo.PropertyType);
            return Quick.Blazor.Bootstrap.Utils.BlazorUtils.GetRenderFragment(editorType, new Dictionary<string, object>()
            {
                [nameof(EditorBase.Field)] = this,
                [nameof(EditorBase.ZTFile)] = ztFile
            });
        }
    }
}
