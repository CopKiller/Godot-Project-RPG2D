using Godot;
using GodotProject.Controller;
using GodotProject.CustomControl;
using System.Collections.Generic;

public partial class MainMenu : Control
{

    public ActiveWindows activeWindows;

    public override void _Ready()
    {
        activeWindows = new ActiveWindows();

        activeWindows.AddActiveWindow(GetNode<WindowTextureRect>("MainContainer"));
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {

    }
}
