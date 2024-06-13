using DragonRunes.Database.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonRunes.Server.Repository
{
    public interface IPlayerRepository : IPlayerModelRepository
    {
        int CountPlayer();
    }
}
