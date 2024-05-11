using GdProject.Infrastructure;
using Godot;
using Network.Packet;

public partial class LoginWindow : BaseWindow
{
    public override void _Ready()
    {
        base._Ready();

        GetNode<Button>("VerticalBox/Button").Connect("pressed", new Callable(this, nameof(OnLoginButtonPressed)));

        GetNode<CheckBox>("VerticalBox/HBoxContainer/SaveUserCheckbox").Connect("toggled", new Callable(this, nameof(SaveUserCheckboxPressed)));

        GetNode<CheckBox>("VerticalBox/HBoxContainer/SavePassCheckbox").Connect("toggled", new Callable(this, nameof(SavePassCheckboxPressed)));
    }

    public void OnLoginButtonPressed()
    {
        var username = GetNode<LineEdit>("VerticalBox/UsernameText").Text;
        var password = GetNode<LineEdit>("VerticalBox/PasswordText").Text;

        var config = NodeManager.GetNode<ConfigManager>(nameof(ConfigManager));

        if (NodeManager.GetNode<CheckBox>("SaveUserCheckbox").ButtonPressed == true)
        {
            config.ConfigData.Username = username;
            config.SaveConfig();
        }
        if (NodeManager.GetNode<CheckBox>("SavePassCheckbox").ButtonPressed == true)
        {
            config.ConfigData.Password = password;
            config.SaveConfig();
        }

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


        new CLogin
        {
            Login = username,
            Password = password
        }.WritePacket(ClientManager.LocalPlayer.PacketProcessor);
    }

    public void SaveUserCheckboxPressed(bool pressed)
    {
        var config = NodeManager.GetNode<ConfigManager>(nameof(ConfigManager));

        if (pressed)
            config.ConfigData.SaveUser = true;
        else
            config.ConfigData.SaveUser = false;

        GD.Print("SaveUserCheckboxPressed: " + config.ConfigData.SaveUser);

        config.SaveConfig();
    }

    public void SavePassCheckboxPressed(bool pressed)
    {

        var config = NodeManager.GetNode<ConfigManager>(nameof(ConfigManager));

        if (pressed)
            config.ConfigData.SavePassword = true;
        else
            config.ConfigData.SavePassword = false;

        GD.Print("SavePassCheckboxPressed: " + config.ConfigData.SavePassword);

        config.SaveConfig();
    }
}
