using Microsoft.AspNetCore.Components;
using YiZuTai.Design;

namespace YiZuTai.Control.TextBlock
{
    public partial class View
    {
        private BaseControl_WithBackgroundView baseControlView;

        [Parameter]
        public Model Model { get; set; }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                baseControlView.RegisterDataSource(Model,
                [
                    new (nameof(Model.Value), () => Model.Value )
                ]);
            }
        }
    }
}
