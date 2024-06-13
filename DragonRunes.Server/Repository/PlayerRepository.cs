using DragonRunes.Database;
using DragonRunes.Database.Repository;
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

    }
}
