﻿using EntityFramework.Entities.Player;
using EntityFramework.Repositories.Interface;
using EntityFramework.Repositories.Player;
using EntityFramework.Repositories.ValidadeData;
using Microsoft.Extensions.DependencyInjection;
using Server.Logger;
using Server.Model;

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

        public async Task<OperationResult> RegisterPlayerAsync(string charName, int accountId)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var playerRepository = GetRepository<PlayerEntity>();
                var repository = (PlayerRepository)playerRepository;

                var player = new PlayerEntity();
                player.Name = charName;
                player.Id = accountId;

                var account = await repository.AddNewPlayerAsync(charName, accountId);

                return account;
            }
        }

        public async void SavePlayerAsync(PlayerDataModel playerData, PlayerPhysicModel playerPhysic)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var playerRepository = GetRepository<PlayerEntity>();
                var repository = (PlayerRepository)playerRepository;

                var player = new PlayerEntity();

                player.Id = playerData.playerId;

                player.Position.X = playerPhysic.Position.X;
                player.Position.Y = playerPhysic.Position.Y;
                player.Direction.X = playerPhysic.Direction.X;
                player.Direction.Y = playerPhysic.Direction.Y;

                var opResult = await repository.SavePlayerAsync(player);

                ExternalLogger.Print($"{opResult.Message}");
            }
        }
    }
}
