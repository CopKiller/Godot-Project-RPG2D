using LiteNetLib;

namespace DragonRunes.Network.Packet
{
    public interface IRecv
    {
        public void ReadPacket(NetPeer peer);
    }
}
