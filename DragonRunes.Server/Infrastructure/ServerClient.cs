using LiteNetLib;
using Server.Model;

namespace DragonRunes.Server.Infrastructure
{
    public class ServerClient
    {
        public event Action<int> OnDisconnect;

        public NetPeer _peer { get; set; }

        public GameState GameState { get; set; } = GameState.InMenu;

        public PlayerDataModel _playerData { get; set; }

        public PlayerPhysicModel _playerPhysic { get; set; }

        public ServerClient() { }

        public ServerClient(NetPeer netPeer)
        {
            _playerData = new PlayerDataModel();
            _playerPhysic = new PlayerPhysicModel();
            _peer = netPeer;

            _playerData.Index = _peer.Id;
        }

        public void Disconnect()
        {
            _peer.Disconnect();
            OnDisconnect?.Invoke(_peer.Id);
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
