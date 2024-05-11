using GdProject.Infrastructure;
using Godot;
using Network.Packet;

public partial class LoginWindow : BaseWindow
{
    public override void _Ready()
    {
        base._Ready();

        GetNode<Button>("VerticalBox/Button").Connect("pressed", new Callable(this, nameof(OnLoginButtonPressed)));
    }

    public void OnLoginButtonPressed()
    {
        var username = GetNode<LineEdit>("VerticalBox/UsernameText").Text;
        var password = GetNode<LineEdit>("VerticalBox/PasswordText").Text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            NodeManager.GetNode<AlertMsg>("AlertMsg").ShowAlert("Username or password is empty");
            return;
        }

        if (username.Length < 3 || password.Length < 3)
        {
            NodeManager.GetNode<AlertMsg>("AlertMsg").ShowAlert("Username or password is too short");
            return;
        }


        var login = new CLogin
        {
            Login = username,
            Password = password
        };
        login.WritePacket(ClientManager.LocalPlayer.PacketProcessor);

        //new CLogin
        //{
        //    Login = username,
        //    Password = password
        //}.WritePacket(InitClient.LocalPlayer.PacketProcessor);
    }
}
