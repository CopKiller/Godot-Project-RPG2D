

using Server.Commands;
using Server.Infrastructure;

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