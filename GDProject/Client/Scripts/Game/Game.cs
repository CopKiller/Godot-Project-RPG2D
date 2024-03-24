using Godot;
using System;
using GdProject.Shared.Scripts.NodeManager;

public partial class Game : Node2D
{
    public override void _Ready()
    {
        // Adiciona este nó ao gerenciador de nós
        NodeManager.AddToNodeManager(this);
    }

    public override void _Process(double delta)
    {
        
    }
}

