using Godot;

public partial class GameManager : Control
{
    [Export] public bool ServerBuild = false;
    [Export] public bool ClientBuild = false;

    public override void _Ready()
    {
        if (ServerBuild)
        {
            GameServer server = new GameServer();
            AddChild(server);
        }
        else
        {
            GameClient client = new GameClient();
            AddChild(client);
        }
    }
}
