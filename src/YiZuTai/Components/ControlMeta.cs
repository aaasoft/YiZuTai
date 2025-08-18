using YiZuTai.Control;
using YiZuTai.Enums;

namespace YiZuTai.Components;

//控制元信息
public class ControlMeta : BaseControl
{
    public BaseControl MoveControl { get; }
    public MoveAction MoveAction { get; }
    /// <summary>
    /// 起始位置
    /// </summary>
    public Point StartLocation { get; private set; }
    /// <summary>
    /// 结束位置
    /// </summary>
    public Point EndLocation { get; private set; }

    public Rect MoveRect { get; private set; }

    public void SetEndLocation(Point endLocation)
    {
        EndLocation = endLocation;
        MoveRect = new Rect()
        {
            X = Math.Min(StartLocation.X, EndLocation.X) - MoveControl.Left,
            Y = Math.Min(StartLocation.Y, EndLocation.Y) - MoveControl.Top,
            Width = Math.Abs(StartLocation.X - EndLocation.X),
            Height = Math.Abs(StartLocation.Y - EndLocation.Y)
        };
    }

    public ControlMeta(MoveAction moveAction, Point startLocation, BaseControl model = null)
    {
        MoveAction = moveAction;
        StartLocation = startLocation;
        EndLocation = startLocation;

        if (model != null)
        {
            MoveControl = model;
            Left = model.Left;
            Top = model.Top;
            Width = model.Width;
            Height = model.Height;
            Angle = model.Angle;
        }
    }
    protected override Type GetViewType() { throw new NotImplementedException(); }
}