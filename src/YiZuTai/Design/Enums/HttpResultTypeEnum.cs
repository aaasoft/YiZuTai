using System.ComponentModel;

namespace YiZuTai.Design.Enums
{
    public enum HttpResultTypeEnum
    {
        [DefaultValue("Restful API")]
        Rest,
        [DefaultValue("JSON(code+message)")]
        JSON_Code_Message
    }

    public class HttpResultTypeEnumItemSource : AbstractEnumItemSource<HttpResultTypeEnum> { }
}
