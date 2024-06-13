using DragonRunes.Database.Repository;
using DragonRunes.Database;
using DragonRunes.Logger;
using DragonRunes.Server.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using DragonRunes.Server.Network;
using DragonRunes.Network;
using DragonRunes.Network.Service;

namespace DragonRunes.Server.Infrastructure
{
    public class Services
    {
        public IServiceProvider Init()
        {
            Console.WriteLine("Initializing Services...");

            var serviceCollection = new ServiceCollection();

            // Database
            ConfigureDatabaseService(serviceCollection);
            //Network
            ConfigureNetworkService(serviceCollection);

            // ...

            var provider = serviceCollection.BuildServiceProvider();

            return provider;
        }

        private void ConfigureDatabaseService(IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IPlayerRepository, PlayerRepository>();
        }

        private void ConfigureNetworkService(IServiceCollection services)
        {
            services.AddScoped<INetworkManager, NetworkManager>();
            services.AddScoped<IService, ServerNetworkService>();
        }

    }
}
