

using DragonRunes.Server.Infrastructure;
using DragonRunes.Server.Commands;

namespace DragonRunes.Server;
public class Program
{
    public static void Main()
    {

        var server = new InitServer();
        server.Start();

        ReadConsole.Read();
    }
}