namespace YiZuTai.TypeEditors
{
    /// <summary>
    /// SourceTypeEditor.xaml 的交互逻辑
    /// </summary>
    public partial class SourceTypeEditor { }
    /*
     : UserControl, ITypeEditor
    {
        public SourceTypeEditor()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public PropertyItem PropertyItem { get; private set; }

        private void refreshButtonText()
        {
            btnMain.Content = PropertyItem.Value ?? "无";
        }

        public FrameworkElement ResolveEditor(PropertyItem propertyItem)
        {
            this.PropertyItem = propertyItem;
            propertyItem.PropertyChanged += PropertyItem_PropertyChanged;
            refreshButtonText();
            return this;
        }

        private void PropertyItem_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(PropertyItem.Value):
                    refreshButtonText();
                    break;
            }
        }

        private void BtnMain_Click(object sender, RoutedEventArgs e)
        {
            var hostWin = Window.GetWindow(this);
            var win = new View.SourceSelectWindow(PropertyItem.PropertyType, (ISource)PropertyItem.Value);
            win.Owner = hostWin;
            win.ShowDialog();
            if (win.Source != PropertyItem.Value)
                PropertyItem.Value = win.Source;
        }

        private ISource getSourceFromClipboard()
        {
            try
            {
                if (!Clipboard.ContainsText())
                    return null;
                var text = Clipboard.GetText();
                var model = XmlConvert.Deserialize(text) as ISource;
                return model;
            }
            catch { return null; }
        }

        private void ContextMenu_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            miCopy.Visibility = PropertyItem.Value == null ? Visibility.Collapsed : Visibility.Visible;
            var source = getSourceFromClipboard();
            if (source != null && PropertyItem.PropertyType.IsInstanceOfType(source))
                miPaste.Visibility = Visibility.Visible;
            else
                miPaste.Visibility = Visibility.Collapsed;
        }

        private void MiCopy_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(XmlConvert.Serialize(PropertyItem.Value));
        }

        private void MiPaste_Click(object sender, RoutedEventArgs e)
        {
            var newValue = getSourceFromClipboard();
            PropertyItem.Value = newValue;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            PropertyItem.Value = null;
        }
    }
    */
}
