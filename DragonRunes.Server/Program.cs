using DragonRunes.Server.Commands;
using DragonRunes.Server.Infrastructure;

namespace DragonRunes.Server;
public class Program
{
    public static void Main()
    {

        var services = new Services();
        var serviceProvider = services.Init();

        var server = new InitServer(serviceProvider);
        server.Start();

        ReadConsole.Read();
    }
}