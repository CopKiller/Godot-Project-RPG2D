
using GdProject.Infrastructure;
using Godot;
using Network.Packet;

public partial class RegisterWindow : Window
{
    public override void _Ready()
    {
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
            GD.Print("Username or password is empty");
            return;
        }

        if (password != passwordRepeat)
        {
            GD.Print("Passwords do not match");
            return;
        }

        if (username.Length < 3 || password.Length < 3)
        {
            GD.Print("Username or password is too short");
            return;
        }

        if (email.Length < 3)
        {
            GD.Print("Email is too short");
            return;
        }

        if (!email.Contains('@') || !email.Contains('.'))
        {
            GD.Print("Email is invalid");
            return;
        }

        GD.Print("Username: " + username);
        GD.Print("Password: " + password);
        GD.Print("Email: " + email);

        new CNewAccount
        {
            Login = username,
            Password = password,
            Email = email
        }.WritePacket(InitClient.LocalPlayer.PacketProcessor);
    }
}
