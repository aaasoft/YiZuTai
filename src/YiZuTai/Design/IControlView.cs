using System;

namespace YiZuTai.Design;

public interface IControlView
{
    bool DesignMode { get; }

    /// <summary>
    /// 双击控件时
    /// </summary>
    void OnDoubleClick();

    /// <summary>
    /// 刷新
    /// </summary>
    void Refresh();
}
