using System.ComponentModel;

namespace YiZuTai.Design.Enums
{
    public enum HttpResultTypeEnum
    {
        [Description("Restful API")]
        Rest,
        [Description("JSON(code+message)")]
        JSON_Code_Message
    }

    public class HttpResultTypeEnumItemSource : AbstractEnumItemSource<HttpResultTypeEnum> { }
}
