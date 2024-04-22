
using GdProject.Network;
using LiteNetLib;

namespace Network.Packet
{
    internal class CNewAccount : ISend
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public void WritePacket(PacketProcessor packetProcessor)
        {
            packetProcessor.SendDataToServer(this, DeliveryMethod.ReliableSequenced);
        }
    }
}
