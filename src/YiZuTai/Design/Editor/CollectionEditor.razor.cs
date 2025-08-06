using Newtonsoft.Json;
using Quick.Blazor.Bootstrap;
using YiZuTai.Components;

namespace YiZuTai.Design.Editor;

public partial class CollectionEditor
{
    private ModalWindow modalWindow;

    private void onClick()
    {
        var valueType = Value.GetType().GetGenericArguments().FirstOrDefault();
        modalWindow.Show($"集合编辑 - {Field.Name}", new DialogParameters<CollectionEditControl>
        {
            { x => x.ValueList, JsonConvert.DeserializeObject(JsonConvert.SerializeObject(Value),Field.PropertyInfo.PropertyType)},
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
