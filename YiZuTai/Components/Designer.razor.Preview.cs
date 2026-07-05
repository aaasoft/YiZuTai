using System;

namespace YiZuTai.Components;

public partial class Designer
{
    private string designerId = Guid.NewGuid().ToString("N");
    private static Dictionary<string, Designer> designerDict = new Dictionary<string, Designer>();
    public static Designer GetDesigner(string id)
    {
        lock (designerDict)
            if (designerDict.TryGetValue(id, out var designer))
                return designer;
        return null;
    }

    private static void registerDesigner(string id, Designer designer)
    {
        lock (designerDict)
            designerDict[id] = designer;
    }

    private static void unregisterDesigner(string id)
    {
        lock (designerDict)
            if (designerDict.ContainsKey(id))
                designerDict.Remove(id);
    }
}
