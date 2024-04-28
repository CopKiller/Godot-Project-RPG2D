using GdProject.Infrastructure;
using Godot;
using Network.Packet;

public partial class LoginWindow : Window
{
    public override void _Ready()
    {
        GetNode<Button>("VerticalBox/Button").Connect("pressed", new Callable(this, nameof(OnLoginButtonPressed)));
    }

    public void OnLoginButtonPressed()
    {
        var username = GetNode<LineEdit>("VerticalBox/UsernameText").Text;
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
