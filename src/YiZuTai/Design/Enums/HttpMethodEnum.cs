using System.ComponentModel;

namespace YiZuTai.Design.Enums
{
    public enum HttpMethodEnum
    {
        [Description("Get")]
        Get,
        [Description("Post")]
        Post,
        [Description("Put")]
        Put,
        [Description("Delete")]
        Delete
    }

    public class HttpMethodEnumItemSource : AbstractEnumItemSource<HttpMethodEnum> { }
}
