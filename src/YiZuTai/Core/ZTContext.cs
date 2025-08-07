using Quick.Blazor.Bootstrap;
using YiZuTai.Design;

namespace YiZuTai.Core;

public class ZTContext
{
    public ZTFile ZTFile { get; set; }
    public Project Project { get; set; }
    public ModalWindow ModalWindow { get; set; }
    public bool DesignMode { get; set; } = false;
}