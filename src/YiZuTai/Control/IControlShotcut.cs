namespace YiZuTai.Control;

/// <summary>
/// 控件快捷操作
/// </summary>
public interface IControlShotcut
{
    /// <summary>
    /// 控件类型
    /// </summary>
    Type ControlType { get; }
    /// <summary>
    /// 捷径名称
    /// </summary>
    string Name { get; }
    /// <summary>
    /// 执行
    /// </summary>
    void Execute(object control);
}
