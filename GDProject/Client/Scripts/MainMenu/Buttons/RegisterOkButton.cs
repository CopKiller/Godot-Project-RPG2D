using Godot;
using System;

public partial class RegisterOkButton : Button
{
    public override void _Ready()
    {
        Connect("pressed", new Callable(this, nameof(OnButtonPressed)));
        //NodeManager.GetNode<Client>("Client").InitConnection();
    }

    private void OnButtonPressed()
    {
        //NodeManager.GetNode<Client>("Client").InitConnection();
        NodeManager.GetNode<RegisterWindow>("RegisterWindow").OnRegisterButtonPressed();
    }
}
