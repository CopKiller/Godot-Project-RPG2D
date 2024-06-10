
using DragonRunes.Network;
using DragonRunes.Network.Packet.Client;

namespace DragonRunes.Server.Network
{
    public partial class ServerPacketProcessor : PacketProcessor
    {
        public void Initialize()
        {
            this.RegisterCustomTypes();

            SubscribePacket();
        }

        public override void SubscribePacket()
        {
            // Register to receive packets  
            this.Subscribe<CLogin>(Login);
            this.Subscribe<CRegister>(Register);
            this.Subscribe<CNewChar>(NewChar);
            this.Subscribe<CPlayerMove>(PlayerMove);
        }

        //public void SendDataToAllBut<T>(T packet, int excludePeerId, DeliveryMethod deliveryMethod = DeliveryMethod.ReliableOrdered) where T : class, new()
        //{
        //    var excludePeer = _players.GetItem(excludePeerId);

        //    var allPlayers = _players.GetItems();
        //    foreach (var player in allPlayers)
        //    {
        //        if (player.Value._peer.Id != excludePeerId)
        //        {
        //            if (player.Value.GameState == GameState.InGame)
        //            {
        //                this.Send(player.Value._peer, packet, deliveryMethod);
        //            }
        //        }
        //    }
        //}

        //public void SendDataToAll<T>(T packet, DeliveryMethod deliveryMethod = DeliveryMethod.ReliableOrdered) where T : class, new()
        //{
        //    var allPlayers = _players.GetItems();
        //    foreach (var player in allPlayers)
        //    {
        //        if (player.Value.GameState == GameState.InGame)
        //        {
        //            this.Send(player.Value._peer, packet, deliveryMethod);
        //        }
        //    }
        //}

        //public virtual void SendDataTo<T>( T packet, int peerId, DeliveryMethod deliveryMethod = DeliveryMethod.ReliableOrdered) where T : class, new()
        //{
        //    //var peer = _players.GetItem(peerId)._peer;
        //    this.Send(peer, packet, deliveryMethod);
        //}
    }
}
