using DragonRunes.Database;
using DragonRunes.Database.Repository;
using DragonRunes.Logger;
using DragonRunes.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonRunes.Server.Repository
{
    public class DatabaseManager
    {
        public readonly AccountRepository AccountRepository;

        public readonly PlayerRepository PlayerRepository;


        public DatabaseManager(AccountRepository accountRepository,
                                PlayerRepository playerRepository)
        {
            AccountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
            PlayerRepository = playerRepository ?? throw new ArgumentNullException(nameof(playerRepository));

        }
    }
}
