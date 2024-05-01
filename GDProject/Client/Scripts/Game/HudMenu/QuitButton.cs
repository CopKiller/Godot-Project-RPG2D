
using Godot;

public partial class QuitButton : TextureButton
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        this.Connect("pressed", new Callable(this, nameof(GoBack)));
    }

	private void GoBack()
    {
        GD.Print("QuitButton pressed");
        NodeManager.GetNode<Game>(nameof(Game)).EndGame();
    }
}
