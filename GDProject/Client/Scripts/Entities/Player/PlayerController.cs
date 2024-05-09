
using GdProject.Client.Scripts.Entities.Player;
using GdProject.Model;
using Godot;

public partial class PlayerController : PlayerInput
{

    public override void _Ready()
    {
        
    }

    public void DuplicatePlayer(PlayerController player)
    {
        var newPlayer = (PlayerController)Duplicate();

        newPlayer.IsLocalPlayer = false;

        newPlayer.Name = player.playerDataModel.Index.ToString();

        GetParent().AddChild(newPlayer);
        NodeManager.AddNode(newPlayer);

        newPlayer.InitializePlayerModel(player.playerDataModel, player.playerPhysicModel);
    }

    public void AddLocalPlayer(PlayerController player)
    {
        IsLocalPlayer = true;

        this.InitializePlayerModel(player.playerDataModel, player.playerPhysicModel);
    }

    private void InitializePlayerModel(PlayerDataModel playerDataModel, PlayerPhysicModel playerPhysicModel)
    {
        this.playerDataModel = playerDataModel;
        this.playerPhysicModel = playerPhysicModel;

        this.InitializePlayerPhysics();
        this.InitializePlayerData();
        this.InitializePlayerNetwork();

        this.InGame = true;

        this.Show();
    }

    public void RemovePlayer()
    {
        InGame = false;
        GetParent().RemoveChild(this);
        QueueFree();
    }
}
