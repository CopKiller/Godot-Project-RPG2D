using Godot;

public partial class MenuWindow : BaseWindow
{
    public override void _Ready()
    {
        base._Ready();

        NodeManager.AddToNodeManager(this);

        AssingButtonSignals();
    }
    private void AssingButtonSignals()
    {
        var emitter = GetTree().GetNodesInGroup("MenuButtons");

        // Conecta o sinal do botão pressionado ao manipulador
        foreach (BaseButton button in emitter)
        {
            button.Pressed += () => _OnButtonPressed(button.Name);
        }
    }

    private void _OnButtonPressed(string buttonName)
    {
        // Verifica se é o botão de jogar
        if (buttonName == "LoginButton")
        {
            GD.Print("LoginButton Pressed");
            NodeManager.GetNode<Window>("LoginWindow").Show();
        }
        else if (buttonName == "RegisterButton")
        {
            GD.Print("LoginButton Pressed");
            NodeManager.GetNode<Window>("RegisterWindow").Show();
        }
        else if (buttonName == "ExitButton")
        {
            // Fecha o jogo
            GetTree().Quit();
        }
    }
}
