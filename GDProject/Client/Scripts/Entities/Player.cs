
using Godot;
using Shared.Scripts.Player;

public partial class Player : PlayerPhysicsModel
{

    public void UpdatePlayer()
    {
        GetNode<RichTextLabel>("PlayerName").Text = PlayerData.PlayerName;

        GetNode<ColorRect>("ColorRect").Size = GetNode<RichTextLabel>("PlayerName").GetMinimumSize();
        Vector2 position = GetNode<RichTextLabel>("PlayerName").Position;
        position.X = -GetNode<ColorRect>("ColorRect").Size.X / 2;
        GetNode<ColorRect>("ColorRect").Position = position;

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
