
using EntityFramework.Configuration;
using EntityFramework.Entities.Account;
using EntityFramework.Entities.Player;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace EntityFramework;

public class MeuDbContext : DbContext
{
    public DbSet<AccountEntity> AccountEntities { get; set; }
    public DbSet<PlayerEntity> PlayerEntities { get; set; }

    public MeuDbContext()
    {
        Batteries.Init();
    }

    public MeuDbContext(DbContextOptions<MeuDbContext> options) : base(options)
    {
        Batteries.Init();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Configura��o da exclus�o em cascata para todas as rela��es
        foreach (var relationship in modelBuilder.Model.GetEntityTypes()
            .SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Cascade;
        }

        // Adicione outras configura��es de modelo, se necess�rio...
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite(@DatabaseDirectory.GetDatabaseDirectory());
        }

        base.OnConfiguring(optionsBuilder);
    }
}