using LiteNetLib.Utils;
using LiteNetLib;
using SharedLibrary.Extensions;
using GdProject.Network.Packet;
using System;
using GdProject.Model;
using GdProject.Network.Extensions;
using GdProject.Network.Packet.Server;
using GdProject.Network.Packet.Client;

namespace GdProject.Network
{
    public class PacketProcessor : NetPacketProcessor
    {
        public PacketProcessor(DictionaryWrapper<int, Player> players)
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

        public void SendDataTo<T>(T packet, int peerId, DeliveryMethod deliveryMethod = DeliveryMethod.ReliableOrdered) where T : class, new()
        {
            //this.Send(peer, packet, deliveryMethod);
        }
    }
}
