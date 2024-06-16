
using GdProject.Client.Scripts.Entities.Player;
using DragonRunes.Shared.CustomDataSerializable;

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

        newPlayer.InitializePlayerModel(player.playerDataModel);
    }

    public void AddLocalPlayer(PlayerController player)
    {
        IsLocalPlayer = true;

        this.InitializePlayerModel(player.playerDataModel);
    }

    private void InitializePlayerModel(PlayerDataModel playerDataModel)
    {
        this.playerDataModel = playerDataModel;

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
        //NodeManager.RemoveNode(this.Name);
        QueueFree();
    }
}
