
using LiteNetLib;
using Server.Model;
using Server.Network;

namespace Network.Packet
{
    internal class SPeersAll : ISend
    {
        public List<PlayerDataModel> PlayerDataModels { get; set; }

        public void WritePacket(PacketProcessor netPacketProcessor, NetPeer netPeer)
        {
            netPacketProcessor.SendDataTo(this, netPeer.Id, DeliveryMethod.ReliableUnordered);
        }

    }
}
