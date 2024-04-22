
using LiteNetLib;
using Server.Network;

namespace Network.Packet
{
    internal class SNewChar : ISend
    {
        public void WritePacket(PacketProcessor netPacketProcessor, NetPeer netPeer)
        {
            netPacketProcessor.SendDataTo(this, netPeer.Id, DeliveryMethod.ReliableUnordered);
        }
    }
}
