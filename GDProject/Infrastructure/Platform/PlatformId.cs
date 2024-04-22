using Godot;

namespace GdProject.Infrastructure.Platform
{
    internal class PlatformId
    {
        public enum Platform
        {
            Windows,
            Android,
            Unknown
        }

        public static Platform GetPlatformId()
        {
            if (OS.GetName() == "Windows")
            {
                return Platform.Windows;
            }
            else if (OS.GetName() == "Android")
            {
                return Platform.Android;
            }
            else
            {
                return Platform.Unknown;
            }
        }
    }
}
