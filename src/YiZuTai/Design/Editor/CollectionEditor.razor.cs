using Quick.Blazor.Bootstrap;
using Quick.Xml;
using System.Collections;
using YiZuTai.Components;

namespace YiZuTai.Design.Editor;

public class ClassForClone
{
    public IList Value { get; set; }
}

public partial class CollectionEditor
{
    private ModalWindow modalWindow;

    private void onClick()
    {
        var valueType = Value.GetType().GetGenericArguments().FirstOrDefault();
        var cloneItem = (ClassForClone)XmlConvert.Deserialize(XmlConvert.Serialize(new ClassForClone() { Value = Value }));
        modalWindow.Show($"集合编辑 - {Field.Name}", new DialogParameters<CollectionEditControl>
        {
            { x => x.ValueList, cloneItem.Value},
            { x => x.ValueType, valueType},
            { x => x.OkAction, value=>
                {
                    Field.Value = value;
                    modalWindow.Close();
                }
            }
        });
    }
}
