using DragonRunes.Database.Repository;
using DragonRunes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonRunes.Server.Repository
{
    public interface IPlayerRepository
    {
        int CountPlayer();
        Task<IPlayerModel> GetPlayerByNameAsync(string name);
        Task<bool> RegisterPlayerAsync(PlayerModel player);
        Task<bool> DeletePlayerAsync(string name);
    }
}
