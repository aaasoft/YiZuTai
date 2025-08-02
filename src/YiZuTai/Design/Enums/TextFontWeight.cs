using System.ComponentModel;

namespace YiZuTai.Design.Enums
{
    public enum TextFontWeight
    {
        [Description("普通")]
        normal,
        [Description("加粗")]
        bold,
        [Description("特粗")]
        bolder,
        [Description("减细")]
        lighter
    }

    public class TextFontWeightItemSource : AbstractEnumItemSource<TextFontWeight> { }
}
