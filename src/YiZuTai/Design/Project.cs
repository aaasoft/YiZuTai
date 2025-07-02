using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

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
            Canvas.PropertyChanged += Canvas_PropertyChanged;
            Controls = new ObservableCollection<BaseControl>();
            Controls.CollectionChanged += Controls_CollectionChanged;
        }

        private void Canvas_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            RaisePropertyChanged(nameof(Canvas));
        }

        private void Controls_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            RaisePropertyChanged(nameof(Controls));
        }
    }
}
