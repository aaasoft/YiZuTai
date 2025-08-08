using System.Collections.ObjectModel;
using Quick.Blazor.Bootstrap;
using YiZuTai.Control;

namespace YiZuTai
{
    public class Project : PropertyNotifyModel
    {
        /// <summary>
        /// 画布
        /// </summary>
        public Canvas Canvas { get; set; }
        /// <summary>
        /// 控件集合
        /// </summary>
        public ObservableCollection<BaseControl> Controls { get; set; }

        public Project()
        {
            Canvas = new Canvas();
            Controls = new ObservableCollection<BaseControl>();
        }
    }
}
