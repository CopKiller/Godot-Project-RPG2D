using LiteNetLib.Utils;
using SharedLibrary.DataType;
using Server.Infrastructure;
using SharedLibrary.Extensions;
using LiteNetLib;

namespace Server.Network.Packet.Client
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
            var player = players.GetItem(peerId);
            if (player._playerData.GameState != GameState.InGame) { return; }

            if (players.ContainsKey(peerId))
            {
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

    public enum PlayerActionType: byte
    {
        Move,
        Stop
    }

}
