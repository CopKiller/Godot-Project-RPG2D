
using DragonRunes.Client.Scripts;
using DragonRunes.Network.Packet.Client;
using LiteNetLib;

namespace DragonRunes.Scripts.Network
{
    public partial class ClientPacketProcessor
    {
        public void SendLogin(NetPeer netPeer, string login, string password)
        {
            var loginPacket = new CLogin
            {
                Login = login,
                Password = password
            };

            SendDataTo(netPeer, loginPacket, DeliveryMethod.ReliableOrdered);
        }
    }
}
