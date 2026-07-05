using Microsoft.AspNetCore.Builder;
using Newtonsoft.Json;
using YiZuTai.Core;

namespace YiZuTai.Model
{
    /// <summary>
    /// 组态文件类
    /// </summary>
    public class ZTFile
    {
        public const string ZTFILE_RESOURCE_FOLDER = "Resource";
        public const string META_FILE = "meta.json";
        public const string CONTENT_FILE = "content.xml";

        /// <summary>
        /// 编号
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 目录
        /// </summary>
        [JsonIgnore]
        public string Folder => ZTFileManager.Instance.GetFileFolder(Id);
        /// <summary>
        /// 资源目录
        /// </summary>
        [JsonIgnore]
        public string ResourceFolder => Path.Combine(Folder, ZTFILE_RESOURCE_FOLDER);

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 分组编号
        /// </summary>
        public string GroupId { get; set; }

        public Project OpenProject()
        {
            var projectFile = Path.Combine(Folder, CONTENT_FILE);
            Project project = null;
            //如果文件存在
            if (File.Exists(projectFile))
            {
                var content = File.ReadAllText(projectFile);
                project = Quick.Xml.XmlConvert.Deserialize<Project>(content);
            }
            if (project == null)
                project = new Project();
            project.IsProjectLoaded = true;
            return project;
        }

        public void Save()
        {
            var folder = Folder;
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            File.WriteAllText(Path.Combine(folder, META_FILE), JsonConvert.SerializeObject(this));
        }

        public void SaveProject(Project project)
        {
            var folder = Folder;
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            File.WriteAllText(Path.Combine(folder, CONTENT_FILE), Quick.Xml.XmlConvert.Serialize(project));
        }

        public string GetStaticResourceUrl(string resourceUrl)
        {
            return GetStaticResourceUrl(Id, resourceUrl);
        }

        private static string GetStaticResourceUrl(string ztFileId, string resourceUrl)
        {
            if (resourceUrl == null || resourceUrl.StartsWith("/"))
                return resourceUrl;
            return YiZuTaiMiddlewareExtensions.GetUrlWithPrefix($"/{ztFileId}/{ZTFILE_RESOURCE_FOLDER}/{resourceUrl}");
        }
    }
}
