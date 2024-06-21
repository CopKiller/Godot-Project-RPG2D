
using DragonRunes.Network.Packet.Server;
using LiteNetLib;

namespace DragonRunes.Server.Network
{
    public partial class ServerPacketProcessor
    {
        public void ServerNewChar(NetPeer netPeer)
        {
            var packet = new SNewChar();
            SendDataTo(netPeer, packet, DeliveryMethod.ReliableUnordered);
        }
    }
}
