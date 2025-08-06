using Microsoft.AspNetCore.Components;
using YiZuTai.Design;

namespace YiZuTai.Control.TextBlock
{
    public partial class View
    {
        private BaseControl_WithBackgroundAndAnimateView baseControlView;

        [Parameter]
        public Model Model { get; set; }
        
/*
                protected override void OnParametersSet()
                {
                    Model.PropertyChanged += Model_OnPropertyChanged;
                }

                private void Model_OnPropertyChanged(object sender, object e)
                {
                    InvokeAsync(StateHasChanged);
                }

                protected override void OnAfterRender(bool firstRender)
                {
                    base.OnAfterRender(firstRender);

                    if (firstRender)
                    {
                        baseControlView.RegisterDataSource(Model,
                        [
                            new (nameof(Model.Value), () => Model.Value )
                        ]);
                    }
                }
                */
    }
}
