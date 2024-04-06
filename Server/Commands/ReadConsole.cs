using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
