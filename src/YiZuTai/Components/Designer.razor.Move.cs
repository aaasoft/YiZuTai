using Microsoft.AspNetCore.Components.Web;
using YiZuTai.Control;
using YiZuTai.Enums;

namespace YiZuTai.Components
{
    public partial class Designer
    {
        //鼠标按下时控件信息
        public ControlMeta MouseDownControlMeta{ get; set; }

        private Dictionary<Func<double, bool>, string> cursorDict = new Dictionary<Func<double, bool>, string>()
        {
            [t => t > 337.5 || t <= 22.5] = "n-resize",
            [t => t > 22.5 && t <= 67.5] = "ne-resize",
            [t => t > 67.5 && t <= 112.5] = "e-resize",
            [t => t > 112.5 && t <= 157.5] = "se-resize",
            [t => t > 157.5 && t <= 202.5] = "s-resize",
            [t => t > 202.5 && t <= 247.5] = "sw-resize",
            [t => t > 247.5 && t <= 292.5] = "w-resize",
            [t => t > 292.5 && t <= 337.5] = "nw-resize"
        };

        private string getSizerCursor(double startAngle)
        {
            double? angle = SelectedItem.Angle;
            if (angle == null)
                return null;
            var finalAngle = angle.Value + startAngle;
            while (finalAngle < 0)
                finalAngle += 360;
            finalAngle = finalAngle % 360;
            foreach (var item in cursorDict)
            {
                var key = item.Key;
                if (key(finalAngle))
                    return item.Value;
            }
            return null;
        }

        /// <summary>
        /// 调整模型大小
        /// </summary>
        /// <param name="baseLine">参考线</param>
        /// <param name="model">模型</param>
        /// <param name="startAngle">起始角度</param>
        /// <param name="endAngle">结束角度</param>
        /// <param name="isPositiveRange">是否正区间</param>
        /// <param name="getValueFunc">获取大小值</param>
        /// <param name="setValueAction">设置大小值</param>
        private static void resizeModel(
            Line baseLine,
            BaseControl model,
            Point preLocation,
            Point newLocation,
            double startAngle,
            double endAngle,
            bool isPositiveRange,
            Func<BaseControl, double> getValueFunc,
            Action<BaseControl, double> setValueAction)
        {
            var offset = newLocation.X - preLocation.X;
            if (!isPositiveRange)
                offset = 0 - offset;

            if (model.Angle == startAngle)
                setValueAction(model, getValueFunc(model) + offset);
            else if (model.Angle == endAngle)
                setValueAction(model, getValueFunc(model) - offset);
            else
            {
                var d = baseLine.ComputePointToLineVector(newLocation);
                var isAngleInRangle = model.Angle > startAngle && model.Angle < endAngle;

                if (isPositiveRange)
                {
                    if (isAngleInRangle)
                        d = 0 - d;
                }
                else
                {
                    if (!isAngleInRangle)
                        d = 0 - d;
                }
                setValueAction(model, d);
            }
        }

        //让基准点位置不变
        private void makeBasePointLocationNotChange(BaseControl preModel, Func<BaseControl, Point> getBasePointFunc)
        {
            var model = SelectedItem;
            var preBasePointLocation = getBasePointFunc(preModel);
            var newBasePointLocation = getBasePointFunc(model);

            model.SetLeftToGrid(Project, model.Left + preBasePointLocation.X - newBasePointLocation.X);
            model.SetTopToGrid(Project, model.Top + preBasePointLocation.Y - newBasePointLocation.Y);
        }

        private void onCanvasMouseDown(MouseEventArgs e)
        {
            SelectedItem = Project;
            sizerMouseDown(e, MoveAction.Selecting);
        }
        
        private void onControlMouseDown(MouseEventArgs e, BaseControl control)
        {
            if (MouseDownControlMeta != null)
                return;

            SelectedItem = control;
            sizerMouseDown(e, MoveAction.MoveLocation);
        }

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
                            SelectedItem = new MultiControl(Project, selectedItems);
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
                        SelectedItem = new MultiControl(Project, selectedItems);
                }
                else
                {
                    if (selectedItems.Count == 0)
                        SelectedItem = Project;
                    else if (selectedItems.Count == 1)
                        SelectedItem = selectedItems[0];
                    else
                    {
                        SelectedItem = new MultiControl(Project, selectedItems);
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

        private void onMouseMove(MouseEventArgs e)
        {
            //如果没有按下鼠标
            if (MouseDownControlMeta == null)
                return;

            var newLocation = getMouseLocation_RelativeToCanvas(e);
            if (MouseDownControlMeta.MoveAction == MoveAction.Selecting)
            {
                MouseDownControlMeta.SetEndLocation(newLocation);
                return;
            }
            //如果没有选中控件
            if (!IsSelectedControl)
                return;

            var offsetX = newLocation.X - MouseDownControlMeta.StartLocation.X;
            var offsetY = newLocation.Y - MouseDownControlMeta.StartLocation.Y;
            if (offsetX == 0 && offsetY == 0)
                return;

            var model = SelectedItem;
            switch (MouseDownControlMeta.MoveAction)
            {
                case MoveAction.MoveLocation:
                    var preLeft = model.Left;
                    var preTop = model.Top;
                    model.SetLeftToGrid(Project, MouseDownControlMeta.Left + offsetX);
                    model.SetTopToGrid(Project, MouseDownControlMeta.Top + offsetY);
                    model.MoveLocationOffset = new Point(model.Left - preLeft, model.Top - preTop);
                    break;
                case MoveAction.Rotate:
                    var centerPointLocation = model.CenterPointLocation;
                    var a = newLocation.X - centerPointLocation.X;
                    var b = newLocation.Y - centerPointLocation.Y;
                    var tanValue = a / b;
                    var newAngle = 0 - Math.Atan(tanValue) * 180 / Math.PI;

                    if (b >= 0)
                        newAngle += 180;
                    while (newAngle < 0)
                        newAngle += 360;
                    newAngle = newAngle % 360;
                    //磁吸角度
                    var magnetAngle = 10;
                    var x = newAngle % 90;
                    if (Math.Abs(x) < magnetAngle)
                    {
                        newAngle -= x;
                    }
                    else if (Math.Abs(x) > (90 - magnetAngle))
                    {
                        newAngle -= x - 90;
                    }
                    model.MoveAngleOffset = newAngle - model.Angle;
                    model.Angle = newAngle;
                    break;
                default:
                    Func<BaseControl, Point> getBasePointFunc = null;
                    switch (MouseDownControlMeta.MoveAction)
                    {
                        case MoveAction.ResizeLeftTop:
                            {
                                //设置右边线为参考线1
                                var preBaseLine1 = new Line(MouseDownControlMeta.RightTopPointLocation, MouseDownControlMeta.RightBottomPointLocation);
                                //设置下边线为参考线2
                                var preBaseLine2 = new Line(MouseDownControlMeta.LeftBottomPointLocation, MouseDownControlMeta.RightBottomPointLocation);

                                var newWidth = model.Width;
                                var newHeight = model.Height;
                                //调整大小
                                resizeModel(preBaseLine1, model, MouseDownControlMeta.StartLocation, newLocation, 0, 180, false, t => MouseDownControlMeta.Width, (t, v) => newWidth = v);
                                resizeModel(preBaseLine2, model, MouseDownControlMeta.StartLocation, newLocation, 90, 270, true, t => MouseDownControlMeta.Height, (t, v) => newHeight = v);
                                //设置默认参考点为右下角参考点
                                getBasePointFunc = resizeModel_FormCorner(model, newWidth, newHeight, t => t.RightBottomPointLocation, e);
                                break;
                            }
                        case MoveAction.ResizeTop:
                            {
                                //设置下边线为参考线1
                                var preBaseLine1 = new Line(MouseDownControlMeta.LeftBottomPointLocation, MouseDownControlMeta.RightBottomPointLocation);
                                //调整大小
                                resizeModel(preBaseLine1, model, MouseDownControlMeta.StartLocation, newLocation, 90, 270, true, t => MouseDownControlMeta.Height, (t, v) => t.SetHeightToGrid(Project, v));
                                //设置参考点为左下角参考点
                                getBasePointFunc = t => t.LeftBottomPointLocation;
                                break;
                            }
                        case MoveAction.ResizeRightTop:
                            {
                                //设置下边线为参考线1
                                var preBaseLine1 = new Line(MouseDownControlMeta.LeftBottomPointLocation, MouseDownControlMeta.RightBottomPointLocation);
                                //设置左边线为参考线2
                                var preBaseLine2 = new Line(MouseDownControlMeta.LeftTopPointLocation, MouseDownControlMeta.LeftBottomPointLocation);

                                var newWidth = model.Width;
                                var newHeight = model.Height;
                                //调整大小
                                resizeModel(preBaseLine2, model, MouseDownControlMeta.StartLocation, newLocation, 0, 180, true, t => MouseDownControlMeta.Width, (t, v) => newWidth = v);
                                resizeModel(preBaseLine1, model, MouseDownControlMeta.StartLocation, newLocation, 90, 270, true, t => MouseDownControlMeta.Height, (t, v) => newHeight = v);
                                //设置默认参考点为左下角参考点
                                getBasePointFunc = resizeModel_FormCorner(model, newWidth, newHeight, t => t.LeftBottomPointLocation, e);
                                break;
                            }
                        case MoveAction.ResizeRight:
                            {
                                //设置左边线为参考线1
                                var preBaseLine1 = new Line(MouseDownControlMeta.LeftTopPointLocation, MouseDownControlMeta.LeftBottomPointLocation);
                                //调整大小
                                resizeModel(preBaseLine1, model, MouseDownControlMeta.StartLocation, newLocation, 0, 180, true, t => MouseDownControlMeta.Width, (t, v) => t.SetWidthToGrid(Project, v));
                                //设置参考点为左上角参考点
                                getBasePointFunc = t => t.LeftTopPointLocation;
                                break;
                            }
                        case MoveAction.ResizeRightBottom:
                            {
                                //设置左边线为参考线1
                                var preBaseLine1 = new Line(MouseDownControlMeta.LeftTopPointLocation, MouseDownControlMeta.LeftBottomPointLocation);
                                //设置上边线为参考线2
                                var preBaseLine2 = new Line(MouseDownControlMeta.LeftTopPointLocation, MouseDownControlMeta.RightTopPointLocation);

                                var newWidth = model.Width;
                                var newHeight = model.Height;
                                //调整大小
                                resizeModel(preBaseLine1, model, MouseDownControlMeta.StartLocation, newLocation, 0, 180, true, t => MouseDownControlMeta.Width, (t, v) => newWidth = v);
                                resizeModel(preBaseLine2, model, MouseDownControlMeta.StartLocation, newLocation, 90, 270, false, t => MouseDownControlMeta.Height, (t, v) => newHeight = v);
                                //设置默认参考点为左上角参考点
                                getBasePointFunc = resizeModel_FormCorner(model, newWidth, newHeight, t => t.LeftTopPointLocation, e);
                                break;
                            }
                        case MoveAction.ResizeBottom:
                            {
                                //设置上边线为参考线1
                                var preBaseLine1 = new Line(MouseDownControlMeta.LeftTopPointLocation, MouseDownControlMeta.RightTopPointLocation);
                                //调整大小
                                resizeModel(preBaseLine1, model, MouseDownControlMeta.StartLocation, newLocation, 90, 270, false, t => MouseDownControlMeta.Height, (t, v) => t.SetHeightToGrid(Project, v));
                                //设置参考点为右上角参考点
                                getBasePointFunc = t => t.RightTopPointLocation;
                                break;
                            }
                        case MoveAction.ResizeLeftBottom:
                            {
                                //设置上边线为参考线1
                                var preBaseLine1 = new Line(MouseDownControlMeta.LeftTopPointLocation, MouseDownControlMeta.RightTopPointLocation);
                                //设置右边线为参考线2
                                var preBaseLine2 = new Line(MouseDownControlMeta.RightTopPointLocation, MouseDownControlMeta.RightBottomPointLocation);

                                var newWidth = model.Width;
                                var newHeight = model.Height;
                                //调整大小
                                resizeModel(preBaseLine2, model, MouseDownControlMeta.StartLocation, newLocation, 0, 180, false, t => MouseDownControlMeta.Width, (t, v) => newWidth = v);
                                resizeModel(preBaseLine1, model, MouseDownControlMeta.StartLocation, newLocation, 90, 270, false, t => MouseDownControlMeta.Height, (t, v) => newHeight = v);
                                //设置默认参考点为右上角参考点
                                getBasePointFunc = resizeModel_FormCorner(model, newWidth, newHeight, t => t.RightTopPointLocation, e);
                                break;
                            }
                        case MoveAction.ResizeLeft:
                            {
                                //设置右边线为参考线1
                                var preBaseLine1 = new Line(MouseDownControlMeta.RightTopPointLocation, MouseDownControlMeta.RightBottomPointLocation);
                                //调整大小
                                resizeModel(preBaseLine1, model, MouseDownControlMeta.StartLocation, newLocation, 0, 180, false, t => MouseDownControlMeta.Width, (t, v) => t.SetWidthToGrid(Project, v));
                                //设置参考点为右下角参考点
                                getBasePointFunc = t => t.RightBottomPointLocation;
                                break;
                            }
                        default:
                            return;
                    }
                    //让基准点保持不变
                    makeBasePointLocationNotChange(MouseDownControlMeta, getBasePointFunc);
                    break;
            }
        }

        private Func<BaseControl, Point> resizeModel_FormCorner(BaseControl model, double newWidth, double newHeight, Func<BaseControl, Point> getBasePointFunc, MouseEventArgs mouseEventArgs)
        {
            //如果是按住Shift键，则拉伸缩放，保持指定点位置不变
            if (mouseEventArgs.ShiftKey)
            {
                model.SetWidthToGrid(Project, newWidth);
                model.SetHeightToGrid(Project, newHeight);
                return getBasePointFunc;
            }
            //如果是按住Control键，则按比例缩放，保持中心点位置不变
            else if (mouseEventArgs.CtrlKey)
            {
                var maxScale = Math.Max(newWidth / MouseDownControlMeta.Width, newHeight / MouseDownControlMeta.Height);
                model.SetWidthToGrid(Project, MouseDownControlMeta.Width * maxScale);
                model.SetHeightToGrid(Project, MouseDownControlMeta.Height * maxScale);
                return t => t.CenterPointLocation;
            }
            else
            {
                //默认，则按比例缩放，保持左上点位置不变
                var maxScale = Math.Max(newWidth / MouseDownControlMeta.Width, newHeight / MouseDownControlMeta.Height);
                model.SetWidthToGrid(Project, MouseDownControlMeta.Width * maxScale);
                model.SetHeightToGrid(Project, MouseDownControlMeta.Height * maxScale);
                return getBasePointFunc;
            }
        }

        private void sizerMouseDown(MouseEventArgs e, MoveAction moveAction)
        {
            //如果不是鼠标左键，则不处理
            if (e.Button != 0)
                return;
            //如果已锁定，则不处理
            if (SelectedItem.IsLocked)
                return;
            var mouseLocation = getMouseLocation_RelativeToCanvas(e);
            MouseDownControlMeta = new ControlMeta(moveAction, mouseLocation, SelectedItem);
        }
    }
}
