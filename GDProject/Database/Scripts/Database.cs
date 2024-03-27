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
using GdProject.Shared.Scripts;
using GdProject.Shared.Scripts.NodeManager;

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

    // Obter o diretório raiz, para criar e acessar o banco de dados
    //public string GetDatabaseDirectory()
    //{
    //    var path = OS.GetUserDataDir();
    //    var databasePath = System.IO.Path.Combine(path, "database.db");

    //    return databasePath;
    //}

    //// Método para acionar o GetDatabaseDirectory e retornar para um projeto externo
    //public string GetDatabaseDirectoryExternal()
    //{
    //    return GetDatabaseDirectory();
    //}
}
