using LiteNetLib.Utils;
using Server.Network.Packet;
using Server.Network.Packet.Client;
using LiteNetLib;
using Server.Infrastructure;
using SharedLibrary.Extensions;
using Server.Model;
using Server.Network.Extensions;

namespace Server.Network
{
    internal class PacketProcessor : NetPacketProcessor
    {
        private readonly DictionaryWrapper<int, ServerClient> _players;
        public PacketProcessor(DictionaryWrapper<int, ServerClient> players)
        {
            // Register custom types
            RegisterCustomTypes();

            // Register to receive packets
            Subscribe<CPlayerAction>(ProcessPacketReceive);
            Subscribe<CLogin>(ProcessPacketReceive);
            Subscribe<CNewAccount>(ProcessPacketReceive);
            Subscribe<CNewChar>(ProcessPacketReceive);

            _players = players;
        }

        internal void RegisterCustomTypes()
        {
            // Register Types Of Serializations
            this.RegisterNestedType<PlayerDataModel>(() => { return new PlayerDataModel(); });
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

        public void SendDataToAllBut<T>(T packet,int excludePeerId, DeliveryMethod deliveryMethod = DeliveryMethod.ReliableOrdered) where T : class, new()
        {
            var excludePeer = _players.GetItem(excludePeerId);

            var allPlayers = _players.GetItems();
            foreach (var player in allPlayers)
            {
                if (player.Value._peer.Id != excludePeerId)
                {
                    if (player.Value._playerData.GameState == GameState.InGame)
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
                if (player.Value._playerData.GameState == GameState.InGame)
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
