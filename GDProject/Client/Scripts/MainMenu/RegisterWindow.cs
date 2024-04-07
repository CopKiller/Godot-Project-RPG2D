
using GdProject.Network;
using Godot;
using Shared.Window.CustomControl;

public partial class RegisterWindow : WindowTextureRect
{
    public override void _Ready()
    {
        //AssingButtonSignals();
    }

    public void OnRegisterButtonPressed()
    {
        var username = GetNode<LineEdit>("VerticalBox/LoginText").Text;
        var password = GetNode<LineEdit>("VerticalBox/PasswordText").Text;

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

        NodeManager.GetNode<ClientNetworkService>(nameof(ClientNetworkService)).Register(username, password);
    }
}
