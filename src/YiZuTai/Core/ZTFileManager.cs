using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using YiZuTai.Core.Utils;

namespace YiZuTai.Core
{
    public class ZTFileManager
    {
        public static ZTFileManager Instance { get; } = new ZTFileManager();
        private Dictionary<string, ZTFile> fileDict = null;

        public void Init()
        {
            fileDict = new Dictionary<string, ZTFile>();
            var ztFileFolder = FolderUtils.GetPathUnderDataDir(ZTFile.ZTFILE_FOLDER);
            if (Directory.Exists(ztFileFolder))
            {
                foreach (var folder in Directory.GetDirectories(ztFileFolder))
                {
                    var metaFile = Path.Combine(folder, ZTFile.META_FILE);
                    if (!File.Exists(metaFile))
                        continue;
                    try
                    {
                        var content = File.ReadAllText(metaFile);
                        var model = JsonConvert.DeserializeObject<ZTFile>(content);
                        model.Id = Path.GetFileName(folder);
                        fileDict[model.Id] = model;
                    }
                    catch { }
                }
            }
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

        public ZTFile[] Query(string keywords)
        {
            lock (fileDict)
            {
                IEnumerable<ZTFile> query = fileDict.Values;
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
    }
}
