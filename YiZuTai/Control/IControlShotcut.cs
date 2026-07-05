using YiZuTai.Model;

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
    /// <param name="context">组态上下文</param>
    /// <param name="control">控件</param>
    void Execute(ZTContext context, object control);
}
