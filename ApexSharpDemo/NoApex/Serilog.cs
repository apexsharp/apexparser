using Serilog;

namespace ApexSharpDemo.NoApex
{
    public static class Serilog
    {
        public static void LogInfo(string logMessage)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();

            Log.Information(logMessage);
        }

        public static void LogInfo(string logMessage, object obj)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();

            Log.Information(logMessage, obj);
        }

        public static void LogDebug(string logMessage)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();

            Log.Debug(logMessage);
        }

        public static void LogError(string logMessage)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();

            Log.Error(logMessage);
        }
    }
}