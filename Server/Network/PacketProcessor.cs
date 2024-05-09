using LiteNetLib;
using LiteNetLib.Utils;
using Network.Packet;
using Server.Infrastructure;
using Server.Model;
using Server.Network.Extensions;
using SharedLibrary.Extensions;

namespace Server.Network
{
    public class PacketProcessor : NetPacketProcessor
    {
        private readonly DictionaryWrapper<int, ServerClient> _players;
        public PacketProcessor(DictionaryWrapper<int, ServerClient> players)
        {
            // Register custom types
            RegisterCustomTypes();

            // Register to receive packets
            Subscribe<CPlayerMoveAction>(ProcessPacketReceive);
            Subscribe<CLogin>(ProcessPacketReceive);
            Subscribe<CNewAccount>(ProcessPacketReceive);
            Subscribe<CNewChar>(ProcessPacketReceive);

            _players = players;
        }

        internal void RegisterCustomTypes()
        {
            // Register Types Of Serializations
            this.RegisterNestedType<PlayerDataModel>(() => { return new PlayerDataModel(); });
            this.RegisterNestedType<PlayerMoveModel>(() => { return new PlayerMoveModel(); });
            this.RegisterNestedType<PlayerPhysicModel>(() => { return new PlayerPhysicModel(); });
            this.RegisterNestedType(NetExtensions.SerializeVector2, NetExtensions.DeserializeVector2);
        }

        internal void Subscribe<T>(Action<T, NetPeer> onReceive) where T : class, new()
        {
            this.SubscribeReusable(onReceive);
        }

        private void ProcessPacketReceive(IRecv obj, NetPeer netPeer)
        {
            obj.ReadPacket(_players, this, netPeer.Id);
        }

        public void SendDataToAllBut<T>(T packet, int excludePeerId, DeliveryMethod deliveryMethod = DeliveryMethod.ReliableOrdered) where T : class, new()
        {
            var excludePeer = _players.GetItem(excludePeerId);

            var allPlayers = _players.GetItems();
            foreach (var player in allPlayers)
            {
                if (player.Value._peer.Id != excludePeerId)
                {
                    if (player.Value.GameState == GameState.InGame)
                    {
                        this.Send(player.Value._peer, packet, deliveryMethod);
                    }
                }
            }
        }

        public void SendDataToAll<T>(T packet, DeliveryMethod deliveryMethod = DeliveryMethod.ReliableOrdered) where T : class, new()
        {
            var allPlayers = _players.GetItems();
            foreach (var player in allPlayers)
            {
                if (player.Value.GameState == GameState.InGame)
                {
                    this.Send(player.Value._peer, packet, deliveryMethod);
                }
            }
        }

        public void SendDataTo<T>(T packet, int peerId, DeliveryMethod deliveryMethod = DeliveryMethod.ReliableOrdered) where T : class, new()
        {
            var peer = _players.GetItem(peerId)._peer;
            this.Send(peer, packet, deliveryMethod);
        }
    }
}
