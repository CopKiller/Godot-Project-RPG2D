
using SharedLibrary.DataType;
using SharedLibrary.Extensions;

namespace GdProject.Network.Packet.Client
{
    internal class CLogin : ISend
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public void WritePacket(PacketProcessor packetProcessor)
        {
            
        }
    }
}
