
using Microsoft.JSInterop;

namespace YiZuTai.Utils;

public class ClipboardUtils
{
    public static ValueTask<string> ReadText(IJSRuntime JSRuntime)
    {
        return JSRuntime.InvokeAsync<string>("navigator.clipboard.readText");
    }

    public static ValueTask WriteText(IJSRuntime JSRuntime, string text)
    {
        return JSRuntime.InvokeVoidAsync("navigator.clipboard.writeText", text);
    }
}
