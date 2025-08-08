using Microsoft.JSInterop;
using Quick.Blazor.Bootstrap;
using YiZuTai.Control;

namespace YiZuTai.Core;

public class ZTContext
{
    public ZTFile ZTFile { get; set; }
    public Project Project { get; set; }
    public ModalWindow ModalWindow { get; set; }
    public bool DesignMode { get; set; } = false;
    public IJSRuntime JSRuntime { get; set; }
}