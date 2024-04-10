
using LiteNetLib;
using LiteNetLib.Utils;

namespace GdProject.Network.Packet
{
    internal interface ISend
    {
        void WritePacket(PacketProcessor netPacketProcessor);
    }
}
