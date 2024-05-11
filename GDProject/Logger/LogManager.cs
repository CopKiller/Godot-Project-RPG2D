using Godot;

namespace GdProject.Logger
{
    internal partial class LogManager : Node, ILogger
    {
        public override void _Ready()
        {
            ExternalLogger.Logger = this;

            Log("Logger initialized");

            // Remove o gerenciador de nós mas mantém seus métodos acessíveis de qualquer parte do código
            this.QueueFree();
        }

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
