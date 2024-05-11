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
        //NodeManager.GetNode<Windows>(nameof(Windows)).CloseAll();

        ClientManager.LocalPlayer.Disconnect();

        //NodeManager.GetNode<PlayerController>("Player").Hide();

        //NodeManager.GetNode<Control>("HudMenu").Hide();

        //NodeManager.GetNode<CanvasLayer>("UI").Hide();

        // Fecha o jogo
        GetTree().Quit();

    }

    //private void ConnectToServer()
    //{
    //    NodeManager.GetNode<ClientNode>("Client").InitMenu();

    //    var timer = new Timer();
    //    timer.WaitTime = 5000;
    //    timer.OneShot = true;
    //    timer.Connect("timeout", new Callable(timer, nameof(OnTimerTimeout)));

    //}

    //private void OnTimerTimeout()
    //{
    //    NodeManager.GetNode<ClientNode>(nameof(ClientNode)).InitNetwork();
    //}
}

