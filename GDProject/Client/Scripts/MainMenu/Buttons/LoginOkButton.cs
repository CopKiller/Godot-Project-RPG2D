using Godot;
using System;

public partial class LoginOkButton : Button
{
    public override void _Ready()
    {
        Connect("pressed", new Callable(this, nameof(OnButtonPressed)));
    }

    private void OnButtonPressed()
    {
        NodeManager.GetNode<Client>("Client").InitConnection();
        NodeManager.GetNode<LoginWindow>("LoginWindow").OnLoginButtonPressed();


        //NodeManager.GetNode<MainMenu>("MainMenu").Hide();

        //NodeManager.GetNode<Game>("Game").Show();
    }
}
