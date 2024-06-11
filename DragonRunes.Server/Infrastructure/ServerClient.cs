using LiteNetLib;
using DragonRunes.Network.CustomData;
using DragonRunes.Server.Network;
using DragonRunes.Network.Packet.Server;
using DragonRunes.Network;

namespace DragonRunes.Server.Infrastructure
{
    public class ServerClient
    {
        public NetPeer _peer { get; set; }

        public GameState GameState { get; set; }

        public PlayerDataModel _playerData { get; set; }

        private readonly ServerPacketProcessor _serverPacketProcessor;

        public ServerClient() { }

        public ServerClient(NetPeer netPeer, ServerPacketProcessor? serverPacketProcessor)
        {
            _serverPacketProcessor = serverPacketProcessor;

            _playerData = new PlayerDataModel();

            _peer = netPeer;

            _playerData.Index = _peer.Id;
        }

        public void Disconnect()
        {
            _serverPacketProcessor.ServerLeft(_peer);

            _peer.Disconnect();
        }

        //public void SendDataToClient(NetDataWriter writer)
        //{
        //    _peer.Send(writer, DeliveryMethod.ReliableOrdered);
        //}

        //public void SendDataToClient(NetDataWriter writer, DeliveryMethod deliveryMethod)
        //{
        //    _peer.Send(writer, deliveryMethod);
        //}

        //public void SendDataToClient(NetDataWriter writer, DeliveryMethod deliveryMethod, byte channel)
        //{
        //    _peer.Send(writer, channel, deliveryMethod);
        //}
    }
}
