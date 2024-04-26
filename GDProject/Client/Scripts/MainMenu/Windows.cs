using GdProject.Client.Scripts.Window.Controller;
using GdProject.Client.Scripts.Window.Interface;
using Godot;

public partial class Windows : Control
{
    public ActiveWindows activeWindows;

    public override void _Ready()
    {
        NodeManager.AddToNodeManager(this);

        //var MainWindow = (IControlWindow)NodeManager.GetNode<MainWindow>("MainWindow");

        //activeWindows = new ActiveWindows();
        //activeWindows.AddActiveWindow(MainWindow);
    }
    public ActiveWindows GetActiveWindows()
    {
        return activeWindows;
    }
    public void AddActiveWindow(IControlWindow window)
    {
        activeWindows.AddActiveWindow(window);
    }

    public void AddActiveWindow(CharacterWindow window)
    {
        activeWindows.AddActiveWindow(window);
    }

    public void CloseWindow(IControlWindow window)
    {
        activeWindows.CloseWindow(window);
    }

    public void CloseAllWindows()
    {
        activeWindows.CloseAllWindows();
    }
}
