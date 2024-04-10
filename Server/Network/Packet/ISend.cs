
using LiteNetLib;
using LiteNetLib.Utils;

namespace Server.Network.Packet
{
    internal interface ISend
    {
        void WritePacket(PacketProcessor netPacketProcessor, NetPeer netPeer);
    }
}
