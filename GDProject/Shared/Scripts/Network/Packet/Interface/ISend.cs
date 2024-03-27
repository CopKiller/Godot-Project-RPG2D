using LiteNetLib.Utils;

namespace GdProject.Shared.Scripts.Network.Packet.Interface
{
    public interface ISend : INetSerializable
    {
        void Send();

    }
}
