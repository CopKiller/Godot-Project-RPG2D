using Godot;
using System;

using LiteNetLib;

namespace GdProject.Shared.Scripts.Global
{
    public class GDPrint : INetLogger
    {
        public static string GetTimestamp()
        {
            return DateTime.Now.ToString("dd.MM.yyyy-HH:mm:ss");
        }

        public static void Print(string message)
        {
            GD.Print($"{GetTimestamp()}: {message}");
        }

        public static void Print(long id, string message)
        {
            GD.Print($"{GetTimestamp()} - <{id}>: {message}");
        }

        public static void PrintErr(string message)
        {
            GD.PrintErr($"{GetTimestamp()}: {message}");
        }

        public static void PrintErr(long id, string message)
        {
            GD.PrintErr($"{GetTimestamp()} - <{id}>: {message}");
        }

        public void WriteNet(NetLogLevel level, string str, params object[] args)
        {
            string logMessage = string.Format("[LiteNetLib {0}] {1}", level, string.Format(str, args));
            switch (level)
            {
                case NetLogLevel.Trace:
                    Print(logMessage);
                    break;
                case NetLogLevel.Info:
                    Print(logMessage);
                    break;
                case NetLogLevel.Warning:
                    Print(logMessage);
                    break;
                case NetLogLevel.Error:
                    PrintErr(logMessage);
                    break;

                    //            public enum NetLogLevel
                    //{
                    //    Warning,
                    //    Error,
                    //    Trace,
                    //    Info
                    //}
            }
        }
    }
}
