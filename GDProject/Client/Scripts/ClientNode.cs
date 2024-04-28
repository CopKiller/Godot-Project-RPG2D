
using GdProject.Infrastructure;
using GdProject.Logger;
using Godot;

namespace GdProject.Client;
public partial class ClientNode : Node
{
    internal InitClient _initClient;

    public override void _Ready()
    {
        ExternalLogger.Logger = new LogManager();

        // Adiciona este nó e os filhos ao gerenciador de nós
        NodeManager.AddToNodeManager(this);

        InitClient();

        InitMenu();
    }

    private void InitClient()
    {
        _initClient = new InitClient();
        _initClient.Start();
    }

    public void InitMenu()
    {
        NodeManager.GetNode<Game>(nameof(Game)).Hide();
        NodeManager.GetNode<MainMenu>(nameof(MainMenu)).InitMenu();
    }

    public void InitGame()
    {
        NodeManager.GetNode<MainMenu>(nameof(MainMenu)).HideMenu();
        NodeManager.GetNode<Game>(nameof(Game)).Show();
    }
}
