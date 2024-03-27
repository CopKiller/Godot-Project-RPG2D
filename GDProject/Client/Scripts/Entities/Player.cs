using GdProject.Shared.Scripts.Class;
using GdProject.Shared.Scripts.NodeManager;
using Godot;
using Shared.Scripts.Player;

public partial class Player : PlayerPhysicsModel
{

    public void SetPlayerName(string playerName)
    {
        playerData.PlayerName = playerName;
        NodeManager.GetNode<RichTextLabel>("PlayerName").Text = playerName;
    }
}
