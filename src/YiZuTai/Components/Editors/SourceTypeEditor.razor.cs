using Microsoft.AspNetCore.Components;
using Quick.Blazor.Bootstrap;
using Quick.Xml;
using YiZuTai.Core;
using YiZuTai.Core.Source;

namespace YiZuTai.Components.Editors
{
    public partial class SourceTypeEditor : EditorBase<ISource>
    {
        private ModalWindow modalWindow;

        [CascadingParameter]
        public IClipboard Clipboard { get; set; }

        public static void Select(ModalWindow modalWindow, IClipboard clipboard, ISource source, Type sourceType, Action<ISource> setSourceAction)
        {
            ISource editValue = null;
            if (source != null)
                editValue = (ISource)XmlConvert.Deserialize(XmlConvert.Serialize(source));
            modalWindow.Show("源选择编辑", new DialogParameters<SourceSelectWindow>
            {
                { x => x.Source, editValue},
                { x => x.SourceType, sourceType},
                { x => x.Clipboard, clipboard},
                { x=> x.OkAction, source=>
                    {
                        setSourceAction(source);
                        modalWindow.Close();
                    }
                }
            });
        }

        private void onClick()
        {
            Select(modalWindow, Clipboard, Value, Field.PropertyInfo.PropertyType, t => Value = t);
        }

        private void onClear()
        {
            Value = null;
        }
    }
}
