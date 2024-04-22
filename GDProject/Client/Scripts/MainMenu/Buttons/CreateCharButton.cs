using Godot;

public partial class CreateCharButton : Button
{
    public override void _Ready()
    {
        Connect("pressed", new Callable(this, nameof(OnCreateCharButtonPressed)));
    }

    private void OnCreateCharButtonPressed()
    {
        NodeManager.GetNode<CharacterWindow>("CharacterWindow").OnCreateCharButtonPressed();
    }
}
