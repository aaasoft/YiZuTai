using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YiZuTai.Design
{
    public class EditorManager
    {
        public static EditorManager Instance { get; } = new EditorManager();
        private Dictionary<Type, Type> editorDict = new Dictionary<Type, Type>();
        public EditorManager()
        {
            editorDict[typeof(string)] = typeof(Editor.EditorInputText);
            editorDict[typeof(int)] = typeof(Editor.EditorInputNumber);
            editorDict[typeof(uint)] = typeof(Editor.EditorInputNumber);
            editorDict[typeof(long)] = typeof(Editor.EditorInputNumber);
            editorDict[typeof(ulong)] = typeof(Editor.EditorInputNumber);
            editorDict[typeof(float)] = typeof(Editor.EditorInputNumber);
            editorDict[typeof(double)] = typeof(Editor.EditorInputNumber);
            editorDict[typeof(decimal)] = typeof(Editor.EditorInputNumber);
            editorDict[typeof(bool)] = typeof(Editor.EditorInputSwitch);
        }

        /// <summary>
        /// 获取编辑器类型
        /// </summary>
        /// <param name="fieldType"></param>
        /// <returns></returns>
        public Type GetEditorType(Type fieldType)
        {
            if (fieldType.IsEnum)
                return typeof(Editor.EditorInputSelect);
            if (editorDict.ContainsKey(fieldType))
                return editorDict[fieldType];
            return typeof(Editor.EditorInputDisplay);
        }
    }
}
