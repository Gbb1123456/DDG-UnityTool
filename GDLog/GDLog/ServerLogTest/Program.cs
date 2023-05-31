using System;
using GDLog;

namespace ServerLogTest
{
    class Program
    {
        static void Main(string[] args)
        {
            GLog.InitSettings();
            GLog.Log($"START...", "ServerGLog");
            GLog.ColorLog(LogColorState.Red, "什么颜色", "Console");
            GLog.ColorLog(LogColorState.Green, "什么颜色", "Console");
            GLog.ColorLog(LogColorState.Blue, "什么颜色", "Console");
            GLog.ColorLog(LogColorState.Cyan, "什么颜色", "Console");
            GLog.ColorLog(LogColorState.Magenta, "什么颜色", "Console");
            GLog.ColorLog(LogColorState.Yellow, "什么颜色", "Console");
            GLog.ColorLog(LogColorState.Red, "什么颜色", "Console");


            Console.ReadKey();
        }
    }
}
