using EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Server.Database.Repository.Account;
using Server.Database.Repository.Player;

namespace Server.Database
{
    internal class DatabaseManager
    {
        private readonly IServiceProvider _serviceProvider;

        public AccountRepo AccountRepo { get; private set; }
        public PlayerRepo PlayerRepo { get; private set; }

        public DatabaseManager() { }

        public DatabaseManager(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Initialize()
        {
            AccountRepo = new AccountRepo(_serviceProvider);
            PlayerRepo = new PlayerRepo(_serviceProvider);
        }

        public void CreateDatabase()
        {
            //Criação do banco de dados, caso não existir
            using (var scope = _serviceProvider.CreateScope())
            {
                var scp = scope.ServiceProvider.GetRequiredService<MeuDbContext>();
                scp.Database.Migrate();
            }
        }
    }
}
