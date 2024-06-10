

namespace DragonRunes.Logger
{
    public class Logg
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
    }
}
