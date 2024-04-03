
using Godot;
using Shared.Scripts.Player;

public partial class Player : PlayerPhysicsModel
{

    public void UpdatePlayer()
    {
        GetNode<RichTextLabel>("PlayerName").Text = PlayerData.PlayerName;
        Position = PlayerData.Position;
        this.Show();
    }
}
