using Godot;
using EntityFramework;
using EntityFramework.Configuration;
using EntityFramework.Entities.Account;
using EntityFramework.Entities.Player;
using EntityFramework.Repositories.Account;
using EntityFramework.Repositories.Interface;
using EntityFramework.Repositories.Player;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using GdProject.Shared.Scripts.Global;
using System.Threading.Tasks;
using EntityFramework.Entities.Interface;

public partial class Database : Node
{
    // Provedor de serviços para injeção de dependência
    public IServiceProvider _serviceProvider { get; set; }

    public override void _Ready()
    {
        var databaseStart = GetTree().Root.GetNode<RPG2D>("RPG2D").DatabaseStart;

        GD.Print("Database Start: " + databaseStart);
        if (!databaseStart)
        {
            QueueFree();
            return;
        }

        // Adiciona este nó e os filhos ao gerenciador de nós
        NodeManager.AddToNodeManager(this);

        InitDatabase();
    }

    public void InitDatabase()
    {
        var serviceCollection = new ServiceCollection();

        // Configuração do DbContext e outros serviços
        var connectionString = DatabaseDirectory.GetDatabaseDirectory();
        serviceCollection.AddDbContext<MeuDbContext>(options => options.UseSqlite(@connectionString));

        // Configuração dos repositórios
        serviceCollection.AddScoped<IRepository<AccountEntity>, AccountRepository>();
        serviceCollection.AddScoped<IRepository<PlayerEntity>, PlayerRepository>();

        var provider = serviceCollection.BuildServiceProvider();

        GDPrint.Print($"Configurada com sucesso! {connectionString}");

        _serviceProvider = provider;

        //Criação do banco de dados, caso não existir
        //using (var scope = provider.CreateScope())
        //{
        //    var scp = scope.ServiceProvider.GetRequiredService<MeuDbContext>();
        //    scp.Database.Migrate();
        //}
    }

    public IRepository<T> GetRepository<T>() where T : class
    {
        return _serviceProvider.GetRequiredService<IRepository<T>>();
    }

    public async Task<IAccountEntity> AuthenticateAsync(string username, string password)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var accountRepository = GetRepository<AccountEntity>();
            var repository = (AccountRepository)accountRepository;

            var account = await repository.AuthenticateAccountAsync(username, password);

            GDPrint.Print(account.Message);

            if (account.EntityType == null)
            {
                return null;
            }

            if (account.Success)
            {
                return account.EntityType;
            }

            return null;
        }
    }

    public async void RegisterAccountAsync(string username, string password)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var accountRepository = GetRepository<AccountEntity>();
            var repository = (AccountRepository)accountRepository;

            var playerAccount = new AccountEntity();
            playerAccount.Login = username;
            playerAccount.Password = password;

            var account = await repository.AddPlayerAccountAsync(playerAccount);

            GDPrint.Print(account.Message);
        }
    }

    public async Task<bool> RegisterPlayerAsync(string charName, int accountId)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var playerRepository = GetRepository<PlayerEntity>();
            var repository = (PlayerRepository)playerRepository;

            var player = new PlayerEntity();
            player.Name = charName;
            player.Id = accountId;

            var account = await repository.AddNewPlayerAsync(charName, accountId);

            GDPrint.Print(account.Message);

            return account.Success;
        }
    }
}
