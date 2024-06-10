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
            this.Subscribe<SPlayerData>(PlayerData);
            this.Subscribe<SPlayerMove>(PlayerMove);
            this.Subscribe<SNewChar>(NewChar);
            this.Subscribe<SLeft>(Left);
            this.Subscribe<SAlertMsg>(AlertMsg);
        }

        public override void SendDataTo<T>(NetPeer playerPeer, T packet, DeliveryMethod deliveryMethod = DeliveryMethod.ReliableOrdered)
        {
            base.SendDataTo(playerPeer, packet, deliveryMethod);
        }
    }
}
