
using DragonRunes.Network.Packet.Server;
using LiteNetLib;

namespace DragonRunes.Server.Network
{
    public partial class ServerPacketProcessor
    {
        public void ServerAlertMsg(NetPeer netPeer, string message)
        {
            var SAlert = new SAlertMsg();
            SAlert.Msg = message;
            SendDataTo(netPeer, SAlert, DeliveryMethod.ReliableOrdered);
        }
    }
}
