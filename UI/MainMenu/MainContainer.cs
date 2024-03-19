using Godot;
using Godot.Collections;
using GodotProject.Window.CustomControl;
using System;

public partial class MainContainer : WindowTextureRect
{
    private Dictionary<MainMenuWindows, WindowTextureRect> subWindows;

    public override void _Ready()
    {
        AssingButtonSignals();
        AddWindowsReferencesToDictionary();
    }
    public void OpenWindow(MainMenuWindows window)
    {
        if (subWindows.ContainsKey(window))
        {
            GetParent<MainMenu>().activeWindows.AddActiveWindow(subWindows[window]);
        }
    }
    public void CloseWindow(MainMenuWindows window)
    {
        if (subWindows.ContainsKey(window))
        {
            GetParent<MainMenu>().activeWindows.CloseWindow(subWindows[window]);
        }
    }

    private void AddWindowsReferencesToDictionary()
    {
        MainMenuWindows mainMenuWindows;

        subWindows = new Dictionary<MainMenuWindows, WindowTextureRect>();

        // Adiciona as referências das janelas filhas
        foreach (Node node in GetParent().GetChildren(true))
        {
            if (node is WindowTextureRect window)
            {

                if (Enum.TryParse(window.Name, out mainMenuWindows))
                {
                    subWindows.Add(mainMenuWindows, window);
                }
                else
                {
                    GD.PrintErr($"{window.Name} Not Found");
                }
            }
        }
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
        if (buttonName == "PlayButton")
        {
            StartGame();
        }
        else if (buttonName == "RegisterButton")
        {
            OpenWindow(MainMenuWindows.RegisterContainer);
        }
        else if (buttonName == "OptionsButton")
        {
            OpenWindow(MainMenuWindows.OptionContainer);
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

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
        
	}

}
