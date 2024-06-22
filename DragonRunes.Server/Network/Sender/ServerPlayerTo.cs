
using DragonRunes.Network.Packet.Server;
using DragonRunes.Network.CustomDataSerializable;
using LiteNetLib;

namespace DragonRunes.Server.Network
{
    public partial class ServerPacketProcessor
    {
        public void ServerPlayerToAllBut(NetPeer netPeer)
        {
            var packet = new SPlayerData();

            var playerData = _players.GetItem(netPeer.Id);

            var playerDataModel = playerData._playerData;

            packet.PlayerDataModels = new List<PlayerDataModel> { playerDataModel };

            SendDataToAllBut(netPeer, packet, DeliveryMethod.ReliableUnordered);
        }
    }
}
