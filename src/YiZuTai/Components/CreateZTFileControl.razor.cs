using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;

namespace YiZuTai.Components
{
    public partial class CreateZTFileControl
    {
        public class CreateZTFileControlModel
        {
            [Required(ErrorMessage = "必须输入名称")]
            public string Name { get; set; }
            public string Description { get; set; }
            public string[] Tags { get; set; }
        }

        public class SelectTagInfo
        {
            public string Name { get; set; }
            public bool Checked { get; set; }
        }

        private SelectTagInfo[] selectTags;

        private CreateZTFileControlModel createZTFileModel = new CreateZTFileControlModel();
        [Parameter]
        public Core.ZTFile ZTFileInfo { get; set; }
        [Parameter]
        public Action<CreateZTFileControlModel> OkAction { get; set; }

        private void Ok()
        {
            createZTFileModel.Tags = selectTags?.Where(t => t.Checked).Select(t => t.Name).ToArray();
            OkAction?.Invoke(createZTFileModel);
        }

        private void checkTag(SelectTagInfo tag)
        {
            tag.Checked = !tag.Checked;
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            selectTags = Core.TagManager.Instance.GetTags()?.Select(t => new SelectTagInfo() { Name = t }).ToArray();
            if (ZTFileInfo != null)
            {
                createZTFileModel.Name = ZTFileInfo.Name;
                createZTFileModel.Description = ZTFileInfo.Description;
                createZTFileModel.Tags = ZTFileInfo.Tags;
                if (createZTFileModel.Tags != null && selectTags != null)
                {
                    foreach (var tag in createZTFileModel.Tags)
                    {
                        var model = selectTags.FirstOrDefault(t => t.Name == tag);
                        if (model == null)
                            continue;
                        model.Checked = true;
                    }
                }
            }
        }

        public static Dictionary<string, object> PrepareParameter(Core.ZTFile containerInfo, Action<CreateZTFileControlModel> okAction)
        {
            return new Dictionary<string, object>()
            {
                [nameof(ZTFileInfo)] = containerInfo,
                [nameof(OkAction)] = okAction
            };
        }
    }
}
