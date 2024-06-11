using DragonRunes.Models;
using Microsoft.EntityFrameworkCore;

namespace DragonRunes.Database
{
    public class DatabaseContext: DbContext
    {
        public DbSet<AccountModel> Accounts { get; set; }
        public DbSet<PlayerModel> Players { get; set; }


        public DatabaseContext()
        {
            Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DragonRunesDatabase.db");

            optionsBuilder.UseSqlite($"Filename={databasePath}");
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }
    }
}
