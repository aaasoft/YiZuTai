using System.ComponentModel;
using Microsoft.AspNetCore.Components;

namespace YiZuTai.Control.TextBlock
{
    public partial class View : IDisposable
    {
        [Parameter]
        public Model Model { get; set; }

        protected override void OnParametersSet()
        {
            Model.PropertyChanged += OnModel_PropertyChanged;
        }

        public void Dispose()
        {
            Model.PropertyChanged -= OnModel_PropertyChanged;
        }

        private void OnModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Model.Text):
                    InvokeAsync(StateHasChanged);
                    break;
            }
        }
    }
}
