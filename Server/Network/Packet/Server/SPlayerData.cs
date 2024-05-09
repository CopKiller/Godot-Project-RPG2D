

using LiteNetLib;
using Server.Model;
using Server.Network;

namespace Network.Packet
{
    internal class SPlayerData : ISend
    {
        public PlayerDataModel PlayerDataModel { get; set; }
        public PlayerPhysicModel PlayerPhysicModel { get; set; }

        public void WritePacket(PacketProcessor packetProcessor, NetPeer netPeer)
        {
            packetProcessor.SendDataTo(this, netPeer.Id, DeliveryMethod.ReliableUnordered);
        }
    }
}
