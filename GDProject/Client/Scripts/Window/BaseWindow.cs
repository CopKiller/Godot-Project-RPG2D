using Godot;

public partial class BaseWindow : Window
{

    public override void _Ready()
    {
        this.Connect("close_requested", new Callable(this, nameof(Close)));
    }

    public void Close()
    {
        Hide();
    }
}
