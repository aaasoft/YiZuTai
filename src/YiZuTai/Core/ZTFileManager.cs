using System.IO.Compression;
using Newtonsoft.Json;
using YiZuTai.Model;

namespace YiZuTai.Core
{
    public class ZTFileManager
    {
        public static ZTFileManager Instance { get; } = new ZTFileManager();
        private Dictionary<string, ZTFile> fileDict = null;
        private string dataFolder;
        private const string GROUPS_FILE = "groups.json";
        private Dictionary<string, ZTGroup> groupDict;

        public void Init(string dataFolder)
        {
#if DEBUG
            dataFolder = Path.Combine(AppContext.BaseDirectory, dataFolder);
#else
            dataFolder = Path.GetFullPath(dataFolder);
#endif
            if (!Directory.Exists(dataFolder))
                Directory.CreateDirectory(dataFolder);
            this.dataFolder = dataFolder;
            //加载分组配置
            var groupConfigFile = Path.Combine(dataFolder, GROUPS_FILE);
            if (File.Exists(groupConfigFile))
            {
                var json = File.ReadAllText(groupConfigFile);
                groupDict = JsonConvert.DeserializeObject<ZTGroup[]>(json).ToDictionary(t => t.Id, t => t);
            }
            if (groupDict == null)
                groupDict = new();

            //加载组态文件
            fileDict = new Dictionary<string, ZTFile>();
            if (Directory.Exists(dataFolder))
            {
                foreach (var folder in Directory.GetDirectories(dataFolder))
                {
                    try
                    {
                        var model = LoadFile(folder);
                        fileDict[model.Id] = model;
                    }
                    catch { }
                }
            }
        }

        private ZTFile LoadFile(string folder)
        {
            var metaFile = Path.Combine(folder, ZTFile.META_FILE);
            if (!File.Exists(metaFile))
                return null;
            var content = File.ReadAllText(metaFile);
            var model = JsonConvert.DeserializeObject<ZTFile>(content);
            return model;
        }

        public bool IsEmpty()
        {
            lock (fileDict)
                return fileDict.Count <= 0;
        }

        public ZTFile Get(string id)
        {
            lock (fileDict)
                if (fileDict.ContainsKey(id))
                    return fileDict[id];
            return null;
        }

        public ZTFile[] Query(string groupId, string keywords)
        {
            lock (fileDict)
            {
                IEnumerable<ZTFile> query = fileDict.Values;
                if (!string.IsNullOrEmpty(groupId))
                    query = query.Where(t => groupId == t.GroupId);
                if (!string.IsNullOrEmpty(keywords))
                    query = query.Where(t => t.Name.Contains(keywords));
                return query.ToArray();
            }
        }

        public void Add(ZTFile model)
        {
            lock (fileDict)
            {
                if (fileDict.ContainsKey(model.Id))
                    return;
                fileDict[model.Id] = model;
                model.Save();
            }
        }

        private void SaveGroupConfig()
        {
            var json = JsonConvert.SerializeObject(groupDict.Values.ToArray());
            File.WriteAllText(Path.Combine(dataFolder, GROUPS_FILE), json);
        }

        public ZTGroup GetGroup(string groupId)
        {
            lock (groupDict)
            {
                if (groupDict.TryGetValue(groupId, out var model))
                    return model;
                return null;
            }
        }

        public ZTGroup[] GetGroups(string parentId)
        {
            lock (groupDict)
            {
                return groupDict.Values.Where(t => t.ParentId == parentId).ToArray();
            }
        }

        public void AddGroup(ZTGroup model)
        {
            lock (groupDict)
            {
                if (groupDict.ContainsKey(model.Id))
                    throw new IOException($"已经存在编号为[{model.Id}]的分组");
                if (model.ParentId != null && !groupDict.ContainsKey(model.ParentId))
                    throw new IOException($"未找到编号为[{model.ParentId}]的父分组");
                groupDict[model.Id] = model;
                SaveGroupConfig();
            }
        }

        public void UpdateGroup(ZTGroup model)
        {
            SaveGroupConfig();
        }

        public void DeleteGroup(string id)
        {
            lock (groupDict)
            {
                if (!groupDict.ContainsKey(id))
                    return;
                groupDict.Remove(id);
                SaveGroupConfig();
            }
        }
        
        public void Update(ZTFile model)
        {
            model.Save();
        }

        public void Delete(string id)
        {
            lock (fileDict)
            {
                if (!fileDict.ContainsKey(id))
                    return;
                var model = fileDict[id];
                Directory.Delete(model.Folder, true);
                fileDict.Remove(id);
            }
        }

        public void ChangeId(string source, string destination)
        {
            lock (fileDict)
            {
                if (fileDict.ContainsKey(destination))
                    throw new IOException($"编号为[{destination}]的组态文件已经存在");
                if (!fileDict.TryGetValue(source, out var model))
                    return;
                Directory.Move(GetFileFolder(source), GetFileFolder(destination));
                fileDict.Remove(source);
                fileDict[destination] = model;
            }
        }

        public string GetDataFolder() => dataFolder;

        public string GetFileFolder(string id)
        {
            return Path.Combine(dataFolder, id);
        }

        public void Export(string id, string destination)
        {
            try
            {
                using (var fs = File.OpenWrite(destination))
                    Export(id, fs);
            }
            catch
            {
                File.Delete(destination);
                throw;
            }
        }

        public void Export(string id, Stream destination)
        {
            var file = Get(id);
            if (file == null)
                throw new FileNotFoundException($"未找到编号为[{id}]的组态文件");
            ZipFile.CreateFromDirectory(file.Folder, destination);
        }

        public ZTFile Import(string source, string groupId)
        {
            using (var fs = File.OpenRead(source))
                return Import(fs, groupId);
        }

        public ZTFile Import(Stream source, string groupId)
        {
            var tmpId = Guid.NewGuid().ToString("N");
            var folder = GetFileFolder(tmpId);
            try
            {
                ZipFile.ExtractToDirectory(source, folder);
                var model = LoadFile(folder);
                var existedModel = Get(model.Id);
                if (existedModel == null)
                {
                    Directory.Move(folder, GetFileFolder(model.Id));
                }
                else
                {
                    model.Id = tmpId;
                }
                lock (fileDict)
                    fileDict[model.Id] = model;
                model.GroupId = groupId;
                model.Save();
                return model;
            }
            catch
            {
                Directory.Delete(folder, true);
                throw;
            }
        }
    }
}
