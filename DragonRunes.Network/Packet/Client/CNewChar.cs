
namespace DragonRunes.Network.Packet.Client
{
    public class CNewChar
    {
        public string Name { get; set; } = string.Empty;
        public Class Class { get; set; }
        public Gender Gender { get; set; }

        //public void WritePacket(PacketProcessor packetProcessor)
        //{
        //    packetProcessor.SendDataToServer(this, DeliveryMethod.ReliableSequenced);
        //}

    }
}
