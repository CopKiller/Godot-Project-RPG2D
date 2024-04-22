using GdProject.Infrastructure;
using Godot;
using Network.Packet;
using Shared.Window.CustomControl;

public partial class LoginWindow : WindowTextureRect
{
    public override void _Ready()
    {
        //NodeManager.GetNode<LineEdit>("LoginText").GrabFocus();
    }

    public void OnLoginButtonPressed()
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


        var login = new CLogin
        {
            Login = username,
            Password = password
        };
        login.WritePacket(InitClient.LocalPlayer.PacketProcessor);

        //new CLogin
        //{
        //    Login = username,
        //    Password = password
        //}.WritePacket(InitClient.LocalPlayer.PacketProcessor);
    }
}
