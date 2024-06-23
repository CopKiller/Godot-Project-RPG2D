

public partial class PlayerController : PlayerInput
{

    public override void _Ready()
    {
        
    }

    public void InitializePlayerModel()
    {
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
        NodeManager.RemoveNode<PlayerController>(this.Name);
        QueueFree();
    }
}
