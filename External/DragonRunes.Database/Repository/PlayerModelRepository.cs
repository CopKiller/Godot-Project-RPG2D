using DragonRunes.Database;
using DragonRunes.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DragonRunes.Database.Repository
{
    public abstract class PlayerModelRepository : IPlayerModelRepository
    {
        protected readonly DatabaseContext _db;

        public PlayerModelRepository(DatabaseContext db)
        {
            _db = db;
        }

        public async virtual Task<bool> AddPlayerAsync(PlayerModel player)
        {
            await _db.Players.AddAsync(player);
            return await _db.SaveChangesAsync() > 0 ? true : false;
        }

        public async virtual Task<bool> DeletePlayerAsync(PlayerModel player)
        {
            _db.Players.Remove(player);
            return await _db.SaveChangesAsync() > 0 ? true : false;
        }

        public async virtual Task<PlayerModel> GetPlayerAsync(string name)
        {
            name = name.ToUpper();
            return await _db.Players.FirstOrDefaultAsync(x => x.Name.ToUpper() == name);
        }

        public async virtual Task<IList<PlayerModel>> GetPlayersAsync()
        {
            return await _db.Players.ToListAsync();
        }

        public async virtual Task<bool> UpdatePlayerAsync(PlayerModel player)
        {
            _db.Players.Update(player);
            return await _db.SaveChangesAsync() > 0 ? true : false;
        }
    }
}
