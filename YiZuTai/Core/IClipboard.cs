using System;

namespace YiZuTai.Core;

/// <summary>
/// 剪贴板接口
/// </summary>
public interface IClipboard
{
    /// <summary>
    /// 获取内容
    /// </summary>
    /// <returns></returns>
    object GetContent();
    /// <summary>
    /// 设置内容
    /// </summary>
    /// <param name="obj"></param>
    void SetContent(object obj);
}
