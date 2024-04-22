
using LiteNetLib;

namespace Network.Packet
{
    internal interface ISend
    {
        void WritePacket(Server.Network.PacketProcessor netPacketProcessor, NetPeer netPeer);
    }
}
