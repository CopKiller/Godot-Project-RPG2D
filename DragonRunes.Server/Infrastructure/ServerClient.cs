using LiteNetLib;
using DragonRunes.Server.Network;
using DragonRunes.Shared.CustomDataSerializable;

namespace DragonRunes.Server.Infrastructure
{
    public class ServerClient
    {
        public NetPeer _peer { get; set; }

        public GameState GameState { get; set; }

        public PlayerDataModel _playerData { get; set; }

        public readonly ServerPacketProcessor _serverPacketProcessor;

        public ServerClient() { }

        public ServerClient(NetPeer netPeer, ServerPacketProcessor? serverPacketProcessor)
        {
            _serverPacketProcessor = serverPacketProcessor;

            _playerData = new PlayerDataModel();

            _peer = netPeer;

            _playerData.Index = _peer.Id;

            GameState = GameState.InLogin;
        }

        public void Disconnect()
        {
            _serverPacketProcessor.ServerLeft(_peer);
            GameState = GameState.Disconnect;
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
