using DragonRunes.Network.Packet;
using DragonRunes.Network.Packet.Server;
using Godot;
using LiteNetLib;

namespace DragonRunes.Scripts.Network
{
    public partial class ClientPacketProcessor
    {
        public void NewChar(SNewChar obj, NetPeer netPeer)
        {

            //ClientManager.LocalPlayer.GameState = GameState.InCharacterCreation;

            //var windowsNode = NodeManager.GetNode<Windows>("Windows");

            //windowsNode.CallDeferred(nameof(windowsNode.CloseAll));
            //windowsNode.CallDeferred(nameof(windowsNode.Open), NodeManager.GetNode<Window>("NewCharWindow"));
        }
    }
}
