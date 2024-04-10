using LiteNetLib;

namespace Server.Network.Packet.Server
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
