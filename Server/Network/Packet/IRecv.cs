
using LiteNetLib;
using LiteNetLib.Utils;
using Server.Infrastructure;
using SharedLibrary.Extensions;

namespace Server.Network.Packet
{
    internal interface IRecv
    {
        public void ReadPacket(DictionaryWrapper<int, ServerClient> players, PacketProcessor netPacketProcessor, int peerId);
    }
}
