using LiteNetLib;
using LiteNetLib.Utils;
using SharedLibrary.Models;

namespace Server.Infrastructure
{
    internal class ServerClient
    {
        public NetPeer _peer { get; set; }

        public PlayerDataModel _playerData { get; set; }

        //private NetPacketProcessor _netPacketProcessor;

        public ServerClient() { }

        public ServerClient(NetPeer netPeer)
        {
            _peer = netPeer;
            //_netPacketProcessor = netPacketProcessor;
            _playerData = new PlayerDataModel();
        }

        public void SendDataToClient(NetDataWriter writer)
        {
            _peer.Send(writer, DeliveryMethod.ReliableOrdered);
        }

        public void SendDataToClient(NetDataWriter writer, DeliveryMethod deliveryMethod)
        {
            _peer.Send(writer, deliveryMethod);
        }

        public void SendDataToClient(NetDataWriter writer, DeliveryMethod deliveryMethod, byte channel)
        {
            _peer.Send(writer, channel, deliveryMethod);
        }
    }
}
