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
        public void ServerAlertMsg(SAlertMsg obj, NetPeer netPeer)
        {
            var alertMsg = NodeManager.GetNode<AlertMsgWindow>("AlertMsg");

            alertMsg.CallDeferred(nameof(alertMsg.SetText), obj.Msg);
        }
    }
}
