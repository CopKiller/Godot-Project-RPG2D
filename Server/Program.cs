

using Server.Infrastructure;
using Server.Commands;

namespace Server;
public class Program
{
    public static void Main()
    {
        
        var server = new InitServer();
        server.Start();

        ReadConsole.Read();
    }
}