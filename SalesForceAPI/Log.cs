using System;
using Newtonsoft.Json;
using Serilog;

namespace SalesForceAPI
{

    public static class Log
    {
        public static void LogMsg(string logMessage, object obj)
        {
            SerilogLog.LogInfo(logMessage, obj);
        }

        public static void LogMsg(string logMessage)
        {
            SerilogLog.LogInfo(logMessage);
        }
    }

    public static class SerilogLog
    {
        public static void LogInfo(string logMessage)
        {
            Serilog.Log.Information(logMessage);
        }

        public static void LogInfo(string logMessage, object obj)
        {
            Serilog.Log.Information(logMessage, obj);
        }

        public static void LogDebug(string logMessage)
        {
            Serilog.Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.ColoredConsole()
                .CreateLogger();

            Serilog.Log.Debug(logMessage);
        }

        public static void LogError(string logMessage)
        {
            Serilog.Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.ColoredConsole()
                .CreateLogger();

            Serilog.Log.Error(logMessage);
        }
    }
}