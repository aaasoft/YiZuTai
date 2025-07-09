using System.ComponentModel;

namespace YiZuTai.Design.Enums
{
    public enum ContentTypeEnum
    {
        [DefaultValue("application/json")]
        application_json,
        [DefaultValue("text/json")]
        text_json,
        [DefaultValue("application/x-www-form-urlencoded")]
        application_x_www_form_urlencoded,
        [DefaultValue("text/xml")]
        text_xml,
    }

    public class ContentTypeEnumItemSource : AbstractEnumItemSource<ContentTypeEnum> { }
}
