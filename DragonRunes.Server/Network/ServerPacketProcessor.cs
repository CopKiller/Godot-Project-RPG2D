
using DragonRunes.Logger;
using DragonRunes.Network;
using DragonRunes.Network.Packet.Client;
using DragonRunes.Server.Infrastructure;
using DragonRunes.Network;
using LiteNetLib;

namespace DragonRunes.Server.Network
{
    public partial class ServerPacketProcessor : PacketProcessor
    {
        private DictionaryWrapper<int, ServerClient> _players => ServerNetworkService._players;

        public void Initialize()
        {
            this.RegisterCustomTypes();

            SubscribePacket();
        }

        public override void SubscribePacket()
        {
            // Register to receive packets  
            this.Subscribe<CLogin>(ClientLogin);
            this.Subscribe<CRegister>(ClientRegister);
            this.Subscribe<CNewChar>(ClientNewChar);
            this.Subscribe<CPlayerMove>(ClientPlayerMove);
        }

        public override void SendDataToAllBut<T>(NetPeer excludePeer, T packet, DeliveryMethod deliveryMethod = DeliveryMethod.ReliableOrdered) where T : class
        {
            var excludePeerId = excludePeer.Id;

            //Logg.Logger.Log("SendDataToAllBut: " + packet.GetType().ToString());

            var allPlayers = _players.GetItems()
                .Select(allPlayers => allPlayers.Value)
                .Where(player => player._peer.Id != excludePeerId)
                .ToList();

            foreach (var player in allPlayers)
            {
                if (player.GameState == GameState.InGame)
                {
                    SendDataTo(player._peer, packet, deliveryMethod);
                }
            }
        }
        public override void SendDataToAll<T>(T packet, DeliveryMethod deliveryMethod = DeliveryMethod.ReliableOrdered) where T : class
        {
            var allPlayers = _players.GetItems();

            //Logg.Logger.Log("SendDataToAll: " + packet.GetType().ToString());

            foreach (var player in allPlayers)
            {
                if (player.Value.GameState == GameState.InGame)
                {
                    this.Send(player.Value._peer, packet, deliveryMethod);
                }
            }
        }

        public override void SendDataTo<T>(NetPeer peer, T packet, DeliveryMethod deliveryMethod = DeliveryMethod.ReliableOrdered) where T : class
        {
            //Logg.Logger.Log("SendDataTo Index: " + peer.Id + " do tipo: " + packet.GetType().ToString());

            base.SendDataTo(peer, packet, deliveryMethod);
        }
    }
}
