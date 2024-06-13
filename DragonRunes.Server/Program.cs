using DragonRunes.Database;
using DragonRunes.Database.Repository;
using DragonRunes.Server.Commands;
using DragonRunes.Server.Infrastructure;
using DragonRunes.Server.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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