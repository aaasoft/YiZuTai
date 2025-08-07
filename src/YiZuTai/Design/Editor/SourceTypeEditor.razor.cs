using Newtonsoft.Json;
using Quick.Blazor.Bootstrap;
using Quick.Xml;
using YiZuTai.Components;
using YiZuTai.Source;

namespace YiZuTai.Design.Editor
{
    public partial class SourceTypeEditor : EditorBase<ISource>
    {
        private ModalWindow modalWindow;

        public static void Select(ModalWindow modalWindow, ISource source, Type sourceType, Action<ISource> setSourceAction)
        {
            ISource editValue = null;
            if (source != null)
                editValue = (ISource)XmlConvert.Deserialize(XmlConvert.Serialize(source));
            modalWindow.Show("源选择编辑", new DialogParameters<SourceSelectWindow>
            {
                { x => x.Source, editValue},
                { x => x.SourceType, sourceType},
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
            Select(modalWindow, Value, Field.PropertyInfo.PropertyType, t => Value = t);
        }

        private void onClear()
        {
            Value = null;
        }
    }
}
