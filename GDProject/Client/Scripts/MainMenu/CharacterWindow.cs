using Godot;
using Shared.Window.CustomControl;
using System;

public partial class CharacterWindow : WindowTextureRect
{
    public void OnCreateCharButtonPressed()
    {
        var name = GetNode<LineEdit>("CharNameText").Text;
        NodeManager.GetNode<ClientNetworkService>(nameof(ClientNetworkService)).CreateChar(name);
    }
}
