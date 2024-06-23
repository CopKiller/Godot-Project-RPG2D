using DragonRunes.Network.Packet.Client;
using LiteNetLib;
using DragonRunes.Network.Packet.Server;

namespace DragonRunes.Server.Network
{
    public partial class ServerPacketProcessor
    {
        public void ClientPlayerMove(CPlayerMove obj, NetPeer netPeer)
        {

            var player = _players.GetItem(netPeer.Id);
            if (player == null) { return; }

            player._playerData.Position = obj.PlayerMoveModel.Position;
            player._playerData.Direction = obj.PlayerMoveModel.Direction;

            obj.PlayerMoveModel.Index = netPeer.Id;

            var packet = new SPlayerMove
            {
                PlayerMoveModel = obj.PlayerMoveModel
            };

            SendDataToAllBut(netPeer, packet, DeliveryMethod.ReliableSequenced);
        }
    }
}
