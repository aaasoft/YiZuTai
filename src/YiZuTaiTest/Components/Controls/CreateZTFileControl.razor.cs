using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;
using YiZuTai.Core;

namespace YiZuTaiTest.Components.Controls
{
    public partial class CreateZTFileControl
    {
        public class CreateZTFileControlModel
        {
            [Required(ErrorMessage = "必须输入名称")]
            public string Name { get; set; }
            public string Description { get; set; }
        }
        private CreateZTFileControlModel createZTFileModel = new CreateZTFileControlModel();
        [Parameter]
        public ZTFile ZTFileInfo { get; set; }
        [Parameter]
        public Action<CreateZTFileControlModel> OkAction { get; set; }

        private void Ok()
        {
            OkAction?.Invoke(createZTFileModel);
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            if (ZTFileInfo != null)
            {
                createZTFileModel.Name = ZTFileInfo.Name;
                createZTFileModel.Description = ZTFileInfo.Description;
            }
        }
    }
}
