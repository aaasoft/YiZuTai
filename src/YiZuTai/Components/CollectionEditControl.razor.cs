using System.Collections;
using Microsoft.AspNetCore.Components;
using YiZuTai.Design;

namespace YiZuTai.Components;

public partial class CollectionEditControl
{
    [Parameter]
    public IList ValueList { get; set; }
    [Parameter]
    public Type ValueType { get; set; }

    private object SelectedItem { get; set; }
    private ControlField PropertyGridSelectedField;

    private void btnAdd_Click()
    {
        var newItem = Activator.CreateInstance(ValueType);
        ValueList.Add(newItem);
        SelectedItem = newItem;
    }

    private void btnDelete_Click()
    {
        if (SelectedItem == null)
            return;
        ValueList.Remove(SelectedItem);
        SelectedItem = null;
    }

    private void onItem_Click(object item)
    {
        SelectedItem = item;
        PropertyGridSelectedField = null;
    }

    private void btnMoveUp_Click()
    {
        if (SelectedItem == null)
            return;
        var index = ValueList.IndexOf(SelectedItem);
        index--;
        if (index < 0)
            return;
        ValueList.Remove(SelectedItem);
        ValueList.Insert(index, SelectedItem);
    }

    private void btnMoveDown_Click()
    {
        if (SelectedItem == null)
            return;
        var index = ValueList.IndexOf(SelectedItem);
        index++;
        if (index >= ValueList.Count)
            return;
        ValueList.Remove(SelectedItem);
        ValueList.Insert(index, SelectedItem);
    }
}
