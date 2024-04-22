using GdProject.Infrastructure;
using Godot;
using Network.Packet;
using Shared.Window.CustomControl;

public partial class CharacterWindow : WindowTextureRect
{
    public void OnCreateCharButtonPressed()
    {
        var name = GetNode<LineEdit>("CharNameText").Text;

        if (string.IsNullOrEmpty(name))
        {
            GD.Print("Character name is empty");
            return;
        }

        if (name.Length < 3)
        {
            GD.Print("Character name is too short");
            return;
        }

        new CNewChar
        {
            Name = name
        }.WritePacket(InitClient.LocalPlayer.PacketProcessor);
    }
}
