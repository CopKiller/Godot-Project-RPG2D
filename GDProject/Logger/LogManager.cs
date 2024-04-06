using Godot;

namespace GdProject.Logger
{
    internal class LogManager : ILogger
    {
        public void LogError(string message)
        {
            GD.Print("[ERROR] " + message);
        }

        public void LogInfo(string message)
        {
            GD.Print("[INFO] " + message);
        }

        public void LogWarning(string message)
        {
            GD.Print("[WARNING] " + message);
        }

        public void Log(string message)
        {
            GD.Print(message);
        }
    }
}
