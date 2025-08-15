using System;
using YiZuTai.Core;

namespace YiZuTai.Control;

public interface IControlView
{
    ZTContext ZTContext { get; }
    
    /// <summary>
    /// 双击控件时
    /// </summary>
    void OnDoubleClick();

    /// <summary>
    /// 刷新
    /// </summary>
    void Refresh();
}
