
using LiteNetLib;
using Server.Model;

namespace Server.Network.Packet.Server
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
