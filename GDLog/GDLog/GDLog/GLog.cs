using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

public static class ExtensionMethonds
{
    public static void Log(this object obj, string msg, params object[] args)
    {
        GDLog.GLog.Log(string.Format(msg, args));
    }
    public static void Log(this object obje, object obj)
    {
        GDLog.GLog.Log(obj);
    }
    /// <summary>
    /// 打印堆栈信息
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="args"></param>
    public static void Trace(this object obj, string msg, params object[] args)
    {
        GDLog.GLog.Trace(string.Format(msg, args));
    }
    public static void Trace(this object obje, object obj)
    {
        GDLog.GLog.Trace(obj);
    }
    public static void ColorLog(this object obj, GDLog.LogColorState state, string msg, params object[] args)
    {
        GDLog.GLog.ColorLog(state, string.Format(msg, args));
    }
    public static void ColorLog(this object obje, GDLog.LogColorState state, object obj, params object[] args)
    {
        GDLog.GLog.ColorLog(state, obj);
    }
    public static void Warn(this object obj, string msg, params object[] args)
    {
        GDLog.GLog.Warn(string.Format(msg, args));
    }
    public static void Warn(this object obje, object obj)
    {
        GDLog.GLog.Warn(obj);
    }
    public static void Error(this object obj, string msg, bool enableTrack = false, params object[] args)
    {
        GDLog.GLog.Error(string.Format(msg, args));
    }
    public static void Error(this object obje, object obj, bool enableTrack = false)
    {
        GDLog.GLog.Error(obj);
    }
}

namespace GDLog
{
    public static class GLog
    {
        class UnityLogger : ILogger
        {
            Type type = Type.GetType("UnityEngine.Debug,UnityEngine");
            public void Log(string msg, LogColorState state = LogColorState.None)
            {
                if (state != LogColorState.None)
                {
                    msg = ColorUnityLog(msg, state);
                }
                else
                {
                    msg = ColorUnityLog(msg, LogColorState.Green);
                }
                type.GetMethod("Log", new Type[] { typeof(object) }).Invoke(null, new object[] { msg });
            }
            public void Warn(string msg)
            {
                //type.GetMethod("LogWarning", new Type[] { typeof(object) }).Invoke(null, new object[] { msg });
                type.GetMethod("Log", new Type[] { typeof(object) }).Invoke(null, new object[] { ColorUnityLog(msg, LogColorState.Yellow) });
            }
            public void Error(string msg)
            {
                //type.GetMethod("LogError", new Type[] { typeof(object) }).Invoke(null, new object[] { msg });
                type.GetMethod("Log", new Type[] { typeof(object) }).Invoke(null, new object[] { ColorUnityLog(msg, LogColorState.Red) });
            }

            private string ColorUnityLog(string msg, LogColorState state)
            {
                switch (state)
                {
                    case LogColorState.Red:
                        msg = $"<color=#FF0000>{msg}</color>";
                        break;
                    case LogColorState.Green:
                        msg = $"<color=#00FF00>{msg}</color>";
                        break;
                    case LogColorState.Blue:
                        msg = $"<color=#0000FF>{msg}</color>";
                        break;
                    case LogColorState.Yellow:
                        msg = $"<color=#FFFF00>{msg}</color>";
                        break;
                    case LogColorState.Cyan:
                        msg = $"<color=#00FFFF>{msg}</color>";
                        break;
                    case LogColorState.Magenta:
                        msg = $"<color=#FF00FF>{msg}</color>";
                        break;
                    case LogColorState.None:
                    default:
                        break;
                }
                return msg;
            }
        }

        class ConsoleLogger : ILogger
        {
            public void Log(string msg, LogColorState state = LogColorState.None)
            {
                WriteConsoleLog(msg, state);
            }
            public void Warn(string msg)
            {
                WriteConsoleLog(msg, LogColorState.Yellow);
            }
            public void Error(string msg)
            {
                WriteConsoleLog(msg, LogColorState.Red);
            }
            private void WriteConsoleLog(string msg, LogColorState colorState)
            {
                switch (colorState)
                {

                    case LogColorState.Red:
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine(msg);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        break;
                    case LogColorState.Green:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(msg);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        break;
                    case LogColorState.Blue:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine(msg);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        break;
                    case LogColorState.Yellow:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine(msg);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        break;
                    case LogColorState.Cyan:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine(msg);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        break;
                    case LogColorState.Magenta:
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine(msg);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        break;
                    case LogColorState.None:
                    default:
                        Console.WriteLine(msg);
                        break;
                }
            }
        }

        public static LogConfig cfg;
        private static ILogger logger;
        private static StreamWriter LogFileWriter = null;
        public static void InitSettings(LogConfig cfg = null)
        {
            if (cfg == null)
            {
                cfg = new LogConfig();
            }
            GLog.cfg = cfg;

            if (cfg.loggerType == LoggerType.Console)
            {
                logger = new ConsoleLogger();
            }
            else
            {
                logger = new UnityLogger();
            }

            if (!cfg.enableSave)
            {
                return;
            }
            if (cfg.enableCover)
            {
                string path = cfg.savePath + cfg.saveName;
                try
                {
                    if (Directory.Exists(cfg.savePath))
                    {
                        if (File.Exists(path))
                        {
                            File.Delete(path);
                        }
                    }
                    else
                    {
                        Directory.CreateDirectory(cfg.savePath);
                    }

                    LogFileWriter = File.AppendText(path);
                    LogFileWriter.AutoFlush = true;
                }
                catch
                {
                    LogFileWriter = null;
                }
            }
            else
            {
                string prefix = DateTime.Now.ToString("yyyy-MM-dd: HH:mm:ss");
                string path = cfg.savePath + prefix + cfg.saveName;
                try
                {
                    if (!Directory.Exists(cfg.savePath))
                    {
                        Directory.CreateDirectory(cfg.savePath);
                    }
                    LogFileWriter = File.AppendText(path);
                    LogFileWriter.AutoFlush = true;
                }
                catch
                {
                    LogFileWriter = null;
                }
            }
        }

        /// <summary>
        /// 常规日志
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public static void Log(string msg, params object[] args)
        {
            if (!cfg.enableLog)
            {
                return;
            }
            msg = DecorateLog(string.Format(msg, args));
            logger.Log(msg);
            if (cfg.enableSave)
            {
                WriteToFile($"[L]:{msg}");
            }
        }
        public static void Log(object obj)
        {
            if (!cfg.enableLog)
            {
                return;
            }
            string msg = DecorateLog(string.Format(obj.ToString()));
            logger.Log(msg);
            if (cfg.enableSave)
            {
                WriteToFile($"[L]:{msg}");
            }
        }
        /// <summary>
        /// 打印堆栈信息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public static void Trace(string msg, params object[] args)
        {
            if (!cfg.enableLog)
            {
                return;
            }
            msg = DecorateLog(string.Format(msg, args), true);
            logger.Log(msg, LogColorState.Magenta);
            if (cfg.enableSave)
            {
                WriteToFile($"[T]:{msg}");
            }
        }
        public static void Trace(object obj)
        {
            if (!cfg.enableLog)
            {
                return;
            }
            string msg = DecorateLog(string.Format(obj.ToString()), true);
            logger.Log(msg, LogColorState.Magenta);
            if (cfg.enableSave)
            {
                WriteToFile($"[T]:{msg}");
            }
        }
        public static void ColorLog(LogColorState state, string msg, params object[] args)
        {
            if (!cfg.enableLog)
            {
                return;
            }
            msg = DecorateLog(string.Format(msg, args));
            logger.Log(msg, state);
            if (cfg.enableSave)
            {
                WriteToFile($"[L]:{msg}");
            }
        }
        public static void ColorLog(LogColorState state, object obj, params object[] args)
        {
            if (!cfg.enableLog)
            {
                return;
            }
            string msg = DecorateLog(string.Format(obj.ToString()));
            logger.Log(msg, state);
            if (cfg.enableSave)
            {
                WriteToFile($"[L]:{msg}");
            }
        }
        public static void Warn(string msg, params object[] args)
        {
            if (!cfg.enableLog)
            {
                return;
            }
            msg = DecorateLog(string.Format(msg, args));
            logger.Warn(msg);
            if (cfg.enableSave)
            {
                WriteToFile($"[W]:{msg}");
            }
        }
        public static void Warn(object obj)
        {
            if (!cfg.enableLog)
            {
                return;
            }
            string msg = DecorateLog(string.Format(obj.ToString()));
            logger.Warn(msg);
            if (cfg.enableSave)
            {
                WriteToFile($"[W]:{msg}");
            }
        }
        public static void Error(string msg, bool enableTrack = false, params object[] args)
        {
            if (!cfg.enableLog)
            {
                return;
            }
            msg = DecorateLog(string.Format(msg, args), enableTrack);
            logger.Error(msg);
            if (cfg.enableSave)
            {
                WriteToFile($"[E]:{msg}");
            }
        }
        public static void Error(object obj, bool enableTrack = false)
        {
            if (!cfg.enableLog)
            {
                return;
            }
            string msg = DecorateLog(string.Format(obj.ToString()), enableTrack);
            logger.Error(msg);
            if (cfg.enableSave)
            {
                WriteToFile($"[E]:{msg}");
            }
        }

        #region Tool

        /// <summary>
        /// 获取时间信息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="isTrace"></param>
        /// <returns></returns>
        private static string DecorateLog(string msg, bool isTrace = false)
        {
            StringBuilder sb = new StringBuilder(cfg.logPrefix, 500);
            if (cfg.enableTime)
            {
                sb.AppendFormat($"{DateTime.Now.ToString("HH:mm:ss---fff")}");
            }
            if (cfg.enableThreadID)
            {
                sb.AppendFormat($"{GetThreadID()}");
            }
            sb.AppendFormat($"{cfg.logSeparate} {msg}");
            if (isTrace)
            {
                sb.AppendFormat($"\nStackTrace:{GetLogTrace()}");
            }
            return sb.ToString();
        }

        /// <summary>
        /// 获取线程ID 
        /// </summary>
        /// <returns></returns>
        private static string GetThreadID()
        {
            return $"ThreadID:{Thread.CurrentThread.ManagedThreadId}";
        }

        /// <summary>
        /// 获取堆栈的信息
        /// </summary>
        /// <returns></returns>
        private static string GetLogTrace()
        {
            StackTrace st = new StackTrace(3, true);

            string traceInfo = "";
            for (int i = 0; i < st.FrameCount; i++)
            {
                StackFrame sf = st.GetFrame(i);
                traceInfo += $"\n{sf.GetFileName()}::{sf.GetMethod()} line:{sf.GetFileLineNumber()}";
            }

            return traceInfo;
        }

        private static void WriteToFile(string msg)
        {
            if (cfg.enableSave && LogFileWriter != null)
            {
                try
                {
                    LogFileWriter.WriteLine(msg);
                }
                catch
                {
                    LogFileWriter = null;
                }
            }
        }
        #endregion
    }
}
