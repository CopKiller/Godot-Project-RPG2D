using Godot;

public partial class winLogin : WindowBase
{
    public override void _Ready()
    {
        base._Ready();

        // Inicia os componentes da cena
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        AssignButtons();
    }
    private void AssignButtons()
    {
        NodeManager.GetNode<Button>("btnLogin").Pressed += () => OnButtonPressed();
    }

    private void OnButtonPressed()
    {
        
    }
}
