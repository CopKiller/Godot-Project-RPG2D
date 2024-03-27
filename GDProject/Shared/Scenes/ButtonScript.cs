using Godot;
using System;

public partial class ButtonScript : Button
{


    public override void _Ready()
    {
        Connect("pressed", new Callable(this, "OnButtonPressed"));
    }

    public void OnButtonPressed()
    {
        GD.Print("Button pressed");

        GameClient client = new GameClient();
        AddChild(client);
    }

}
