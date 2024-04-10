
using LiteNetLib;
using LiteNetLib.Utils;
using SharedLibrary.Extensions;

namespace GdProject.Network.Packet.Client
{
    internal class CNewAccount : ISend
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public void WritePacket(PacketProcessor packetProcessor)
        {
            
        }
    }
}
