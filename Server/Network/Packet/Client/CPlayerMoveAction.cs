using LiteNetLib;
using Server.Infrastructure;
using Server.Model;
using Server.Network;
using SharedLibrary.Extensions;

namespace Network.Packet
{
    internal class CPlayerMoveAction : IRecv, ISend
    {
        public int Index { get; set; }

        public PlayerMoveModel PlayerMoveModel { get; set; }

        public void ReadPacket(DictionaryWrapper<int, ServerClient> players,
            PacketProcessor netPacketProcessor, int peerId)
        {
            if (players.ContainsKey(peerId))
            {
                var player = players.GetItem(peerId);

                if (player.GameState != GameState.InGame) { return; }

                this.Index = player._playerData.Index;

                player._playerPhysic.Position = PlayerMoveModel.Position;
                player._playerPhysic.Direction = PlayerMoveModel.Direction;
                player._playerPhysic.isRunning = PlayerMoveModel.isRunning;

                WritePacket(netPacketProcessor, player._peer);
            }
        }

        public void WritePacket(PacketProcessor netPacketProcessor, NetPeer netPeer)
        {
            netPacketProcessor.SendDataToAllBut(this, netPeer.Id, DeliveryMethod.ReliableSequenced);
        }
    }

}
