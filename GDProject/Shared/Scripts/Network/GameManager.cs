using GdProject.Shared.Scripts;
using Godot;
using LiteNetLib;

public partial class GameManager : Control
{
    [Export] public bool ServerBuild = false;
    [Export] public bool ClientBuild = false;

    public override void _Ready()
    {

        GD.Print("GameManager Ready");

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

        NetDebug.Logger = new GDPrint();
        NetDebug.Logger.WriteNet(NetLogLevel.Trace, "NetDebug Logger initialized");
    }
}
