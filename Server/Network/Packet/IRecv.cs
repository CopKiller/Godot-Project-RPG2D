

using Server.Infrastructure;
using Server.Network;
using SharedLibrary.Extensions;

namespace Network.Packet
{
    internal interface IRecv
    {
        public void ReadPacket(DictionaryWrapper<int, ServerClient> players, PacketProcessor netPacketProcessor, int peerId);
    }
}
