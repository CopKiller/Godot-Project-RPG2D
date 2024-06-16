
using DragonRunes.Client.Scripts;
using DragonRunes.Network.Packet.Client;
using LiteNetLib;

namespace DragonRunes.Scripts.Network
{
    public partial class ClientPacketProcessor
    {
        public void SendNewChar(NetPeer netPeer, string Name, Gender gender)
        {
            var packet = new CNewChar
            {
                Name = Name,
                Gender = gender,
            };

            SendDataTo(netPeer, packet, DeliveryMethod.ReliableOrdered);
        }
    }
}
