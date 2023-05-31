using System;
using System.Collections.Generic;
using System.Text;



namespace GDLog
{
    public enum LoggerType
    {
        Unity,
        Console,
    }

    public enum LogColorState
    {
        None,
        Red,
        Green,
        Blue,
        Yellow,
        Cyan,
        Magenta,
    }

    public class LogConfig
    {
        /// <summary>
        /// 是否开启日志
        /// </summary>
        public bool enableLog = true;
        public string logPrefix = "#";
        /// <summary>
        /// 是否显示时间
        /// </summary>
        public bool enableTime = true;
        public string logSeparate = ">>";
        /// <summary>
        /// 是否显示线程ID
        /// </summary>
        public bool enableThreadID = true;
        /// <summary>
        /// 是否显示堆栈信息
        /// </summary>
        public bool enableTrace = true;
        /// <summary>
        /// 是否保存日志信息
        /// </summary>
        public bool enableSave = false;
        /// <summary>
        /// 保存的日志信息是否在同一个路径(true：在同一个路径，本次日志信息会覆盖上次的日志信息)
        /// </summary>
        public bool enableCover = true;
        /// <summary>
        /// 文件路径，默认为当前程序的根文件夹下
        /// </summary>
        public string savePath = string.Format("{0}Logs\\", AppDomain.CurrentDomain.BaseDirectory);
        public string saveName = "ConsoleGLog.txt";
        public LoggerType loggerType = LoggerType.Console;
    }

    interface ILogger
    {
        void Log(string msg, LogColorState state = LogColorState.None);
        void Warn(string msg);
        void Error(string msg);
    }
}
