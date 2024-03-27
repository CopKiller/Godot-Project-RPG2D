using GdProject.Client.Scripts.Window.Controller;
using GdProject.Client.Scripts.Window.Interface;
using GdProject.Shared.Scripts.NodeManager;
using Godot;
using System;

public partial class Client : Node
{
    public override void _Ready()
    {
        var clientStart = GetTree().Root.GetNode<RPG2D>("RPG2D").ClientStart;

        GD.Print("Client Start: " + clientStart);
        if (!clientStart)
        {
            QueueFree();
            return;
        }

        // Adiciona este nó e os filhos ao gerenciador de nós
        NodeManager.AddToNodeManager(this);

        InitMenu();
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
        var gameClient = new GameClient();
        AddChild(gameClient);

        NodeManager.AddNode(gameClient);
    }


}
