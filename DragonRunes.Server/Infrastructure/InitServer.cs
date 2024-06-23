using LiteNetLib;
using DragonRunes.Logger;
using DragonRunes.Server.Logger;
using DragonRunes.Network;
using DragonRunes.Network;
using DragonRunes.Server.Network;
using DragonRunes.Server.Repository;
using DragonRunes.Database.Repository;
using DragonRunes.Models;
using Microsoft.Extensions.DependencyInjection;
using DragonRunes.Network.Service;

namespace DragonRunes.Server.Infrastructure
{
    public class InitServer
    {
        private readonly IServiceProvider _serviceProvider;

        private INetworkManager _networkManager;

        public static DatabaseManager _databaseManager;

        private bool _isRunning = false;

        internal InitServer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Start()
        {
            _isRunning = true;

            StartLogger();

            StartDatabase();

            StartNetwork();

            Logg.Logger.Log("Server Started...");

            Logg.Logger.Log($"Quantidade de contas: {CountDatabaseAccounts().ToString()}");
        }

        private void StartLogger()
        {
            Logg.Logger = new LogManager();
            Logg.Logger.Log("Logs Initialized...");
        }

        private void StartDatabase()
        {
            var accountRepo = _serviceProvider.GetRequiredService<AccountRepository>();
            var playerRepo = _serviceProvider.GetRequiredService<PlayerRepository>();
            _databaseManager = new DatabaseManager(accountRepo, playerRepo);
            Logg.Logger.Log("Database Initialized...");
        }

        private void StartNetwork()
        {
            _networkManager =  _serviceProvider.GetRequiredService<INetworkManager>();
            _networkManager.Register(_serviceProvider.GetRequiredService<INetworkService>());
            _networkManager.Start();
            Logg.Logger.Log("Network Started...");
        }

        private int CountDatabaseAccounts()
        {
            return _databaseManager.AccountRepository.CountAccounts();
        }
    }
}
