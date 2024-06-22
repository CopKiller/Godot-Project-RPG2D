
using DragonRunes.Client.Scripts;
using DragonRunes.Network.Packet.Client;
using LiteNetLib;

namespace DragonRunes.Scripts.Network
{
    public partial class ClientPacketProcessor
    {
        public void ClientRegister(NetPeer netPeer, string login, string password, string mail, string birthday)
        {
            var packet = new CRegister
            {
                Login = login,
                Password = password,
                Email = mail,
                BirthDate = birthday
            };

            SendDataTo(netPeer, packet, DeliveryMethod.ReliableOrdered);
        }
    }
}
