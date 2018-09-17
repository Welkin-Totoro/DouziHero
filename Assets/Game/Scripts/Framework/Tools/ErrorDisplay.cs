using UnityEngine;

public class ErrorDisplay : MonoBehaviour
{
    internal void OnEnable()
    {
        //Application.RegisterLogCallback(HandleLog);
        Application.logMessageReceived += HandleLog;
    }

    internal void OnDisable()
    {
        //Application.RegisterLogCallback(null);
        Application.logMessageReceived -= HandleLog;
    }

    private string m_logs;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="logString">错误信息</param>
    /// <param name="stackTrace">跟踪堆栈</param>
    /// <param name="type">错误类型</param>
    void HandleLog(string logString, string stackTrace, LogType type)
    {
        m_logs += logString + "\n";
    }

    public bool Log;
    private Vector2 m_scroll;
    internal void OnGUI()
    {
        if (!Log)
            return;
        m_scroll = GUILayout.BeginScrollView(m_scroll);
        GUILayout.Label(m_logs);
        GUILayout.EndScrollView();
    }
}