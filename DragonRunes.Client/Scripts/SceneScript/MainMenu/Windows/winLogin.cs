using DragonRunes.Client.Scripts;
using DragonRunes.Scripts.Network;
using DragonRunes.Network;
using Godot;

public partial class winLogin : WindowBase
{
    public override void _Ready()
    {
        base._Ready();

        // Inicia os componentes da cena
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        AssignButtons();
        AssignTextFields();

    }
    private void AssignButtons()
    {
        NodeManager.GetNode<Button>("btnGo").Pressed += () => GoLogin();
    }
    private void AssignTextFields()
    {
        NodeManager.GetNode<LineEdit>("txtLogin").TextChanged += (text) => UserText(text);
        NodeManager.GetNode<LineEdit>("txtPass").TextChanged += (text) => PassText(text);
    }

    private void GoLogin()
    {
        var clientManager = NodeManager.GetNode<ClientManager>(nameof(ClientManager));

        var playerPeer = clientManager._player.CurrentPeer;

        var packetProcessor = clientManager._networkService._clientPacketProcessor;

        var loginField = NodeManager.GetNode<LineEdit>("txtLogin").Text;
        var passField = NodeManager.GetNode<LineEdit>("txtPass").Text;

        if (loginField.IsValidName() && passField.IsValidPassword())
        {
            packetProcessor.SendLogin(playerPeer, loginField, passField);
        }
    }

    private void UserText(string text)
    {
        if (text.IsValidName())
            NodeManager.GetNode<LineEdit>("txtLogin").AddThemeColorOverride("font_color", new Color(0, 1, 0));
        else
            NodeManager.GetNode<LineEdit>("txtLogin").AddThemeColorOverride("font_color", new Color(1, 0, 0));
    }
    private void PassText(string text)
    {
        if (text.IsValidPassword())
            NodeManager.GetNode<LineEdit>("txtPass").AddThemeColorOverride("font_color", new Color(0, 1, 0));
        else
            NodeManager.GetNode<LineEdit>("txtPass").AddThemeColorOverride("font_color", new Color(1, 0, 0));
    }
}
