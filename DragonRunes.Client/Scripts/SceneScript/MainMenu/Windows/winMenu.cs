using Godot;

public partial class winMenu : WindowBase
{
    public override void _Ready()
    {
        // Inicia os componentes da cena
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        AssignButtons();
    }
    private void AssignButtons()
    {
        var emitter = GetTree().GetNodesInGroup("MenuButtons");

        foreach (Node node in emitter)
        {
            if (node is Button)
            {
                ((Button)node).Pressed += () => OnButtonPressed(((Button)node).Name);
            }
        }

    }
    private void OnButtonPressed(string buttonName)
    {
        switch (buttonName)
        {
            case "btnLogin":
                NodeManager.GetNode<WindowBase>("winLogin").Show();
                break;
            case "btnRegister":
                NodeManager.GetNode<WindowBase>("winRegister").Show();
                break;
            case "btnRecovery":
                //NodeManager.GetNode<WindowBase>("winLogin").Show();
                break;
            case "btnOptions":
                NodeManager.GetNode<WindowBase>("winOptions").Show();
                break;
            case "btnCredits":
                //NodeManager.GetNode<WindowBase>("winLogin").Show();
                break;
            case "btnExit":
                GetTree().Quit();
                break;
        }
    }
}
