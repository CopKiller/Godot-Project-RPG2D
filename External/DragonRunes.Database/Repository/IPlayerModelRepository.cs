using DragonRunes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonRunes.Database.Repository
{
    public interface IPlayerModelRepository
    {
        Task<IList<PlayerModel>> GetPlayersAsync();
        Task<PlayerModel> GetPlayerAsync(string login);
        Task AddPlayerAsync(PlayerModel player);
        Task UpdatePlayerAsync(PlayerModel player);
        Task DeletePlayerAsync(PlayerModel player);
    }
}
