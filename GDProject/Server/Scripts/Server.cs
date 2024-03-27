using Godot;
using System;

public partial class Server : Node
{
    public override void _Ready()
    {
        var serverStart = GetTree().Root.GetNode<RPG2D>("RPG2D").ServerStart;

        GD.Print("Server Start: " + serverStart);
        if (!serverStart)
        {
            QueueFree();
            return;
        }

        GameServer server = new GameServer();
        AddChild(server);
    }
}
