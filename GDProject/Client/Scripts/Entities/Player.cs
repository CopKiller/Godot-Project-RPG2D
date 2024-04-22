
using GdProject.Infrastructure;
using GdProject.Logger;
using GdProject.Model;
using Godot;
using Shared.Scripts.Player;
using System.Collections.Generic;

public partial class Player : PlayerPhysicsModel
{

    public void UpdatePlayer()
    {
        //GetChild<RichTextLabel>(0).Text = PlayerData.PlayerName;
        GetNode<RichTextLabel>("PlayerName").Text = PlayerData.PlayerName;
        Position = new Vector2(PlayerData.Position.X, PlayerData.Position.Y);
        this.Show();
    }

    public void DuplicatePlayer(Player player)
    {
        var newPlayer = (Player)Duplicate();

        newPlayer.IsLocalPlayer = false;

        newPlayer.PlayerData = player.PlayerData;

        newPlayer.Name = player.PlayerData.Index.ToString();

        GetParent().AddChild(newPlayer);
        NodeManager.AddNode(newPlayer);

        newPlayer.UpdatePlayer();

        newPlayer.Show();
    }

    public void AddLocalPlayer(Player player)
    {
        IsLocalPlayer = true;

        PlayerData = player.PlayerData;

        UpdatePlayer();

        Show();
    }

    public void UpdatePlayerAction(Vector2 position)
    {
        Position = position;
    }
}
