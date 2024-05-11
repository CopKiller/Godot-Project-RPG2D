
using GdProject.Infrastructure;
using Godot;
using Network.Packet;

public partial class RegisterWindow : BaseWindow
{
    public override void _Ready()
    {
        base._Ready();

        GetNode<Button>("VerticalBox/Button").Connect("pressed", new Callable(this, nameof(OnRegisterButtonPressed)));
    }

    public void OnRegisterButtonPressed()
    {
        var username = GetNode<LineEdit>("VerticalBox/UsernameText").Text;
        var password = GetNode<LineEdit>("VerticalBox/PasswordText").Text;
        var passwordRepeat = GetNode<LineEdit>("VerticalBox/RepeatPasswordText").Text;
        var email = GetNode<LineEdit>("VerticalBox/EmailText").Text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            NodeManager.GetNode<AlertMsg>("AlertMsg").ShowAlert("Username or password is empty");
            return;
        }

        if (password != passwordRepeat)
        {
            NodeManager.GetNode<AlertMsg>("AlertMsg").ShowAlert("Passwords do not match");
            return;
        }

        if (username.Length < 3 || password.Length < 3)
        {
            NodeManager.GetNode<AlertMsg>("AlertMsg").ShowAlert("Username or password is too short");
            return;
        }

        if (email.Length < 3)
        {
            NodeManager.GetNode<AlertMsg>("AlertMsg").ShowAlert("Email is too short");
            return;
        }

        if (!email.Contains('@') || !email.Contains('.'))
        {
            NodeManager.GetNode<AlertMsg>("AlertMsg").ShowAlert("Email is invalid");
            return;
        }

        NodeManager.GetNode<ClientManager>(nameof(ClientManager)).Start();

        new CNewAccount
        {
            Login = username,
            Password = password,
            Email = email
        }.WritePacket(ClientManager.LocalPlayer.PacketProcessor);
    }
}
