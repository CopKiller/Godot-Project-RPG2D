using Godot;

public partial class Game : Node2D
{
    public override void _Ready()
    {
        // Adiciona este nó ao gerenciador de nós
        NodeManager.AddToNodeManager(this);
    }
}

