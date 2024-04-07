using EntityFramework.Entities.Player;
using EntityFramework.Repositories.Interface;
using EntityFramework.Repositories.Player;
using Microsoft.Extensions.DependencyInjection;
using Server.Logger;

namespace Server.Database.Repository.Player
{
    internal class PlayerRepo
    {
        private readonly IServiceProvider _serviceProvider;

        public IRepository<T> GetRepository<T>() where T : class
        {
            return _serviceProvider.GetRequiredService<IRepository<T>>();
        }

        public PlayerRepo(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<bool> RegisterPlayerAsync(string charName, int accountId)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var playerRepository = GetRepository<PlayerEntity>();
                var repository = (PlayerRepository)playerRepository;

                var player = new PlayerEntity();
                player.Name = charName;
                player.Id = accountId;

                var account = await repository.AddNewPlayerAsync(charName, accountId);

                ExternalLogger.Print(account.Message);

                return account.Success;
            }
        }
    }
}
