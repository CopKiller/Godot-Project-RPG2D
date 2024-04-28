using GdProject.Infrastructure;
using Godot;
using Network.Packet;

public partial class NewCharWindow : Window
{
    public override void _Ready()
    {
        GetNode<Button>("VerticalBox/Button").Connect("pressed", new Callable(this, nameof(OnCreateCharButtonPressed)));
    }

    public void OnCreateCharButtonPressed()
    {
        var name = GetNode<LineEdit>("VerticalBox/PlayerNameText").Text;

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
