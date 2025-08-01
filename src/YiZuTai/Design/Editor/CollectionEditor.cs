namespace YiZuTai.Design.Editor
{
    public class CollectionEditor { }
    /*
     : Xceed.Wpf.Toolkit.PropertyGrid.Editors.CollectionEditor
    {
        private string beforeValueXml = null;
        private CollectionControlButton button;
        protected override CollectionControlButton CreateEditor()
        {
            button = base.CreateEditor();
            button.Click += Button_Click;
            return button;
        }

        private class Container
        {
            public object Value { get; set; }
        }

        protected override void ResolveValueBinding(PropertyItem propertyItem)
        {
            beforeValueXml = XmlConvert.Serialize(new Container() { Value = propertyItem.Value });
            base.ResolveValueBinding(propertyItem);
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var itemSource = button.ItemsSource;
            var afterValueXml = XmlConvert.Serialize(new Container() { Value = itemSource });
            if (beforeValueXml != afterValueXml)
                button.ItemsSource = itemSource;
        }
    }
    */
}
