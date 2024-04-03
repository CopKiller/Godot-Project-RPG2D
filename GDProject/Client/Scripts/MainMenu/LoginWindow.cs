﻿using Godot;
using Shared.Window.CustomControl;
using System;

public partial class LoginWindow : WindowTextureRect
{
    public override void _Ready()
    {
        //NodeManager.GetNode<LineEdit>("LoginText").GrabFocus();
    }

    public void OnLoginButtonPressed()
    {
        var username = NodeManager.GetNode<LineEdit>("LoginText").Text;
        var password = NodeManager.GetNode<LineEdit>("PasswordText").Text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            GD.Print("Username or password is empty");
            return;
        }

        if (username.Length < 3 || password.Length < 3)
        {
            GD.Print("Username or password is too short");
            return;
        }

        GD.Print("Username: " + username);
        GD.Print("Password: " + password);

        NodeManager.GetNode<ClientNetworkService>(nameof(ClientNetworkService)).Login(username, password);
    }
}
