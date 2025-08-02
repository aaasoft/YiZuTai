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
            if (Model.Value != null)
                Model.Value.PropertyChanged += OnModelValue_PropertyChanged;
            Model.PropertyChanging += OnModel_PropertyChanging;
            Model.PropertyChanged += OnModel_PropertyChanged;
        }

        public void Dispose()
        {
            Model.PropertyChanging -= OnModel_PropertyChanging;
            Model.PropertyChanged -= OnModel_PropertyChanged;
        }

        private void OnModel_PropertyChanging(object sender, PropertyChangingEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Model.Value):
                    if (Model.Value != null)
                        Model.Value.PropertyChanged -= OnModelValue_PropertyChanged;
                    break;
            }
        }

        private void OnModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Model.Value):
                    if (Model.Value != null)
                        Model.Value.PropertyChanged += OnModelValue_PropertyChanged;
                    break;
            }
            InvokeAsync(StateHasChanged);
        }

        private void OnModelValue_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            InvokeAsync(StateHasChanged);
        }
    }
}
