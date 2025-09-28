using UnityEngine;

public class DebugingTool
{
    private static DebugingTool _debugingTool;
    public static DebugingTool debugingTool{ get { return _debugingTool; } }

    public void DebugLog(string Log)
    {
#if DEBUG
        Debug.Log("Log");
#endif
    }
}
