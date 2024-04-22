
using GdProject.Network;

namespace Network.Packet
{
    internal interface ISend
    {
        void WritePacket(PacketProcessor netPacketProcessor);
    }
}
