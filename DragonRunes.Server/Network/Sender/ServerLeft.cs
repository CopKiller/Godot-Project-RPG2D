
using DragonRunes.Network.Packet.Server;
using LiteNetLib;

namespace DragonRunes.Server.Network
{
    public partial class ServerPacketProcessor
    {
        public void ServerLeft(NetPeer netPeer)
        {
            var sLeft = new SLeft();
            sLeft.Index = netPeer.Id;
            SendDataToAllBut(netPeer, sLeft, DeliveryMethod.ReliableUnordered);
        }
    }
}
