

using LiteNetLib;
using Server.Model;

namespace Server.Network.Packet.Server
{
    internal class SPlayerData : ISend
    {
        public PlayerDataModel PlayerDataModel { get; set; }

        public void WritePacket(PacketProcessor packetProcessor, NetPeer netPeer)
        {
        packetProcessor.SendDataTo(this, netPeer.Id, DeliveryMethod.ReliableUnordered);
        }
    }
}
