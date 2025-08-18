using Microsoft.AspNetCore.Components.Web;
using YiZuTai.Control;
using YiZuTai.Enums;

namespace YiZuTai.Components
{
    public partial class Designer
    {
        //鼠标按下时控件信息
        public ControlMeta MouseDownControlMeta { get; set; }

        private void onMouseUp(MouseEventArgs e)
        {
            if (MouseDownControlMeta == null)
                return;
            if (MouseDownControlMeta.MoveAction == MoveAction.Selecting)
            {
                var rect = MouseDownControlMeta.MoveRect;

                //获取框选的控件
                var selectedItems = Project.Controls
                .Where(t => !t.IsLocked && t.Visiable
                    && rect.Contains(t.LeftTopPointLocation)
                    && rect.Contains(t.RightTopPointLocation)
                    && rect.Contains(t.RightBottomPointLocation)
                    && rect.Contains(t.LeftBottomPointLocation))
                    .ToList();

                if (e.CtrlKey)
                {
                    if (selectedItems.Count == 0)
                        return;
                    //如果当前没有选中
                    if (SelectedItem == null
                        || SelectedItem == Project)
                    {
                        if (selectedItems.Count == 1)
                            SelectedItem = selectedItems[0];
                        else
                            SelectedItem = new MultiControl(Project, Project.DesignGridSize, selectedItems);
                        return;
                    }
                    //如果当前选中了多个控件
                    if (IsSelectedMutiControl)
                    {
                        var container = (MultiControl)SelectedItem;
                        foreach (var model in selectedItems)
                        {
                            if (container.Items.Contains(model))
                                container.RemoveControl(model);
                            else
                                container.AddControl(model);
                        }
                        if (container.Items.Count == 0)
                        {
                            SelectedItem = Project;
                        }
                        else if (container.Items.Count == 1)
                        {
                            SelectedItem = container.Items.FirstOrDefault();
                        }
                        else
                        {
                            SelectedItem = container;
                        }
                        return;
                    }
                    //当前选中的是普通控件
                    if (selectedItems.Contains(SelectedItem))
                        selectedItems.Remove(SelectedItem);
                    else
                        selectedItems.Add(SelectedItem);
                    if (selectedItems.Count == 0)
                        return;
                    else if (selectedItems.Count == 1)
                        SelectedItem = selectedItems.FirstOrDefault();
                    else
                        SelectedItem = new MultiControl(Project, Project.DesignGridSize, selectedItems);
                }
                else
                {
                    if (selectedItems.Count == 0)
                        SelectedItem = Project;
                    else if (selectedItems.Count == 1)
                        SelectedItem = selectedItems[0];
                    else
                    {
                        SelectedItem = new MultiControl(Project, Project.DesignGridSize, selectedItems);
                    }
                }
            }
            MouseDownControlMeta = null;
        }

        public Point getMouseLocation_RelativeToCanvas(MouseEventArgs e)
        {
            var x = e.OffsetX;
            var y = e.OffsetY;
            if (DesignScale != 100)
            {
                x /= DesignScale * 1D / 100;
                y /= DesignScale * 1D / 100;
            }
            return new Point(x, y);
        }
    }
}
