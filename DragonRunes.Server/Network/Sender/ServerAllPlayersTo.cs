
using DragonRunes.Network.Packet.Server;
using DragonRunes.Shared.CustomDataSerializable;
using LiteNetLib;

namespace DragonRunes.Server.Network
{
    public partial class ServerPacketProcessor
    {
        public void ServerAllPlayerData(NetPeer netPeer)
        {
            var packet = new SPlayerData();

            packet.PlayerDataModels = _players.GetItems()
                .Where(a => a.Value.GameState == GameState.InGame)
                .Select(x => x.Value._playerData)
                .ToList();

            SendDataTo(netPeer, packet, DeliveryMethod.ReliableOrdered);
        }
    }
}
