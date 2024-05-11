using GdProject.Client;
using GdProject.Infrastructure;
using Godot;
using System;

public partial class Game : Node2D
{
    public override void _Ready()
    {
        // Adiciona este nó ao gerenciador de nós
        //NodeManager.AddToNodeManager(this);
    }

    public void InitGame()
    {
        this.Show();
        NodeManager.GetNode<CanvasLayer>("UI").Show();
    }

    public void EndGame()
    {
        NodeManager.GetNode<Windows>(nameof(Windows)).CloseAll();

        ClientManager.LocalPlayer.Disconnect();

        NodeManager.GetNode<PlayerController>(nameof(PlayerController)).Hide();

        NodeManager.GetNode<Control>("HudMenu").Hide();

        NodeManager.GetNode<ClientNode>("Client").InitMenu();

        NodeManager.GetNode<CanvasLayer>("UI").Hide();
    }
}

