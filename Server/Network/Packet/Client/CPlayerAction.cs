using LiteNetLib;
using Server.Infrastructure;
using Server.Network;
using SharedLibrary.DataType;
using SharedLibrary.Extensions;

namespace Network.Packet
{
    internal class CPlayerAction : IRecv, ISend
    {
        public int PlayerId { get; set; }
        public PlayerActionType ActionType { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Direction { get; set; }
        public float Speed { get; set; }
        public bool Running { get; set; }

        public void ReadPacket(DictionaryWrapper<int, ServerClient> players,
            PacketProcessor netPacketProcessor, int peerId)
        {
            if (players.ContainsKey(peerId))
            {
                var player = players.GetItem(peerId);

                if (player.GameState != GameState.InGame) { return; }

                switch (ActionType)
                {
                    case PlayerActionType.Move:
                        players.GetItem(peerId)._playerData.Position = Position;
                        PlayerId = peerId;

                        WritePacket(netPacketProcessor, player._peer);
                        break;
                    case PlayerActionType.Stop:
                        players.GetItem(peerId)._playerData.Position = Position;
                        PlayerId = peerId;

                        WritePacket(netPacketProcessor, player._peer);
                        break;
                    default:
                        break;
                }
            }
        }

        public void WritePacket(PacketProcessor netPacketProcessor, NetPeer netPeer)
        {
            netPacketProcessor.SendDataToAllBut(this, netPeer.Id, DeliveryMethod.ReliableSequenced);
        }
    }

    public enum PlayerActionType : byte
    {
        Move,
        Stop
    }

}
