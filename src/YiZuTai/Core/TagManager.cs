using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using YiZuTai.Core.Utils;

namespace YiZuTai.Core
{
    public class TagManager
    {
        public static TagManager Instance { get; } = new TagManager();
        public const string TAGS_DATA_FILE = "tags.json";
        private List<string> tagList = null;

        public bool IsEmpty()
        {
            lock (tagList)
                return tagList.Count == 0;
        }

        public string[] GetTags() => tagList.ToArray();

        public void Init()
        {
            var tagsDataFile = FolderUtils.GetPathUnderDataDir(TAGS_DATA_FILE);
            if (File.Exists(tagsDataFile))
            {
                var content = File.ReadAllText(tagsDataFile);
                tagList = JsonConvert.DeserializeObject<List<string>>(content);
            }
            if (tagList == null)
                tagList = new List<string>();
        }

        private void Save()
        {
            var tagsDataFile = FolderUtils.GetPathUnderDataDir(TAGS_DATA_FILE);
            File.WriteAllText(tagsDataFile, JsonConvert.SerializeObject(tagList));
        }

        public bool Contains(string tag)
        {
            lock (tagList)
                return tagList.Contains(tag);
        }

        public void Add(string tag)
        {
            lock (tagList)
            {
                tagList.Add(tag);
                Save();
            }
        }

        public void MoveLeft(string tag)
        {
            lock (tagList)
            {
                var index = tagList.IndexOf(tag);
                if (index <= 0)
                    return;
                tagList.RemoveAt(index);
                tagList.Insert(index - 1, tag);
                Save();
            }
        }

        public void MoveRight(string tag)
        {
            lock (tagList)
            {
                var index = tagList.IndexOf(tag);
                if (index >= tagList.Count - 1)
                    return;
                tagList.RemoveAt(index);
                tagList.Insert(index + 1, tag);
                Save();
            }
        }

        public void Delete(string tag)
        {
            var files = ZTFileManager.Instance.Query(tag, null);
            if (files.Length > 0)
                throw new ApplicationException($"文件[{files[0].Name}]正在使用此标签");
            lock (tagList)
            {
                while (tagList.Contains(tag))
                    tagList.Remove(tag);
                Save();
            }
        }
    }
}
