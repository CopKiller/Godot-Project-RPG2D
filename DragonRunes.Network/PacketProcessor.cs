using DragonRunes.Network.CustomData;
using DragonRunes.Network.CustomData.Extension;
using DragonRunes.Shared.CustomDataSerializable;
using DragonRunes.Shared.CustomDataSerializable.Extension;
using LiteNetLib;
using LiteNetLib.Utils;

namespace DragonRunes.Network
{
    public class PacketProcessor : NetPacketProcessor
    {
        public PacketProcessor() { }

        protected void RegisterCustomTypes()
        {
            // Register Types Of Serializations
            this.RegisterNestedType<PlayerDataModel>(() => { return new PlayerDataModel(); });
            this.RegisterNestedType<PlayerMoveModel>(() => { return new PlayerMoveModel(); });
            //this.RegisterNestedType(Vector2Extension.SerializeVector2, Vector2Extension.DeserializeVector2);
        }

        public virtual void SubscribePacket() { }

        protected void Subscribe<T>(Action<T, NetPeer> onReceive) where T : class, new()
        {
            this.SubscribeReusable(onReceive);
        }

        public virtual void SendDataToAllBut<T>(NetPeer excludePeer, T packet, DeliveryMethod deliveryMethod = DeliveryMethod.ReliableOrdered) where T : class, new()
        {
            
        }
        public virtual void SendDataToAll<T>(T packet, DeliveryMethod deliveryMethod = DeliveryMethod.ReliableOrdered) where T : class, new()
        {
            
        }
        public virtual void SendDataTo<T>(NetPeer playerPeer, T packet, DeliveryMethod deliveryMethod = DeliveryMethod.ReliableOrdered) where T : class, new()
        {
            this.Send(playerPeer, packet, deliveryMethod);
        }
    }
}
