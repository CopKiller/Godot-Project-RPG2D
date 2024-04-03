using Godot;
using Shared.Window.CustomControl;
using GdProject.Client.Scripts.Window.Controller;
using GdProject.Client.Scripts.Window.Interface;
using System;


public partial class MainWindow : WindowTextureRect
{
    // Obtenha o nó pai (Windows)
    private ActiveWindows activeWindows => GetParent<Windows>().activeWindows;

    private Godot.Collections.Dictionary<MainMenuWindows, Control> subWindows;

    public override void _Ready()
    {
        AssingButtonSignals();
        AddWindowsReferencesToDictionary();

    }
    public void OpenWindow(MainMenuWindows window)
    {
        if (subWindows.ContainsKey(window))
        {
            activeWindows.AddActiveWindow((IControlWindow)subWindows[window]);
        }
    }
    public void CloseWindow(MainMenuWindows window)
    {
        if (subWindows.ContainsKey(window))
        {
            activeWindows.CloseWindow((IControlWindow)subWindows[window]);
        }
    }

    private void AddWindowsReferencesToDictionary()
    {
        MainMenuWindows mainMenuWindows;

        subWindows = new Godot.Collections.Dictionary<MainMenuWindows, Control>();

        // Adiciona as referências das janelas filhas
        foreach (Node node in GetParent().GetChildren(true))
        {
            if (node is IControlWindow window)
            {

                if (Enum.TryParse(window.WindowName, out mainMenuWindows))
                {
                    subWindows.Add(mainMenuWindows, (Control)window);
                }
                else
                {
                    //GD.PrintErr($"{window.WindowName} Not Found");
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
        if (buttonName == "LoginButton")
        {
            GD.Print("LoginButton Pressed");
            var LoginWindow = (IControlWindow)NodeManager.GetNode<WindowTextureRect>("LoginWindow");
            activeWindows.AddActiveWindow(LoginWindow);
        }
        else if (buttonName == "RegisterButton")
        {
            GD.Print("LoginButton Pressed");
            var RegisterWindow = (IControlWindow)NodeManager.GetNode<WindowTextureRect>("RegisterWindow");
            activeWindows.AddActiveWindow(RegisterWindow);
        }
        else if (buttonName == "OptionButton")
        {

        }
        else if (buttonName == "ExitButton")
        {
            // Fecha o jogo
            GetTree().Quit();
        }
    }

}
