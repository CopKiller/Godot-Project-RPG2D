using GdProject.Shared.Scripts.NodeManager;
using Godot;
using System;

public partial class RPG2D : Node2D
{
    [Export] public bool ServerStart = false;
    [Export] public bool ClientStart = false;

    public override void _Ready()
    {
        if (ServerStart)
        {
            NodeManager.AddToNodeManager(GetNode<Node>("Server"));
        }
        
        if (ClientStart)
        {
            NodeManager.AddToNodeManager(GetNode<Node>("Client"));
        }

        // Remove o nó compartilhado por não estar sendo usado
        GetNode<Node>("Shared").QueueFree();
    }
}
