using System.ComponentModel;
using YiZuTai.Control;

namespace YiZuTai.Sample.Controls.Web;

[Category("常用")]
[DisplayName("网页")]
public class Model : BaseControl
{
    protected override Type GetViewType() => typeof(View);

}
