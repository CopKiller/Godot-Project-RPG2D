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
            this.RegisterNestedType(NetExtensions.SerializeVector2, NetExtensions.DeserializeVector2);
        }

        internal void SubscribePacket()
        {
            // Register to receive packets  
            Subscribe<SPeersAll>(ProcessPacketReceive);
            Subscribe<SPlayerData>(ProcessPacketReceive);
            Subscribe<CPlayerAction>(ProcessPacketReceive);
            Subscribe<SNewChar>(ProcessPacketReceive);
            Subscribe<SLeft>(ProcessPacketReceive);
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
            Send(InitClient.LocalPlayer.CurrentPeer, packet, deliveryMethod);

            // log
            //ExternalLogger.Print($"SendDataToServer: {packet.GetType().Name}");
        }
    }
}
