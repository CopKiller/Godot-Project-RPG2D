

namespace GdProject.Network.Packet.Client
{
    internal class CNewChar : ISend
    {
        public string Name { get; set; }

        public void WritePacket(PacketProcessor packetProcessor)
        {
            
        }

    }
}
