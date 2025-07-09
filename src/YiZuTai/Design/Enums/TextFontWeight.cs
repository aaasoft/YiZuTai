using System.ComponentModel;

namespace YiZuTai.Design.Enums
{
    public enum TextFontWeight
    {
        [DefaultValue("普通")]
        normal,
        [DefaultValue("加粗")]
        bold,
        [DefaultValue("特粗")]
        bolder,
        [DefaultValue("减细")]
        lighter
    }

    public class TextFontWeightItemSource : AbstractEnumItemSource<TextFontWeight> { }
}
