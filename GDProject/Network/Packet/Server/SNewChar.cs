

using GdProject.Infrastructure;
using Godot;

namespace Network.Packet
{
    public class SNewChar : IRecv
    {
        public void ReadPacket(int peerId)
        {
            ClientManager.LocalPlayer.GameState = GameState.InCharacterCreation;

            var windowsNode = NodeManager.GetNode<Windows>("Windows");

            windowsNode.CallDeferred(nameof(windowsNode.CloseAll));
            windowsNode.CallDeferred(nameof(windowsNode.Open), NodeManager.GetNode<Window>("NewCharWindow"));
        }
    }
}
