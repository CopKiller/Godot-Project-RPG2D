
using GdProject.Infrastructure;
using GdProject.Logger;
using Godot;

namespace GdProject.Client;
public partial class ClientNode : Node
{
    public override void _Ready()
    {

        InitClient();

        InitMenu();

        InitConfigs();
    }

    public void InitClient()
    {
        NodeManager.GetNode<ClientManager>(nameof(ClientManager)).Start();
    }

    public void InitMenu()
    {
        NodeManager.GetNode<Game>(nameof(Game)).Hide();
        NodeManager.GetNode<MainMenu>(nameof(MainMenu)).InitMenu();

        NodeManager.GetNode<Camera2D>(nameof(Camera2D)).Zoom = new Vector2(1, 1);
    }

    public void InitGame()
    {
        NodeManager.GetNode<MainMenu>(nameof(MainMenu)).HideMenu();
        NodeManager.GetNode<Game>(nameof(Game)).InitGame();

        NodeManager.GetNode<Camera2D>(nameof(Camera2D)).Zoom = new Vector2(2, 2);
    }

    public void InitConfigs()
    {
        var config = NodeManager.GetNode<ConfigManager>(nameof(ConfigManager));

        config.LoadConfig();

        if (config.ConfigData.SaveUser)
        {
            NodeManager.GetNode<CheckBox>("SaveUserCheckbox").ButtonPressed = true;

            if (!string.IsNullOrEmpty(config.ConfigData.Username))
            {
                NodeManager.GetNode<LineEdit>("UsernameText").Text = config.ConfigData.Username;
            }
        }

        if (config.ConfigData.SavePassword)
        {
            NodeManager.GetNode<CheckBox>("SavePassCheckbox").ButtonPressed = true;

            if (!string.IsNullOrEmpty(config.ConfigData.Password))
            {
                NodeManager.GetNode<LineEdit>("PasswordText").Text = config.ConfigData.Password;
            }
        }

    }
}
