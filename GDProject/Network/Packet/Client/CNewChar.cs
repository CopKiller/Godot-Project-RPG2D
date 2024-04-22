
using GdProject.Network;
using LiteNetLib;

namespace Network.Packet
{
    internal class CNewChar : ISend
    {
        public string Name { get; set; }

        public void WritePacket(PacketProcessor packetProcessor)
        {
            packetProcessor.SendDataToServer(this, DeliveryMethod.ReliableSequenced);
        }

    }
}
