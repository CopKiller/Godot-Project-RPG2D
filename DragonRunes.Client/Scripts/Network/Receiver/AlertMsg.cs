using DragonRunes.Client.Scripts;
using DragonRunes.Client.Scripts.Logger;
using DragonRunes.Network.Packet;
using DragonRunes.Network.Packet.Server;
using Godot;
using LiteNetLib;

namespace DragonRunes.Scripts.Network
{
    public partial class ClientPacketProcessor
    {
        public void AlertMsg(SAlertMsg obj, NetPeer netPeer)
        {
            var alertmsgWindow = NodeManager.GetNode<ClientManager>("ClientManager");

            alertmsgWindow.CallDeferred(nameof(alertmsgWindow.AlertMsg), obj.Msg);
        }
    }
}
