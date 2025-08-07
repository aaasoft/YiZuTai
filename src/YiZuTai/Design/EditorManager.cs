namespace YiZuTai.Design
{
    public class EditorManager
    {
        public static EditorManager Instance { get; } = new EditorManager();
        private Dictionary<Type, Type> editorDict = new Dictionary<Type, Type>();
        public EditorManager()
        {
            editorDict[typeof(string)] = typeof(Editor.EditorInputText);
            editorDict[typeof(int)] = typeof(Editor.EditorInputNumber_Int32);
            editorDict[typeof(int?)] = typeof(Editor.EditorInputNumber_Int32_Nullable);
            editorDict[typeof(uint)] = typeof(Editor.EditorInputNumber_UInt32);
            editorDict[typeof(uint?)] = typeof(Editor.EditorInputNumber_UInt32_Nullable);
            editorDict[typeof(long)] = typeof(Editor.EditorInputNumber_Int64);
            editorDict[typeof(long?)] = typeof(Editor.EditorInputNumber_Int64_Nullable);
            editorDict[typeof(ulong)] = typeof(Editor.EditorInputNumber_UInt64);
            editorDict[typeof(ulong?)] = typeof(Editor.EditorInputNumber_UInt64_Nullable);
            editorDict[typeof(float)] = typeof(Editor.EditorInputNumber_Single);
            editorDict[typeof(float?)] = typeof(Editor.EditorInputNumber_Single_Nullable);
            editorDict[typeof(double)] = typeof(Editor.EditorInputNumber_Double);
            editorDict[typeof(double?)] = typeof(Editor.EditorInputNumber_Double_Nullable);
            editorDict[typeof(decimal)] = typeof(Editor.EditorInputNumber_Decimal);
            editorDict[typeof(decimal?)] = typeof(Editor.EditorInputNumber_Decimal_Nullable);
            editorDict[typeof(bool)] = typeof(Editor.EditorInputSwitch);
            editorDict[typeof(bool?)] = typeof(Editor.EditorInputSwitch_Nullable);
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
