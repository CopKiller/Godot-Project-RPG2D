using DragonRunes.Network.Packet.Client;
using LiteNetLib;
using DragonRunes.Network.Packet.Server;
using DragonRunes.Logger;

namespace DragonRunes.Server.Network
{
    public partial class ServerPacketProcessor
    {
        public void ClientPlayerMove(CPlayerMove obj, NetPeer netPeer)
        {

            var player = _players.GetItem(netPeer.Id);
            if (player == null) { return; }

            obj.PlayerMoveModel.Index = netPeer.Id;

            //Logg.Logger.Log("PlayerMoveModel: Recebido" + obj.PlayerMoveModel.Index.ToString());
            //Logg.Logger.Log($"Other X: {obj.PlayerMoveModel.Position.X} Other Y: {obj.PlayerMoveModel.Position.Y}");

            player._playerData.Position = obj.PlayerMoveModel.Position;
            player._playerData.Direction = obj.PlayerMoveModel.Direction;

            //Logg.Logger.Log($"New X: {obj.PlayerMoveModel.Position.X} New Y: {obj.PlayerMoveModel.Position.Y}");

            var packet = new SPlayerMove
            {
                PlayerMoveModel = obj.PlayerMoveModel
            };

            SendDataToAllBut(netPeer, packet, DeliveryMethod.ReliableSequenced);
        }
    }
}
