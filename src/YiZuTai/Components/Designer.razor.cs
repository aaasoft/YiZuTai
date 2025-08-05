using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using Quick.Blazor.Bootstrap;
using YiZuTai.Design;

namespace YiZuTai.Components
{
    public partial class Designer : ComponentBase, IDisposable
    {
        private ModalLoading modalLoading;
        private ToastStack toastStack;
        private ModalPrompt modalPrompt;
        private ModalAlert modalAlert;
        private ModalWindow modalWindow;

        private string title = "设计 - 易组态";
        private ControlField _PropertyGridSelectedField;
        public ControlField PropertyGridSelectedField

        {
            get { return _PropertyGridSelectedField; }
            set
            {
                _PropertyGridSelectedField = value;
                NotifyBrowserWindowResize();
            }
        }
        private string PropertyGridSelectedFieldDescriptionStyle => (PropertyGridSelectedField == null || string.IsNullOrEmpty(PropertyGridSelectedField.Description)) ? "display: none" : "display: inline";

        private int _DesignScale = 100;
        /// <summary>
        /// 显示比例
        /// </summary>
        private int DesignScale
        {
            get { return _DesignScale; }
            set
            {
                if (value < 1)
                    return;
                _DesignScale = value;
                NotifyBrowserWindowResize();
            }
        }

        public bool ShowToolBox = true;
        public bool ShowPropertyBox = true;

        private void NotifyBrowserWindowResize()
        {
            Task.Delay(10).ContinueWith(t => JSRuntime.InvokeVoidAsync("YiZuTai.Designer.OnResizeFunc"));
        }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Parameter]
        public string Id { get; set; }
        public Core.ZTFile Model { get; private set; }
        public Project Project { get; private set; }
        public Canvas Canvas => Project.Canvas;

        private bool isWindowOnbeforeUnloadEventAdded = false;
        private BaseControl _SelectedItem;
        private BaseControl SelectedItem
        {
            get { return _SelectedItem; }
            set
            {
                _SelectedItem = value;
                PropertyGridSelectedField = null;
                NotifyBrowserWindowResize();
            }
        }

        private string SelectedItemName
        {
            get
            {
                //如果选中了控件，则返回控件的名称
                if (IsSelectedControl)
                    return SelectedItem?.Name;
                //返回空字符串
                return string.Empty;
            }
            set
            {
                BaseControl item = null;
                if (!string.IsNullOrEmpty(value))
                    item = Project.Controls.FirstOrDefault(t => t.Name == value);
                if (item == null)
                    item = Canvas;
                SelectedItem = item;
            }
        }
        //是否选中了控件
        public bool IsSelectedControl => SelectedItem != null && SelectedItem != Canvas;

        protected override void OnParametersSet()
        {
            Model = Core.ZTFileManager.Instance.Get(Id);
            Project = Model.OpenProject();
            Project.PropertyChanged += Project_PropertyChanged;
            Project.Canvas.PropertyChanged += Control_PropertyChanged;
            foreach (var control in Project.Controls)
                control.PropertyChanged += Control_PropertyChanged;
            Project.Controls.CollectionChanged += Controls_CollectionChanged;
            SelectedItem = Canvas;
        }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
                Task.Delay(100).ContinueWith(t => refreshTitle());
        }

        private void refreshTitle()
        {
            var titleStr = $"{Model.Name} - 设计 - 易组态";
            if (isWindowOnbeforeUnloadEventAdded)
                titleStr = "*" + titleStr;
            title = titleStr;
            JSRuntime.InvokeVoidAsync("YiZuTai.SetTitle", title);
        }

        private ControlAttribute dragingControlAttr = null;
        //ondragstart事件处理方法
        private void HandleDragStart(DragEventArgs e, ControlAttribute controlAttr)
        {
            // 设置拖动效果为"move"
            e.DataTransfer.DropEffect = "move";
            e.DataTransfer.EffectAllowed = "move";
            dragingControlAttr = controlAttr;
        }

        // ondrop事件处理方法
        private void HandleDrop(DragEventArgs e)
        {
            if (dragingControlAttr == null)
                return;
            var newLocation = getMouseLocation_RelativeToCanvas(e);
            addControl(dragingControlAttr, newLocation);
        }

        private void Controls_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    foreach (BaseControl control in e.NewItems)
                    {
                        control.PropertyChanged += Control_PropertyChanged;
                        SelectedItem = control;
                    }
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    foreach (BaseControl control in e.OldItems)
                        control.PropertyChanged -= Control_PropertyChanged;
                    SelectedItem = Canvas;
                    break;
            }
            onDesignChanged();
        }

        private void Project_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (mouseDownControlMeta != null)
                return;
            InvokeAsync(StateHasChanged);
        }

        private void Control_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var currentControl = (BaseControl)sender;
            switch (e.PropertyName)
            {
                case nameof(BaseControl.Name):
                    //检查控件名称是否重复
                    if (Project.Controls.Any(t => t != currentControl && t.Name == currentControl.Name))
                    {
                        currentControl.Name = ControlManager.Instance.GetItem(currentControl.GetType().FullName).Name + Guid.NewGuid().ToString("N");
                        return;
                    }
                    break;
                case nameof(BaseControl.Width):
                case nameof(BaseControl.Height):
                    //如果是画面的宽度或者高度有变化
                    if (currentControl == Canvas)
                        NotifyBrowserWindowResize();
                    break;
            }
            Project_PropertyChanged(sender, e);
            onDesignChanged();
        }

        private void onKeyDown(KeyboardEventArgs e)
        {
            if ("KeyS" == e.Code && (e.CtrlKey || e.MetaKey))
            {
                Save();
                return;
            }

            //如果选中了控件，且控件不是画布
            if (SelectedItem != null && SelectedItem != Canvas)
            {
                //如果控件已锁定，则不允许通过键盘调整位置和大小
                if (SelectedItem.IsLocked)
                    return;

                var setp = e.CtrlKey ? 10 : 1;
                switch (e.Code)
                {
                    case "ArrowUp":
                        SelectedItem.Top -= setp;
                        break;
                    case "ArrowDown":
                        SelectedItem.Top += setp;
                        break;
                    case "ArrowLeft":
                        if (e.ShiftKey)
                            SelectedItem.Angle -= setp;
                        else
                            SelectedItem.Left -= setp;
                        break;
                    case "ArrowRight":
                        if (e.ShiftKey)
                            SelectedItem.Angle += setp;
                        else
                            SelectedItem.Left += setp;
                        break;
                    case "Delete":
                        removeControl();
                        break;
                }
            }
        }

        private void onDesignChanged()
        {
            if (isWindowOnbeforeUnloadEventAdded)
                return;
            JSRuntime.InvokeVoidAsync("YiZuTai.Designer.AddWindowOnBeforeUnloadEvent");
            isWindowOnbeforeUnloadEventAdded = true;
            refreshTitle();
        }

        public void Dispose()
        {
            if (Project != null)
            {
                Project.PropertyChanged -= Project_PropertyChanged;
                foreach (var control in Project.Controls)
                    control.PropertyChanged -= Project_PropertyChanged;
                Project.Controls.CollectionChanged -= Controls_CollectionChanged;
                Project.Canvas.PropertyChanged -= Control_PropertyChanged;
            }
        }

        // 保存
        private void Save()
        {
            Model.SaveProject(Project);
            toastStack.AddToast("保存", "保存成功!", BackgroundTheme.success);
            JSRuntime.InvokeVoidAsync("YiZuTai.Designer.RemoveWindowOnBeforeUnloadEvent");
            isWindowOnbeforeUnloadEventAdded = false;
            refreshTitle();
        }

        // 预览
        private void Preview()
        {
            modalWindow.Show("预览", new DialogParameters<View>
            {
                {x => x.Model, Model},
                {x => x.Project, Quick.Xml.XmlConvert.Deserialize<Project>(Quick.Xml.XmlConvert.Serialize(Project))}
            });
        }

        // 图片管理
        private void ImageManage()
        {
            modalWindow.Show("图片管理", new DialogParameters<ImageSelectControl>
            {
                {x => x.ZTFile, Model}
            });
        }

        // 文件管理
        private void FileManage()
        {
            modalWindow.Show<FileManageControl>("文件管理", new DialogParameters<FileManageControl>
            {
                {x => x.ZTFileId, Model.Id}
            });
        }

        //添加控件
        private void addControl(ControlAttribute controlAttr, Point location)
        {
            var newControl = controlAttr.CreateInstance();
            newControl.Left = location.X - newControl.Width / 2;
            newControl.Top = location.Y - newControl.Height / 2;
            var index = 0;
            do
            {
                index++;
                var name = controlAttr.Name + index;
                if (Project.Controls.Any(t => t.Name == name))
                    continue;
                newControl.Name = name;
                break;
            }
            while (true);
            newControl.OnDragDropCreate(location);
            Project.Controls.Add(newControl);
        }

        //移除控件
        private void removeControl()
        {
            //如果不是选中了控件
            if (!IsSelectedControl)
                return;
            Project.Controls.Remove(SelectedItem);
        }
    }
}
