
using GdProject.Infrastructure;
using GdProject.Logger;
using Godot;

namespace GdProject.Client;
public partial class ClientNode : Node
{
    public override void _Ready()
    {

        InitClient();

        InitMenu();
    }

    public void InitClient()
    {
        NodeManager.GetNode<ClientManager>(nameof(ClientManager)).Start();
    }

    public void InitMenu()
    {
        NodeManager.GetNode<Game>(nameof(Game)).Hide();
        NodeManager.GetNode<MainMenu>(nameof(MainMenu)).InitMenu();

        NodeManager.GetNode<Camera2D>(nameof(Camera2D)).Zoom = new Vector2(1, 1);
    }

    public void InitGame()
    {
        NodeManager.GetNode<MainMenu>(nameof(MainMenu)).HideMenu();
        NodeManager.GetNode<Game>(nameof(Game)).InitGame();

        NodeManager.GetNode<Camera2D>(nameof(Camera2D)).Zoom = new Vector2(2, 2);
    }
}
