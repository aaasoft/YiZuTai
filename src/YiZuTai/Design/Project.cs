using System.Collections.ObjectModel;
using YiZuTai.ViewModel;

namespace YiZuTai.Design
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
