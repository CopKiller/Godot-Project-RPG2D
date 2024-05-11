using GdProject.Infrastructure;
using GdProject.Logger;
using GdProject.Model;
using GdProject.Network.Extensions;
using LiteNetLib;
using LiteNetLib.Utils;
using Network.Packet;
using System;

namespace GdProject.Network
{
    public class PacketProcessor : NetPacketProcessor
    {
        public PacketProcessor()
        {
            // Register custom types
            RegisterCustomTypes();

            // Register to receive packets  
            SubscribePacket();
        }

        internal void RegisterCustomTypes()
        {
            // Register Types Of Serializations
            this.RegisterNestedType<PlayerDataModel>(() => { return new PlayerDataModel(); });
            this.RegisterNestedType<PlayerMoveModel>(() => { return new PlayerMoveModel(); });
            this.RegisterNestedType<PlayerPhysicModel>(() => { return new PlayerPhysicModel(); });
            this.RegisterNestedType(NetExtensions.SerializeVector2, NetExtensions.DeserializeVector2);
        }

        internal void SubscribePacket()
        {
            // Register to receive packets  
            Subscribe<SPeersAll>(ProcessPacketReceive);
            Subscribe<SPlayerData>(ProcessPacketReceive);
            Subscribe<CPlayerMoveAction>(ProcessPacketReceive);
            Subscribe<SNewChar>(ProcessPacketReceive);
            Subscribe<SLeft>(ProcessPacketReceive);
            Subscribe<SAlertMsg>(ProcessPacketReceive);
        }

        internal void Subscribe<T>(Action<T, NetPeer> onReceive) where T : class, new()
        {
            this.SubscribeReusable(onReceive);
        }

        private void ProcessPacketReceive(IRecv obj, NetPeer netPeer)
        {
            obj.ReadPacket(netPeer.Id);
        }

        public void SendDataToServer<T>(T packet, DeliveryMethod deliveryMethod = DeliveryMethod.ReliableOrdered) where T : class, new()
        {

            // Preciso de uma referencia do NetPeer do servidor
            Send(ClientManager.LocalPlayer.CurrentPeer, packet, deliveryMethod);
        }
    }
}
