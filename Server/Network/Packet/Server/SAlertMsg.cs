using LiteNetLib;
using Server.Network;

namespace Network.Packet
{
    internal class SAlertMsg : ISend
    {
        public string Msg { get; set; }
        public void WritePacket(PacketProcessor netPacketProcessor, NetPeer netPeer)
        {
            netPacketProcessor.SendDataTo(this, netPeer.Id, DeliveryMethod.ReliableUnordered);
        }
    }
}
