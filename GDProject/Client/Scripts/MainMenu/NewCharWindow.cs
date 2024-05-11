using GdProject.Infrastructure;
using Godot;
using Network.Packet;

public partial class NewCharWindow : BaseWindow
{
    public override void _Ready()
    {
        base._Ready();

        GetNode<Button>("VerticalBox/Button").Connect("pressed", new Callable(this, nameof(OnCreateCharButtonPressed)));
    }

    public void OnCreateCharButtonPressed()
    {
        var name = GetNode<LineEdit>("VerticalBox/PlayerNameText").Text;

        if (string.IsNullOrEmpty(name))
        {
            NodeManager.GetNode<AlertMsg>("AlertMsg").ShowAlert("Character name is empty");
            return;
        }

        if (name.Length < 3)
        {
            NodeManager.GetNode<AlertMsg>("AlertMsg").ShowAlert("Character name is too short");
            return;
        }

        new CNewChar
        {
            Name = name
        }.WritePacket(ClientManager.LocalPlayer.PacketProcessor);
    }
}
