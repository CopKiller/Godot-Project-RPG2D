using DragonRunes.Network;
using DragonRunes.Network.Packet;
using DragonRunes.Network.Packet.Server;
using LiteNetLib;

namespace DragonRunes.Scripts.Network
{
    public partial class ClientPacketProcessor: PacketProcessor
    {
        public void Initialize()
        {
            this.RegisterCustomTypes();

            SubscribePacket();
        }

        public override void SubscribePacket()
        {
            // Register to receive packets  
            this.Subscribe<SPlayerData>(ServerPlayerData);
            this.Subscribe<SPlayerMove>(ServerPlayerMove);
            this.Subscribe<SNewChar>(ServerNewChar);
            this.Subscribe<SLeft>(ServerLeft);
            this.Subscribe<SAlertMsg>(ServerAlertMsg);
            this.Subscribe<SInGame>(ServerInGame);
        }

        public override void SendDataTo<T>(NetPeer playerPeer, T packet, DeliveryMethod deliveryMethod = DeliveryMethod.ReliableOrdered)
        {
            base.SendDataTo(playerPeer, packet, deliveryMethod);
        }
    }
}
