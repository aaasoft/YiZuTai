using Microsoft.JSInterop;
using Quick.Blazor.Bootstrap;
using YiZuTai.Components;

namespace YiZuTai.Core;

public class ZTContext
{
    public ZTFile ZTFile { get; set; }
    public Project Project { get; set; }
    public ModalWindow ModalWindow { get; set; }
    public Designer Designer { get; set; }
    public bool DesignMode => Designer != null;
    public IJSRuntime JSRuntime { get; set; }
}