
using Godot;

public partial class RPG2D : Node2D
{
    public override void _Ready()
    {
        NodeManager.AddToNodeManager(GetNode<Node>("Client"));
    }
}
