using System.Collections;
using YiZuTai.Components.Editors;
using YiZuTai.DataSource;
using YiZuTai.HyperLinkSource;

namespace YiZuTai.Core
{
    public class EditorManager
    {
        public static EditorManager Instance { get; } = new EditorManager();
        private Dictionary<Type, Type> editorDict = new Dictionary<Type, Type>();
        public EditorManager()
        {
            editorDict[typeof(string)] = typeof(EditorInputText);
            editorDict[typeof(int)] = typeof(EditorInputNumber_Int32);
            editorDict[typeof(int?)] = typeof(EditorInputNumber_Int32_Nullable);
            editorDict[typeof(uint)] = typeof(EditorInputNumber_UInt32);
            editorDict[typeof(uint?)] = typeof(EditorInputNumber_UInt32_Nullable);
            editorDict[typeof(long)] = typeof(EditorInputNumber_Int64);
            editorDict[typeof(long?)] = typeof(EditorInputNumber_Int64_Nullable);
            editorDict[typeof(ulong)] = typeof(EditorInputNumber_UInt64);
            editorDict[typeof(ulong?)] = typeof(EditorInputNumber_UInt64_Nullable);
            editorDict[typeof(float)] = typeof(EditorInputNumber_Single);
            editorDict[typeof(float?)] = typeof(EditorInputNumber_Single_Nullable);
            editorDict[typeof(double)] = typeof(EditorInputNumber_Double);
            editorDict[typeof(double?)] = typeof(EditorInputNumber_Double_Nullable);
            editorDict[typeof(decimal)] = typeof(EditorInputNumber_Decimal);
            editorDict[typeof(decimal?)] = typeof(EditorInputNumber_Decimal_Nullable);
            editorDict[typeof(short)] = typeof(EditorInputNumber_Int16);
            editorDict[typeof(short?)] = typeof(EditorInputNumber_Int16_Nullable);
            editorDict[typeof(ushort)] = typeof(EditorInputNumber_UInt16);
            editorDict[typeof(ushort?)] = typeof(EditorInputNumber_UInt16_Nullable);
            editorDict[typeof(byte)] = typeof(EditorInputNumber_Byte);
            editorDict[typeof(byte?)] = typeof(EditorInputNumber_Byte_Nullable);
            editorDict[typeof(sbyte)] = typeof(EditorInputNumber_SByte);
            editorDict[typeof(sbyte?)] = typeof(EditorInputNumber_SByte_Nullable);
            editorDict[typeof(DateTime)] = typeof(EditorInputDate_DateTime);
            editorDict[typeof(DateTime?)] = typeof(EditorInputDate_DateTime_Nullable);
            editorDict[typeof(bool)] = typeof(EditorInputSwitch);
            editorDict[typeof(bool?)] = typeof(EditorInputSwitch_Nullable);
            editorDict[typeof(IDataSource)] = typeof(SourceTypeEditor);
            editorDict[typeof(IHyperLinkSource)] = typeof(SourceTypeEditor);
            editorDict[typeof(IQueryDataSource)] = typeof(SourceTypeEditor);
        }

        /// <summary>
        /// 获取编辑器类型
        /// </summary>
        /// <param name="fieldType"></param>
        /// <returns></returns>
        public Type GetEditorType(Type fieldType)
        {
            if (fieldType.IsEnum)
                return typeof(EditorInputSelect);
            if (typeof(IList).IsAssignableFrom(fieldType))
                return typeof(CollectionEditor);

            if (fieldType.IsGenericType && fieldType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    var genericArgument1 = fieldType.GetGenericArguments().FirstOrDefault();
                    if (genericArgument1.IsEnum)
                        return typeof(EditorInputSelect_Nullable);
                }

            if (editorDict.ContainsKey(fieldType))
                return editorDict[fieldType];
            return typeof(EditorInputDisplay);
        }
    }
}
