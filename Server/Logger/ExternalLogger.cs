
using LiteNetLib;

namespace Server.Logger
{
    public class ExternalLogger : INetLogger
    {
        public static ILogger Logger = null;

        public static string GetTimestamp()
        {
            return DateTime.Now.ToString("dd.MM.yyyy-HH:mm:ss");
        }

        public static void Print(string message)
        {
            Logger.Log($"{GetTimestamp()}: {message}");
        }

        public static void PrintInfo(string message)
        {
            Logger.Log($"{GetTimestamp()}: {message}");
        }

        public static void PrintInfo(long id, string message)
        {
            Logger.Log($"{GetTimestamp()} - <{id}>: {message}");
        }

        public static void Print(long id, string message)
        {
            Logger.Log($"{GetTimestamp()} - <{id}>: {message}");
        }

        public static void PrintErr(string message)
        {
            Logger.LogError($"{GetTimestamp()}: {message}");
        }

        public static void PrintErr(long id, string message)
        {
            Logger.LogError($"{GetTimestamp()} - <{id}>: {message}");
        }

        /// <summary>
        /// Log debug message
        /// LiteNetLib will call this method with LogLevel.Trace
        /// </summary>
        public void WriteNet(NetLogLevel level, string str, params object[] args)
        {
            string logMessage = string.Format("[LiteNetLib {0}] {1}", level, string.Format(str, args));
            switch (level)
            {
                case NetLogLevel.Trace:
                    Logger.Log(logMessage);
                    break;
                case NetLogLevel.Info:
                    Logger.LogInfo(logMessage);
                    break;
                case NetLogLevel.Warning:
                    Logger.LogWarning(logMessage);
                    break;
                case NetLogLevel.Error:
                    Logger.LogError(logMessage);
                    break;
            }
        }
    }
}
