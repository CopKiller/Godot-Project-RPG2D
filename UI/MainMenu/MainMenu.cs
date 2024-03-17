using Godot;
using GodotProject.Controller;
using GodotProject.CustomControl;
using System.Collections.Generic;

public partial class MainMenu : Control
{
    float logoAlpha = 0.1f;

    public ActiveWindows activeWindows;

    public override void _Ready()
    {
        activeWindows = new ActiveWindows();

        //GetNode<TextureRect>("Logo").Visible = true;

        activeWindows.AddActiveWindow(GetNode<WindowTextureRect>("MainContainer"));
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {

        //if (logoAlpha > 1)
        //{
        //    logoAlpha = 1;
        //}
        //else
        //{
        //    logoAlpha += 0.3f * (float)delta;
        //}
        //GetNode<TextureRect>("Logo").Modulate = new Color(1, 1, 1, logoAlpha);
    }
}
