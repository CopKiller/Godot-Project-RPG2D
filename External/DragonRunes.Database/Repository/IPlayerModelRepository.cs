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
        Task<bool> AddPlayerAsync(PlayerModel player);
        Task<bool> UpdatePlayerAsync(PlayerModel player);
        Task<bool> DeletePlayerAsync(PlayerModel player);
    }
}
