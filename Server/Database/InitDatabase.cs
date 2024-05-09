using EntityFramework;
using EntityFramework.Configuration;
using EntityFramework.Entities.Account;
using EntityFramework.Entities.Player;
using EntityFramework.Repositories.Account;
using EntityFramework.Repositories.Interface;
using EntityFramework.Repositories.Player;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Server.Logger;

namespace Server.Database
{

    internal class InitDatabase
    {
        public DatabaseManager _databaseManager;
        public void Start()
        {
            var serviceCollection = new ServiceCollection();

            // Configuração do DbContext e outros serviços
            var connectionString = DatabaseDirectory.GetDatabaseDirectory();
            serviceCollection.AddDbContext<MeuDbContext>(options => options.UseSqlite(@connectionString));

            // Configuração dos repositórios
            serviceCollection.AddScoped<IRepository<AccountEntity>, AccountRepository>();
            serviceCollection.AddScoped<IRepository<PlayerEntity>, PlayerRepository>();

            var provider = serviceCollection.BuildServiceProvider();

            ExternalLogger.Print($"Configurada com sucesso! {connectionString}");

            _databaseManager = new DatabaseManager(provider);
            _databaseManager.Initialize();

            // Descomente a linha abaixo para criar um novo banco de dados
             _databaseManager.CreateDatabase();
        }
    }
}
