using System;

namespace YiZuTai;

public class YiZuTaiOptions
{
    public string Prefix { get; set; } = nameof(YiZuTai);
    public string DataFolder { get; set; } = "Data";
    public bool UseDefaultControls { get; set; } = true;
}
