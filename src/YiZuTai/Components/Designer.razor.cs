using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using Quick.Blazor.Bootstrap;
using YiZuTai.Core;
using YiZuTai.Control;
using YiZuTai.Core.ObjectConnection;
using Quick.Blazor.Bootstrap.Admin;
using Quick.Xml;
using Quick.Blazor.Bootstrap.Utils;
using YiZuTai.Utils;

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

        private int DesignMargin = 6;

        private int _DesignScale = 100;
        /// <summary>
        /// 显示比例
        /// </summary>
        public int DesignScale
        {
            get { return _DesignScale; }
            set
            {
                if (value < 1)
                    return;
                _DesignScale = value;
            }
        }

        public bool ShowDesignGrid = false;
        public bool ShowToolBox = true;
        public bool ShowPropertyBox = true;

        [Parameter]
        public string Id { get; set; }
        public ZTFile Model { get; private set; }
        public Project Project { get; private set; }

        private ZTContext ZTContext;
        [Inject]
        private IJSRuntime JSRuntime { get; set; }

        private bool isWindowOnbeforeUnloadEventAdded = false;
        private BaseControl _SelectedItem;
        public BaseControl SelectedItem
        {
            get { return _SelectedItem; }
            set
            {
                _SelectedItem = value;
                InvokeAsync(StateHasChanged);
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
                    item = Project.GetAllControls().FirstOrDefault(t => t.Name == value);
                if (item == null)
                    item = Project;
                SelectedItem = item;
            }
        }
        //是否选中了控件
        public bool IsSelectedControl => SelectedItem != null && SelectedItem != Project;
        //是否选中了多个控件
        public bool IsSelectedMutiControl => IsSelectedControl && SelectedItem is MultiControl;

        protected override void OnParametersSet()
        {
            Model = ZTFileManager.Instance.Get(Id);
            Project = Model.OpenProject();
            ZTContext = new ZTContext()
            {
                ZTFile = Model,
                JSRuntime = JSRuntime,
                Designer = this
            };
            LoadProject();
            registerDesigner(designerId, this);
        }

        private void LoadProject()
        {
            ObjectConnectionManager.Instance.AddObject(Project);

            Project.PropertyChanged += Project_PropertyChanged;
            foreach (var control in Project.Controls)
                control.PropertyChanged += Control_PropertyChanged;
            Project.Controls.CollectionChanged += Controls_CollectionChanged;
            setParentToControls(Project, Project.Controls);
            SelectedItem = Project;
            ZTContext.Project = Project;
        }

        private void setParentToControls(BaseControl parent, IEnumerable<BaseControl> controls)
        {
            if (controls == null)
                return;
            foreach (var control in controls)
            {
                control.Parent = parent;
                if (control.IsContainer)
                    setParentToControls(control, control.Controls);
            }
        }

        private void UnloadProject()
        {
            if (Project != null)
            {
                ObjectConnectionManager.Instance.RemoveObject(Project);

                Project.PropertyChanged -= Project_PropertyChanged;
                foreach (var control in Project.Controls)
                    control.PropertyChanged -= Project_PropertyChanged;
                Project.Controls.CollectionChanged -= Controls_CollectionChanged;
                SelectedItem = null;
                ZTContext.Project = null;
            }
        }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                Task.Delay(100).ContinueWith(t => refreshTitle());
                ZTContext.ModalWindow = modalWindow;
            }
        }

        private void refreshTitle()
        {
            var titleStr = $"{Model.Name} - 设计 - 易组态";
            if (isWindowOnbeforeUnloadEventAdded)
                titleStr = "*" + titleStr;
            title = titleStr;
            JSRuntime.InvokeVoidAsync("YiZuTai.SetTitle", title);
        }

        public ControlAttribute dragingControlAttr = null;
        //ondragstart事件处理方法
        private void HandleDragStart(DragEventArgs e, ControlAttribute controlAttr)
        {
            // 设置拖动效果为"move"
            e.DataTransfer.DropEffect = "move";
            e.DataTransfer.EffectAllowed = "move";
            dragingControlAttr = controlAttr;
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
                    SelectedItem = Project;
                    break;
            }
            onDesignChanged();
        }

        private void Project_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (MouseDownControlMeta != null)
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
            }
            onDesignChanged();
        }

        private async Task onKeyDown(KeyboardEventArgs e)
        {
            if (e.CtrlKey || e.MetaKey)
            {
                switch (e.Code)
                {
                    case "KeyA":
                        SelectedItem = new MultiControl(Project, Project.DesignGridSize, Project.Controls);
                        return;
                    case "KeyX":
                        if (SelectedItem == null)
                            return;
                        await copyControl();
                        removeControl();
                        return;
                    case "KeyS":
                        Save();
                        return;
                    case "KeyC":
                        await copyControl();
                        return;
                    case "KeyV":
                        await pasteControl();
                        return;
                }
            }

            //如果选中了控件，且控件不是画布
            if (SelectedItem != null && SelectedItem != Project)
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
            unregisterDesigner(designerId);
            UnloadProject();
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
            var previewUrl = ZTFile.GetStaticResourceUrl(ZTContext.ZTFile.Id, $"../View?DesignerId={designerId}");
            ZTContext.JSRuntime.InvokeVoidAsync("eval", @$"
new YiZuTai.Window({{
    maxmin: true,
    pages: [{{
        title: '预览',
        url: '{previewUrl}'
    }}]
}}).show();"
            );
        }

        // 文件管理
        private void FileManage()
        {
            if (!Directory.Exists(Model.ResourceFolder))
                Directory.CreateDirectory(Model.ResourceFolder);
            modalWindow.Show("资源文件", new DialogParameters<FileManageControl>
            {
                { x => x.ZTFile, Model}
            });
        }

        //显示源码
        private void ShowSourceCode()
        {
            modalWindow.Show("源码", new DialogParameters<TextEditControl>
            {
                { x => x.GetTextFunc, ()=> XmlConvert.Serialize(Project)},
                { x => x.SetTextAction, t=>
                    {
                        try
                        {
                            UnloadProject();
                            Project = (Project)XmlConvert.Deserialize(t);
                            LoadProject();
                            onDesignChanged();
                            InvokeAsync(StateHasChanged);
                            modalWindow.Close();
                        }
                        catch(Exception ex)
                        {
                            modalAlert.Show("错误",ExceptionUtils.GetExceptionMessage(ex));
                        }
                    }
                }
            });
        }

        //添加控件
        private void addControl(BaseControl control, BaseControl container = null)
        {
            checkAndModifyControlName(control, Project.GetAllControls());
            if (container == null && SelectedItem != null && SelectedItem.IsContainer)
                container = SelectedItem;
            if (container == null)
                container = Project;
            //判断粘贴的控件有没有超出容器可视范围，则移动到可视范围内
            if (control.Left + control.Width > container.Width)
                control.Left = container.Width - control.Width;
            if (control.Top + control.Height > container.Height)
                control.Top = container.Height - control.Height;
            if (control.Left < 0)
                control.Left = 0;
            if (control.Top < 0)
                control.Top = 0;
            control.Parent = container;
            container.Controls.Add(control);
        }

        private void checkAndModifyControlName(BaseControl control, IEnumerable<BaseControl> controlContainer)
        {
            var controlAttr = ControlManager.Instance.GetItem(control.GetType().FullName);
            var index = 0;
            do
            {
                index++;
                var name = controlAttr.Name + index;
                if (controlContainer.Any(t => t.Name == name))
                    continue;
                control.Name = name;
                break;
            }
            while (true);
        }

        private async Task copyControl()
        {
            //如果不是选中了控件
            if (!IsSelectedControl)
                return;
            try
            {
                var content = XmlConvert.Serialize(SelectedItem);
                await ClipboardUtils.WriteText(JSRuntime, content);
            }
            catch (Exception ex)
            {
                modalAlert.Show("错误", ExceptionUtils.GetExceptionMessage(ex));
            }
        }

        private async Task pasteControl()
        {
            try
            {
                var content = await ClipboardUtils.ReadText(JSRuntime);
                if (string.IsNullOrEmpty(content))
                    throw new IOException("剪贴板为空");
                var obj = XmlConvert.Deserialize(content);
                if (obj is not BaseControl control)
                    throw new IOException("剪贴板中的内容不是控件");
                if (obj is MultiControl multiControl)
                {
                    foreach (var item in multiControl.Items)
                        addControl(item);
                    SelectedItem = new MultiControl(Project, Project.DesignGridSize, multiControl.Items);
                }
                else
                {
                    addControl(control);
                    SelectedItem = control;
                }
            }
            catch (Exception ex)
            {
                modalAlert.Show("错误", ExceptionUtils.GetExceptionMessage(ex));
            }
        }

        //移除控件
        private void removeControl()
        {
            //如果不是选中了控件
            if (!IsSelectedControl)
                return;
            if (SelectedItem is MultiControl multiControl)
            {
                foreach (var item in multiControl.Items)
                    removeControl(item);
                SelectedItem = multiControl.Container;
            }
            else
            {
                var parent = SelectedItem.Parent;
                removeControl(SelectedItem);
                SelectedItem = parent;
            }
        }

        private void removeControl(BaseControl control)
        {
            if (control.Parent == null)
                return;
            control.Parent.Controls.Remove(control);
            control.Parent = null;
        }

        private void setControlIndex(BaseControl parent, BaseControl item, int index)
        {
            parent.Controls.Remove(item);
            if (index < 0)
                index = 0;
            if (index > parent.Controls.Count)
                index = parent.Controls.Count;
            parent.Controls.Insert(index, item);
        }
        private int getControlIndex(BaseControl parent, BaseControl item) => parent.Controls.IndexOf(item);
        private void setControlIndex(Func<int, int> getNewIndexFunc)
        {
            var item = SelectedItem;
            if (item == null
                || !item.Visiable)
                return;
            if (item is MultiControl)
                return;
            var parent = item.Parent;
            setControlIndex(parent, item, getNewIndexFunc(getControlIndex(parent, item)));
            SelectedItem = item;
        }

        //鼠标按下时控件信息
        public ControlMeta MouseDownControlMeta { get; set; }

        private void executeShotcut(IControlShotcut shotcut)
        {
            try
            {
                shotcut.ExecuteCommand.Execute(null);
                onDesignChanged();
            }
            catch (Exception ex)
            {
                modalAlert.Show("错误", $"执行快捷操作[{shotcut.Name}]时出错，原因：{ExceptionUtils.GetExceptionMessage(ex)}");
            }
        }
    }
}
