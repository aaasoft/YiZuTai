using System;

namespace YiZuTai.Design;

public interface IControlView
{
    bool DesignMode { get; }

    /// <summary>
    /// 刷新
    /// </summary>
    void Refresh();
}
