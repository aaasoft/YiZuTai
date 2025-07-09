using System.ComponentModel;

namespace YiZuTai.Design.Enums
{
    public enum HttpMethodEnum
    {
        [DefaultValue("Get")]
        Get,
        [DefaultValue("Post")]
        Post,
        [DefaultValue("Put")]
        Put,
        [DefaultValue("Delete")]
        Delete
    }

    public class HttpMethodEnumItemSource : AbstractEnumItemSource<HttpMethodEnum> { }
}
