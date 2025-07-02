using Microsoft.AspNetCore.Components.Web;
using YiZuTai.Design;

namespace YiZuTai.Components
{
    public partial class Designer
    {
        //控制元信息
        public class ControlMeta : BaseControl
        {
            /// <summary>
            /// 鼠标位置
            /// </summary>
            public Point MouseLocation { get; set; }

            public ControlMeta(BaseControl model, Point mouseLocation)
            {
                Left = model.Left;
                Top = model.Top;
                Width = model.Width;
                Height = model.Height;
                Angle = model.Angle;
                MouseLocation = mouseLocation;
            }
        }

        private double resize_box_width = 7;
        //鼠标按下时控件信息
        private ControlMeta mouseDownControlMeta;

        private bool isMoveLocation = false;
        private bool isResizeLeftTop = false;
        private bool isResizeTop = false;
        private bool isResizeRightTop = false;
        private bool isResizeRight = false;
        private bool isResizeRightBottom = false;
        private bool isResizeBottom = false;
        private bool isResizeLeftBottom = false;
        private bool isResizeLeft = false;
        private bool isRotate = false;

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

            model.SetLeftToGrid(Canvas, model.Left + preBasePointLocation.X - newBasePointLocation.X);
            model.SetTopToGrid(Canvas, model.Top + preBasePointLocation.Y - newBasePointLocation.Y);
        }

        private void onCanvasMouseDown(MouseEventArgs e)
        {
            SelectedItem = Canvas;
        }

        private void onControlMouseDown(MouseEventArgs e, BaseControl control)
        {
            if (mouseDownControlMeta != null)
                return;

            SelectedItem = control;
            sizerMouseDown(e, () => isMoveLocation = true);
        }

        private void onMouseUp(MouseEventArgs e)
        {
            isMoveLocation = false;
            isResizeLeftTop = false;
            isResizeTop = false;
            isResizeRightTop = false;
            isResizeRight = false;
            isResizeRightBottom = false;
            isResizeBottom = false;
            isResizeLeftBottom = false;
            isResizeLeft = false;
            isRotate = false;
            mouseDownControlMeta = null;
        }

        private Point getMouseLocation_RelativeToCanvas(MouseEventArgs e)
        {
            return new Point(e.OffsetX, e.OffsetY);
        }

        private void onMouseMove(MouseEventArgs e)
        {
            //如果没有按下鼠标
            if (mouseDownControlMeta == null)
                return;
            //如果没有选中控件
            if (!IsSelectedControl)
                return;

            var newLocation = getMouseLocation_RelativeToCanvas(e);
            if (DesignScale != 100)
            {
                newLocation.X /= DesignScale * 1D / 100;
                newLocation.Y /= DesignScale * 1D / 100;
            }
            var offsetX = newLocation.X - mouseDownControlMeta.MouseLocation.X;
            var offsetY = newLocation.Y - mouseDownControlMeta.MouseLocation.Y;
            if (offsetX == 0 && offsetY == 0)
                return;

            var model = SelectedItem;
            if (isMoveLocation)
            {
                var preLeft = model.Left;
                var preTop = model.Top;
                model.SetLeftToGrid(Canvas, mouseDownControlMeta.Left + offsetX);
                model.SetTopToGrid(Canvas, mouseDownControlMeta.Top + offsetY);
                model.MoveLocationOffset = new Point(model.Left - preLeft, model.Top - preTop);
            }
            else if (isRotate)
            {
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
                model.MoveAngleOffset = newAngle - model.Angle;
                model.Angle = newAngle;
            }
            else
            {
                Func<BaseControl, Point> getBasePointFunc = null;
                if (isResizeLeftTop)
                {
                    //设置右边线为参考线1
                    var preBaseLine1 = new Line(mouseDownControlMeta.RightTopPointLocation, mouseDownControlMeta.RightBottomPointLocation);
                    //设置下边线为参考线2
                    var preBaseLine2 = new Line(mouseDownControlMeta.LeftBottomPointLocation, mouseDownControlMeta.RightBottomPointLocation);

                    var newWidth = model.Width;
                    var newHeight = model.Height;
                    //调整大小
                    resizeModel(preBaseLine1, model, mouseDownControlMeta.MouseLocation, newLocation, 0, 180, false, t => mouseDownControlMeta.Width, (t, v) => newWidth = v);
                    resizeModel(preBaseLine2, model, mouseDownControlMeta.MouseLocation, newLocation, 90, 270, true, t => mouseDownControlMeta.Height, (t, v) => newHeight = v);
                    //设置默认参考点为右下角参考点
                    getBasePointFunc = resizeModel_FormCorner(model, newWidth, newHeight, t => t.RightBottomPointLocation, e);
                }
                else if (isResizeTop)
                {
                    //设置下边线为参考线1
                    var preBaseLine1 = new Line(mouseDownControlMeta.LeftBottomPointLocation, mouseDownControlMeta.RightBottomPointLocation);
                    //调整大小
                    resizeModel(preBaseLine1, model, mouseDownControlMeta.MouseLocation, newLocation, 90, 270, true, t => mouseDownControlMeta.Height, (t, v) => t.SetHeightToGrid(Canvas, v));
                    //设置参考点为左下角参考点
                    getBasePointFunc = t => t.LeftBottomPointLocation;
                }
                else if (isResizeRightTop)
                {
                    //设置下边线为参考线1
                    var preBaseLine1 = new Line(mouseDownControlMeta.LeftBottomPointLocation, mouseDownControlMeta.RightBottomPointLocation);
                    //设置左边线为参考线2
                    var preBaseLine2 = new Line(mouseDownControlMeta.LeftTopPointLocation, mouseDownControlMeta.LeftBottomPointLocation);

                    var newWidth = model.Width;
                    var newHeight = model.Height;
                    //调整大小
                    resizeModel(preBaseLine2, model, mouseDownControlMeta.MouseLocation, newLocation, 0, 180, true, t => mouseDownControlMeta.Width, (t, v) => newWidth = v);
                    resizeModel(preBaseLine1, model, mouseDownControlMeta.MouseLocation, newLocation, 90, 270, true, t => mouseDownControlMeta.Height, (t, v) => newHeight = v);
                    //设置默认参考点为左下角参考点
                    getBasePointFunc = resizeModel_FormCorner(model, newWidth, newHeight, t => t.LeftBottomPointLocation, e);
                }
                else if (isResizeRight)
                {
                    //设置左边线为参考线1
                    var preBaseLine1 = new Line(mouseDownControlMeta.LeftTopPointLocation, mouseDownControlMeta.LeftBottomPointLocation);
                    //调整大小
                    resizeModel(preBaseLine1, model, mouseDownControlMeta.MouseLocation, newLocation, 0, 180, true, t => mouseDownControlMeta.Width, (t, v) => t.SetWidthToGrid(Canvas, v));
                    //设置参考点为左上角参考点
                    getBasePointFunc = t => t.LeftTopPointLocation;
                }
                else if (isResizeRightBottom)
                {
                    //设置左边线为参考线1
                    var preBaseLine1 = new Line(mouseDownControlMeta.LeftTopPointLocation, mouseDownControlMeta.LeftBottomPointLocation);
                    //设置上边线为参考线2
                    var preBaseLine2 = new Line(mouseDownControlMeta.LeftTopPointLocation, mouseDownControlMeta.RightTopPointLocation);

                    var newWidth = model.Width;
                    var newHeight = model.Height;
                    //调整大小
                    resizeModel(preBaseLine1, model, mouseDownControlMeta.MouseLocation, newLocation, 0, 180, true, t => mouseDownControlMeta.Width, (t, v) => newWidth = v);
                    resizeModel(preBaseLine2, model, mouseDownControlMeta.MouseLocation, newLocation, 90, 270, false, t => mouseDownControlMeta.Height, (t, v) => newHeight = v);
                    //设置默认参考点为左上角参考点
                    getBasePointFunc = resizeModel_FormCorner(model, newWidth, newHeight, t => t.LeftTopPointLocation, e);
                }
                else if (isResizeBottom)
                {
                    //设置上边线为参考线1
                    var preBaseLine1 = new Line(mouseDownControlMeta.LeftTopPointLocation, mouseDownControlMeta.RightTopPointLocation);
                    //调整大小
                    resizeModel(preBaseLine1, model, mouseDownControlMeta.MouseLocation, newLocation, 90, 270, false, t => mouseDownControlMeta.Height, (t, v) => t.SetHeightToGrid(Canvas, v));
                    //设置参考点为右上角参考点
                    getBasePointFunc = t => t.RightTopPointLocation;
                }
                else if (isResizeLeftBottom)
                {
                    //设置上边线为参考线1
                    var preBaseLine1 = new Line(mouseDownControlMeta.LeftTopPointLocation, mouseDownControlMeta.RightTopPointLocation);
                    //设置右边线为参考线2
                    var preBaseLine2 = new Line(mouseDownControlMeta.RightTopPointLocation, mouseDownControlMeta.RightBottomPointLocation);

                    var newWidth = model.Width;
                    var newHeight = model.Height;
                    //调整大小
                    resizeModel(preBaseLine2, model, mouseDownControlMeta.MouseLocation, newLocation, 0, 180, false, t => mouseDownControlMeta.Width, (t, v) => newWidth = v);
                    resizeModel(preBaseLine1, model, mouseDownControlMeta.MouseLocation, newLocation, 90, 270, false, t => mouseDownControlMeta.Height, (t, v) => newHeight = v);
                    //设置默认参考点为右上角参考点
                    getBasePointFunc = resizeModel_FormCorner(model, newWidth, newHeight, t => t.RightTopPointLocation, e);
                }
                else if (isResizeLeft)
                {
                    //设置右边线为参考线1
                    var preBaseLine1 = new Line(mouseDownControlMeta.RightTopPointLocation, mouseDownControlMeta.RightBottomPointLocation);
                    //调整大小
                    resizeModel(preBaseLine1, model, mouseDownControlMeta.MouseLocation, newLocation, 0, 180, false, t => mouseDownControlMeta.Width, (t, v) => t.SetWidthToGrid(Canvas, v));
                    //设置参考点为右下角参考点
                    getBasePointFunc = t => t.RightBottomPointLocation;
                }
                else
                {
                    return;
                }
                //让基准点保持不变
                makeBasePointLocationNotChange(mouseDownControlMeta, getBasePointFunc);
            }
        }

        private Func<BaseControl, Point> resizeModel_FormCorner(BaseControl model, double newWidth, double newHeight, Func<BaseControl, Point> getBasePointFunc, MouseEventArgs mouseEventArgs)
        {
            //如果是按住Shift键，则拉伸缩放，保持指定点位置不变
            if (mouseEventArgs.ShiftKey)
            {
                model.SetWidthToGrid(Canvas, newWidth);
                model.SetHeightToGrid(Canvas, newHeight);
                return getBasePointFunc;
            }
            //如果是按住Control键，则按比例缩放，保持中心点位置不变
            else if (mouseEventArgs.CtrlKey)
            {
                var maxScale = Math.Max(newWidth / mouseDownControlMeta.Width, newHeight / mouseDownControlMeta.Height);
                model.SetWidthToGrid(Canvas, mouseDownControlMeta.Width * maxScale);
                model.SetHeightToGrid(Canvas, mouseDownControlMeta.Height * maxScale);
                return t => t.CenterPointLocation;
            }
            else
            {
                //默认，则按比例缩放，保持左上点位置不变
                var maxScale = Math.Max(newWidth / mouseDownControlMeta.Width, newHeight / mouseDownControlMeta.Height);
                model.SetWidthToGrid(Canvas, mouseDownControlMeta.Width * maxScale);
                model.SetHeightToGrid(Canvas, mouseDownControlMeta.Height * maxScale);
                return getBasePointFunc;
            }
        }

        private void sizerMouseDown(MouseEventArgs e, Action action)
        {
            //如果不是鼠标左键，则不处理
            if (e.Button != 0)
                return;
            //如果已锁定，则不处理
            if (SelectedItem.IsLocked)
                return;
            var mouseLocation = getMouseLocation_RelativeToCanvas(e);
            if (DesignScale != 100)
            {
                mouseLocation.X /= DesignScale * 1D / 100;
                mouseLocation.Y /= DesignScale * 1D / 100;
            }
            mouseDownControlMeta = new ControlMeta(SelectedItem, mouseLocation);
            action?.Invoke();
        }
    }
}
