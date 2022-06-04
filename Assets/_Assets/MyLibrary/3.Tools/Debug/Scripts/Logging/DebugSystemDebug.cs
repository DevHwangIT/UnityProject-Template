using UnityEngine;
using System.Collections.Generic;

namespace MyLibrary.Tools
{
    public static class DebugSystemDebug
    {
        #region Structure
        public struct DebugLog
        {
            private System.DateTime logTime;
            private UnityEngine.LogType type;
            private string msg;
            private string trace;

            public DebugLog(LogType _type, string _msg, string _trace)
            {
                logTime = System.DateTime.Now;
                type = _type;
                msg = _msg;
                trace = _trace;
            }

            public System.DateTime GetLogTime => logTime;
            public LogType GetLogType => type;
            public string GetMsg => msg;
            public string GetTrace => trace;
        }
        #endregion

        #region LogFunction
        private static List<DebugLog> logList = new List<DebugLog>();
        public static int GetCount() { return logList.Count;}
        public static List<DebugLog> GetList()
        {
            return logList;
        }
        public static bool TryGetIndex(int index, out DebugLog logData)
        {
            if (logList.Count <= index)
            {
                logData = new DebugLog(LogType.Log, "", "");
                return false;
            }
            else
            {
                logData = logList[index];
                return true;
            }
        }
        public static void Clear()
        {
            logList.Clear();
        }
        #endregion

        public static void Run()
        {
            Application.logMessageReceived += Application_Log;
        }
        
        public static void Pause()
        {
            Application.logMessageReceived -= Application_Log;
        }

        private static void Application_Log(string condition, string stackTrace, LogType type)
        {
            logList.Add(new DebugLog(type, condition, stackTrace));
        }
    }
}