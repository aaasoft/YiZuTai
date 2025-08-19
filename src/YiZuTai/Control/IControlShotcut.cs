using System;
using System.Windows.Input;

namespace YiZuTai.Control;

/// <summary>
/// 控件快捷操作
/// </summary>
public interface IControlShotcut
{
    /// <summary>
    /// 主题捷径名称
    /// </summary>
    string Name { get; }
    /// <summary>
    /// 绑定的命令
    /// </summary>
    ICommand ExecuteCommand { get; }
}
