using LiteNetLib;
using Server.Network;

namespace Network.Packet
{
    internal class SLeft : ISend
    {
        public int Index { get; set; }

        public void WritePacket(PacketProcessor netPacketProcessor, NetPeer netPeer)
        {
            netPacketProcessor.SendDataToAllBut(this, netPeer.Id, DeliveryMethod.ReliableUnordered);

        }
    }
}
