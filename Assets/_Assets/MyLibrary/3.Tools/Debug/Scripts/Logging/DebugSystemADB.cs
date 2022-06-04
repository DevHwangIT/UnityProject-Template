//TODO : ADB 관련 작업 추가 구현하기. [ ADB Message -> Unity Log -> DebugSystemLogging ]

// #if PLATFORM_ANDROID
// using System;
// using System.Collections;
// using System.Collections.Generic;
// using System.Diagnostics;
// using System.Text;
// using UnityEditor;
// using UnityEngine;
// using Debug = UnityEngine.Debug;
//
// public class DebugSystemADB : MonoBehaviour
// {
//     private List<string> mADBMsgs = new List<string>();
//     Process mADBProcess;
//
//     void StartADB()
//     {
//         if (mADBProcess != null) return;
//
//         Console.InputEncoding = Encoding.UTF8;
//         Console.OutputEncoding = Encoding.UTF8;
//
//         mADBProcess = new Process();
//         mADBProcess.StartInfo.FileName = "adb.exe";
//         mADBProcess.StartInfo.Arguments = "shell";
//         mADBProcess.StartInfo.UseShellExecute = false;
//         mADBProcess.StartInfo.CreateNoWindow = true;
//         mADBProcess.StartInfo.RedirectStandardError = true;
//         mADBProcess.StartInfo.RedirectStandardOutput = true;
//         mADBProcess.StartInfo.RedirectStandardInput = true;
//         mADBProcess.OutputDataReceived += OnReceived;
//         mADBProcess.ErrorDataReceived += OnReceived;
//         mADBProcess.StartInfo.StandardOutputEncoding = Encoding.UTF8;
//         mADBProcess.StartInfo.StandardErrorEncoding = Encoding.UTF8;
//         mADBProcess.Start();
//
//         using (var tStream = mADBProcess.StandardInput)
//         {
//             tStream.WriteLine("logcat");
//             tStream.Close();
//         }
//
//         EditorApplication.delayCall += () =>
//         {
//             mADBProcess.BeginOutputReadLine();
//             mADBProcess.BeginErrorReadLine();
//
//             EditorApplication.update -= ParseADBMsg;
//             EditorApplication.update += ParseADBMsg;
//         };
//     }
//
//     void StopADB()
//     {
//         if (mADBProcess == null) return;
//
//         try
//         {
//             if (mADBProcess != null && !mADBProcess.HasExited)
//             {
//                 mADBProcess.CancelOutputRead();
//                 mADBProcess.CancelErrorRead();
//                 mADBProcess.Kill();
//             }
//
//             EditorApplication.update -= ParseADBMsg;
//         }
//         catch (Exception ex)
//         {
//             Debug.LogException(ex);
//         }
//         finally
//         {
//             mADBProcess = null;
//         }
//     }
//
//     void OnReceived(object sender, DataReceivedEventArgs e)
//     {
//         if (e.Data == null) return;
//
//         var tMsg = e.Data;
//
// #if !UNITY_2017_3_OR_NEWER
//         var tBytes = Encoding.Default.GetBytes(tMsg);
//         tMsg = Encoding.UTF8.GetString(tBytes);
// #endif
//
//         lock (mADBMsgs)
//         {
//             mADBMsgs.Add(tMsg);
//         }
//     }
//
//     void ParseADBMsg()
//     {
//         if (mADBProcess == null) return;
//
//         if (mADBProcess.HasExited)
//         {
//             StopADB();
//             return;
//         }
//
//         lock (mADBMsgs)
//         {
//             if (mADBMsgs.Count == 0) return;
//
//             var tMsg = string.Empty;
//             var tTrace = string.Empty;
//             var tLastIndex = 0;
//             var tChange = false;
//             for (int i = 0, imax = mADBMsgs.Count; i < imax; i++)
//             {
//                 var tResult = HandleMessage(mADBMsgs[i], (pMsgType, pInfo) =>
//                 {
//                     pInfo.id = mTotalInfos.Count;
//                     mTotalInfos.Add(pInfo);
//                     switch (pMsgType)
//                     {
//                         case MessageType.Info:
//                             mInfos_Info.Add(pInfo);
//                             break;
//                         case MessageType.Warning:
//                             mInfos_Warning.Add(pInfo);
//                             break;
//                         case MessageType.Error:
//                             mInfos_Error.Add(pInfo);
//                             break;
//                     }
//
//                 }, ref tMsg, ref tTrace);
//
//                 if (tResult)
//                 {
//                     tLastIndex = i;
//                 }
//
//                 tChange |= tResult;
//             }
//
//             if (tChange)
//             {
//                 UpdateInfos(false);
//                 mADBMsgs.RemoveRange(0, tLastIndex + 1);
//                 Repaint();
//             }
//         }
//     }
//
//     bool HandleMessage(string pFullMsg, Action<MessageType, Info> pCallBack, ref string pShortMsg, ref string pTrace)
//     {
//         if (string.IsNullOrEmpty(pFullMsg)) return false;
//
//         Match tMatch = null;
//         var tTag = string.Empty;
//         var tRegex = string.Empty;
//         foreach (var tag in tags)
//         {
//             if (pFullMsg.IndexOf(tag) == -1)
//             {
//                 continue;
//             }
//
//             var tIsMatch = false;
//             foreach (var item in sRegexs)
//             {
//                 tTag = tag;
//                 tRegex = item;
//                 tMatch = Regex.Match(pFullMsg, string.Format(item, tag));
//                 if (tMatch.Success)
//                 {
//                     tIsMatch = true;
//                     break;
//                 }
//             }
//
//             if (tIsMatch) break;
//         }
//
//         if (tMatch == null || !tMatch.Success) return false;
//
//         var tDesc = tMatch.Groups["desc"].Value;
//         var tType = tMatch.Groups["type"].Value;
//         var tMsgType = MessageType.None;
//         switch (tType)
//         {
//             //Info
//             case "I":
//             //Verbose
//             case "V":
//             //Debug
//             case "D":
//                 tMsgType = MessageType.Info;
//                 break;
//             //Warning
//             case "W":
//                 tMsgType = MessageType.Warning;
//                 break;
//             //Assert
//             case "A":
//             //Error
//             case "E":
//                 tMsgType = MessageType.Error;
//                 break;
//         }
//
//         if (tMsgType == MessageType.None || string.IsNullOrEmpty(tDesc)) return false;
//
//         if (string.IsNullOrEmpty(pShortMsg))
//         {
//             pShortMsg = tDesc;
//         }
//         else
//         {
//             pTrace += tMatch.Groups["spaceDesc"].Value + tDesc + "\n";
//         }
//
//         if ((tTag == cUnityTag && tDesc.IndexOf("(Filename:") != -1))
//         {
//             if (!string.IsNullOrEmpty(pShortMsg.Trim()) && !string.IsNullOrEmpty(pTrace.Trim()))
//             {
//                 var tDateMatch = Regex.Match(pFullMsg, cDateRegex);
//                 DateTime? tDate = null;
//
//                 if (tDateMatch.Success)
//                 {
//                     var tMonth = tDateMatch.Groups["month"].Value;
//                     var tDay = tDateMatch.Groups["day"].Value;
//                     var tHour = tDateMatch.Groups["hour"].Value;
//                     var tMinute = tDateMatch.Groups["minute"].Value;
//                     var tSecond = tDateMatch.Groups["second"].Value;
//                     var tMillisecond = tDateMatch.Groups["millisecond"].Value;
//                     try
//                     {
//                         tDate = new DateTime(DateTime.Now.Year, int.Parse(tMonth), int.Parse(tDay), int.Parse(tHour),
//                             int.Parse(tMinute), int.Parse(tSecond), int.Parse(tMillisecond));
//                     }
//                     catch
//                     {
//                     }
//                 }
//
//                 var tInfo = new Info()
//                 {
//                     msg = pShortMsg,
//                     trace = pTrace,
//                     type = tMsgType,
//                     tag = tTag,
//                     date = tDate,
//                 };
//                 pCallBack(tMsgType, tInfo);
//             }
//
//             pShortMsg = string.Empty;
//             pTrace = string.Empty;
//         }
//         else if (tTag != cUnityTag)
//         {
//             if (!string.IsNullOrEmpty(pShortMsg.Trim()))
//             {
//                 var tInfo = new Info() {msg = pShortMsg, trace = pTrace, type = tMsgType, tag = tTag};
//                 pCallBack(tMsgType, tInfo);
//             }
//
//             pShortMsg = string.Empty;
//             pTrace = string.Empty;
//         }
//
//         return true;
//     }
//
//     class Info
//     {
//         public string msg { set; private get; }
//         public string tag { set; private get; }
//         public MessageType type { set; get; }
//         public string trace { set; private get; }
//         public int index { private set; get; }
//         public int id { set; get; }
//         public DateTime? date { set; get; }
//
//         string mTrace;
//         string mMsg;
//         public const int cItemHeight = 30;
//
//
//         public string GetMsg()
//         {
//             if (string.IsNullOrEmpty(mMsg))
//             {
//                 var tDateStr = string.Empty;
//                 if (!date.HasValue)
//                 {
//                     //tDateStr = "00/00 00:00:00.000";
//                 }
//                 else
//                 {
//                     tDateStr = date.Value.ToString("MM/dd HH:mm:ss.fff");
//                 }
//
//                 mMsg = string.Format("<color=#CBCBCB>{0} {1} {2}</color>: {3}", id, tag, tDateStr, msg);
//             }
//
//             return mMsg;
//         }
//
//         public bool MatchMasg(string mSearchText)
//         {
//             if (string.IsNullOrEmpty(mSearchText)) return true;
//             return msg.IndexOf(mSearchText, StringComparison.OrdinalIgnoreCase) != -1;
//         }
//     }
// }
// #endif
