using GdProject.Client.Scripts.Window.Controller;
using GdProject.Client.Scripts.Window.Interface;
using GdProject.Shared.Scripts.NodeManager;
using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class Windows : Control
{
    public ActiveWindows activeWindows;

    public override void _Ready()
    {
        NodeManager.AddToNodeManager(this);

        var MainWindow = (IControlWindow)NodeManager.GetNode<MainWindow>("MainWindow");

        activeWindows = new ActiveWindows();
        activeWindows.AddActiveWindow(MainWindow);
    }
    public ActiveWindows GetActiveWindows()
    {
        return activeWindows;
    }
}
