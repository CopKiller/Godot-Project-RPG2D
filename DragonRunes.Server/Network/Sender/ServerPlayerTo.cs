
using DragonRunes.Network.Packet.Server;
using DragonRunes.Network.CustomDataSerializable;
using LiteNetLib;

namespace DragonRunes.Server.Network
{
    public partial class ServerPacketProcessor
    {
        public void ServerPlayerToAllBut(NetPeer netPeer, PlayerDataModel playerDataModel)
        {
            var packet = new SPlayerData();

            packet.PlayerDataModels = new List<PlayerDataModel> { playerDataModel };

            SendDataToAllBut(netPeer, packet, DeliveryMethod.ReliableUnordered);
        }
    }
}
