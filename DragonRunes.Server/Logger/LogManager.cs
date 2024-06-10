

using DragonRunes.Logger;

namespace DragonRunes.Server.Logger
{
    internal class LogManager : ILogger
    {
        public void LogError(string message)
        {
            Console.WriteLine("[ERROR] " + message);
        }

        public void LogInfo(string message)
        {
            Console.WriteLine("[INFO] " + message);
        }

        public void LogWarning(string message)
        {
            Console.WriteLine("[WARNING] " + message);
        }

        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
