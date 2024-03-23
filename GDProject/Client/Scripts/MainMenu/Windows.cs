using GdProject.Client.Scripts.Window.Controller;
using Godot;
using System;

public partial class Windows : Control
{
    public ActiveWindows activeWindows;

    public override void _Ready()
    {
        var MainWindow = GetNode<MainWindow>("MainWindow");

        activeWindows = new ActiveWindows();
        activeWindows.AddActiveWindow(MainWindow);
    }
    public ActiveWindows GetActiveWindows()
    {
        return activeWindows;
    }
}
