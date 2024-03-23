using Shared.Scripts.Player;
using Godot;
using System;

public partial class Game : Node2D
{
    public override void _Ready()
    {

    }

    public override void _Input(InputEvent @event)
    {
        
    }

    public void InitializePlayer(PlayerDataModel playerData)
    {
        var player = new Player();
        player.HandlePlayerData(playerData);
        AddChild(player);
    }

}
