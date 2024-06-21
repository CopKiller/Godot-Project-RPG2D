using DragonRunes.Database;
using DragonRunes.Database.Repository;
using DragonRunes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonRunes.Server.Repository
{
    public class PlayerRepository : PlayerModelRepository, IPlayerRepository
    {
        public PlayerRepository(DatabaseContext db) : base(db) { }

        // Métodos de implementação e tratamento, específicos do servidor
        public int CountPlayer()
        {
            return _db.Players.Count();
        }

        public async Task<bool> RegisterPlayerAsync(PlayerModel player)
        {
            if (await CheckPlayerExistAsync(player.Name))
            {
                return false;
            }

            var result = await base.AddPlayerAsync(player);
            return result;
        }

        public async Task<bool> DeletePlayerAsync(string name)
        {
            var result = await base.GetPlayerAsync(name);

            return await base.DeletePlayerAsync(result);
        }

        public async Task<IPlayerModel> GetPlayerByNameAsync(string name)
        {
            var result = await base.GetPlayerAsync(name);

            return result;
        }

        private async Task<bool> CheckPlayerExistAsync(string name)
        {

            var result = await base.GetPlayerAsync(name);

            return result != null;

        }
    }
}
