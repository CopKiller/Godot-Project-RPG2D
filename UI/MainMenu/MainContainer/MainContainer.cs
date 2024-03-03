using Godot;

public partial class MainContainer : ColorRect
{
    public override void _Ready()
    {
        AssingButtonSignals();
    }

    private void _OnButtonPressed(string buttonName)
    {

        // Verifica se é o botão de jogar
        if (buttonName == "PlayButton")
        {
            StartGame();
        }
        else if (buttonName == "RegisterButton")
        {
            HideMainContainer();
            ShowRegisterContainer();
        }
        else if (buttonName == "OptionsButton")
        {

        }
        else if (buttonName == "ExitButton")
        {
            // Fecha o jogo
            GetTree().Quit();
        }
    }
    private void StartGame()
    {
        // Carrega a cena do jogo
        GetTree().ChangeSceneToFile("res://scenes/game.tscn");
    }
    private void HideMainContainer()
    {
        Owner.GetNode<ColorRect>("MainContainer").Visible = false;
    }
    private void ShowRegisterContainer()
    {
        GetParent().GetNode<ColorRect>("RegisterContainer").Visible = true;
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

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}
}
