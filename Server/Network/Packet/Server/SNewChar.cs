
using LiteNetLib;
using LiteNetLib.Utils;

namespace Server.Network.Packet.Server
{
    internal class SNewChar : ISend
    {
        public void WritePacket(PacketProcessor netPacketProcessor, NetPeer netPeer)
        {
            netPacketProcessor.SendDataTo(this, netPeer.Id, DeliveryMethod.ReliableUnordered);
        }
    }
}
