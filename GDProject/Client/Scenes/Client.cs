using GdProject.Client.Scripts.Window.Controller;
using GdProject.Client.Scripts.Window.Interface;
using GdProject.Shared.Scripts.NodeManager;
using Godot;
using System;

public partial class Client : Node
{

    public override void _Ready()
    {
        NodeManager.AddToNodeManager(this);

        NodeManager.GetNode<Game>(nameof(Game)).Show();
    }
}
