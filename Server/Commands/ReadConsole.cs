namespace Server.Commands
{
    internal class ReadConsole
    {
        public static void Read()
        {
            while (true)
            {
                string command = Console.ReadLine();
                if (command == "exit")
                {
                    break;
                }
            }
        }
    }
}
