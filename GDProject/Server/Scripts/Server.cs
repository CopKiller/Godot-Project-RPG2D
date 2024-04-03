using Godot;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;

public partial class Server : Node
{
    //public ServerNetworkService server;
    public override void _Ready()
    {
        var serverStart = GetTree().Root.GetNode<RPG2D>("RPG2D").ServerStart;

        GD.Print("Server Start: " + serverStart);
        if (!serverStart)
        {
            QueueFree();
            return;
        }

        InitServer();
    }

    public void InitServer()
    {
        var server = new ServerNetworkService();
        server.Name = "ServerNetworkService";

        AddChild(server);
        NodeManager.AddNode(server);
    }
}
