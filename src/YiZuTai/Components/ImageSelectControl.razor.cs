using Microsoft.AspNetCore.Components;
using YiZuTai.Model;

namespace YiZuTai.Components
{
    public partial class ImageSelectControl
    {
        private static HashSet<string> imageFileSuffixHashSet = new HashSet<string> { ".jpg", ".png", ".svg", ".gif", ".tif", ".bmp" };
        private readonly YiZuTai.Utils.UnitStringConverting storageUSC = YiZuTai.Utils.UnitStringConverting.StorageUnitStringConverting;
        private readonly YiZuTai.Utils.UnitStringConverting timeUSC = YiZuTai.Utils.UnitStringConverting.TimeUnitStringConverting;

        [Parameter]
        public ZTFile ZTFile { get; set; }
        [Parameter]
        public string SelectedItem { get; set; }
        [Parameter]
        public Action<string> OkAction { get; set; }
        private string[] Images { get; set; }
        private string CurrentPath { get; set; }

        protected override void OnParametersSet()
        {
            CurrentPath = ZTFile.ResourceFolder;
            refresh();
        }

        private string getImageSize(string id)
        {
            var file = Path.Combine(CurrentPath, id);
            var fileInfo = new FileInfo(file);
            if (!fileInfo.Exists)
                return "";
            return storageUSC.GetString(fileInfo.Length, 0, true) + "B";
        }

        private void refresh()
        {
            List<string> list = new List<string>();
            if (Directory.Exists(CurrentPath))
            {
                foreach (var file in Directory.GetFiles(CurrentPath, "*", SearchOption.AllDirectories))
                {
                    var extension = Path.GetExtension(file);
                    if (!imageFileSuffixHashSet.Contains(extension))
                        continue;
                    var id = file.Substring(CurrentPath.Length);
                    id = id.Replace(@"\", "/");
                    while (id.StartsWith("/"))
                        id = id.Substring(1);
                    list.Add(id);
                }
            }
            Images = list.ToArray();
        }

        private void btnOk_Click()
        {
            OkAction?.Invoke(SelectedItem);
        }
    }
}
