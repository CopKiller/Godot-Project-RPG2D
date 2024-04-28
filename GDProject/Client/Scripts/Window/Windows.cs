using GdProject.Logger;
using Godot;

public partial class Windows : CanvasLayer
{
    public override void _Ready()
    {
        NodeManager.AddNode(this);
    }

    public void Close(Window window)
    {
        window.Hide();
    }

    public void CloseAll()
    {
        var windows = NodeManager.GetNodes<Window>();

        ExternalLogger.Print($"Closing all windows. Total: {windows.Count}");

        windows.FindAll(w => w.Visible).ForEach(w => w.Hide());
    }

    public void CloseAllExcept(Window window)
    {
        var windows = NodeManager.GetNodes<Window>();

        windows.FindAll(w => w != window && w.Visible).ForEach(w => w.Hide());
    }

    public void Open(Window window)
    {
        window.Show();
    }

    public void Open<T>(string name) where T : Window
    {
        var window = NodeManager.GetNode<T>(name);

        if (window != null)
        {
            window.Show();
        }
    }

}
