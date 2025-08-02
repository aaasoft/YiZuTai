using System.ComponentModel;

namespace YiZuTai.Design.Enums
{
    public enum ContentTypeEnum
    {
        [Description("application/json")]
        application_json,
        [Description("text/json")]
        text_json,
        [Description("application/x-www-form-urlencoded")]
        application_x_www_form_urlencoded,
        [Description("text/xml")]
        text_xml,
    }

    public class ContentTypeEnumItemSource : AbstractEnumItemSource<ContentTypeEnum> { }
}
