
using GdProject.Logger;
using GdProject.Network;
using Godot;

namespace GdProject.Client;
public partial class Client : Node
{
    public override void _Ready()
    {
        ExternalLogger.Logger = new LogManager();

        // Adiciona este nó e os filhos ao gerenciador de nós
        NodeManager.AddToNodeManager(this);

        InitMenu();

        InitConnection();
    }

    public void InitMenu()
    {
        NodeManager.GetNode<Game>(nameof(Game)).Hide();
        NodeManager.GetNode<MainMenu>(nameof(MainMenu)).Show();
    }

    public void InitGame()
    {
        NodeManager.GetNode<MainMenu>(nameof(MainMenu)).Hide();
        NodeManager.GetNode<Game>(nameof(Game)).Show();
    }

    public void InitConnection()
    {
        var clientNetworkService = new ClientNetworkService();
        clientNetworkService.Name = "ClientNetworkService";
        AddChild(clientNetworkService);
        NodeManager.AddNode(clientNetworkService);
        clientNetworkService.Register();
        clientNetworkService.Connect();
    }
}
