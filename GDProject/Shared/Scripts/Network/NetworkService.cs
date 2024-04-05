using System;
using System.Collections.Generic;
using GdProject.Shared.Scripts.Network.Packet.Client;
using LiteNetLib;
using LiteNetLib.Utils;
using GdProject.Shared.Scripts.Network.Extensions;
using GdProject.Shared.Scripts.Entities.Player;
using GdProject.Shared.Scripts.Global;
using GDProject.Extensions;
using Godot;

namespace GdProject.Shared.Scripts.Network
{
    public partial class NetworkService : Node, IService
    {
        internal readonly NetPacketProcessor NetPacketProcessor = new();
        internal NetManager? NetManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkService"/> class.
        /// </summary>
        protected NetworkService()
        {
            // Register Types Of Serializations
            NetPacketProcessor.RegisterNestedType<PlayerDataModel>(() => { return new PlayerDataModel(); });
            NetPacketProcessor.RegisterNestedType<CPlayerAction>(() => { return new CPlayerAction(); });
            NetPacketProcessor.RegisterNestedType(NetExtensions.SerializeVector2, NetExtensions.DeserializeVector2);
        }

        /// <summary>
        /// Latency of each player
        /// </summary>
        //public Dictionary<NetPeer, int> PeerLatency { get; } = new Dictionary<NetPeer, int>();

        /// <inheritdoc />
        public virtual void Register()
        { }

        /// <inheritdoc />
        public virtual void Render(float delta)
        { }

        /// <summary>
        /// Send an message to an specific client (non serialized)
        /// </summary>
        /// <param name="peerId"></param>
        /// <param name="obj"></param>
        /// <param name="method"></param>
        /// <typeparam name="T"></typeparam>
        public void SendMessage<T>(int peerId, T obj, DeliveryMethod method = DeliveryMethod.ReliableOrdered) where T : class, new()
        {
            if (this.NetManager == null)
            {
                return;
            }

            var peer = this.NetManager.GetPeerById(peerId);
            if (peer != null)
            {
                this.NetPacketProcessor.Send(peer, obj, method);
            }
        }

        /// <summary>
        /// Send an network message to an specific client by id (serialized)
        /// </summary>
        /// <param name="peerId"></param>
        /// <param name="command"></param>
        /// <param name="method"></param>
        /// <typeparam name="T"></typeparam>
        public void SendMessageSerialisable<T>(int peerId, T command, DeliveryMethod method = DeliveryMethod.ReliableOrdered) where T : INetSerializable, new()
        {
            if (this.NetManager == null)
            {
                return;
            }

            var peer = this.NetManager.GetPeerById(peerId);
            if (peer != null)
            {
                this.NetPacketProcessor.SendNetSerializable(peer, ref command, method);
            }
        }

        /// <summary>
        /// Send an network message to all clients
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void SentMessageToAll<T>(T obj, DeliveryMethod method = DeliveryMethod.ReliableOrdered) where T : class, new()
        {
            if (this.NetManager == null)
            {
                return;
            }

            this.NetPacketProcessor.Send(this.NetManager, obj, method);
        }

        public void SentMessageToAll<T>(T obj, DeliveryMethod method,
            NetPeer netPeer) where T : class, new()
        {
            if (this.NetManager == null)
            {
                return;
            }

            this.NetPacketProcessor.Send(this.NetManager, obj, method, netPeer);
        }

        /// <summary>
        /// Send an network message to all clients
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void SentMessageToAllSerialized<T>(T obj, DeliveryMethod method = DeliveryMethod.ReliableOrdered) where T : INetSerializable, new()
        {
            if (this.NetManager == null)
            {
                return;
            }

            this.NetPacketProcessor.SendNetSerializable(this.NetManager, ref obj, method);
        }

        public void SentMessageToAllSerialized<T>(T obj, DeliveryMethod method,
            NetPeer netPeer) where T : INetSerializable, new()
        {
            if (this.NetManager == null)
            {
                return;
            }
            this.NetPacketProcessor.SendNetSerializable(this.NetManager, ref obj, method, netPeer);
        }

        /// <summary>
        /// Subscribe an network command from type class
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void Subscribe<T>(Action<T, NetPeer> onReceive) where T : class, new()
        {
            this.NetPacketProcessor.SubscribeReusable(onReceive);
        }

        /// <summary>
        /// Subscribe an network command from type INetSerializable
        /// </summary>
        /// <param name="onReceive"></param>
        /// <typeparam name="T"></typeparam>
        public void SubscribeSerialisable<T>(Action<T, NetPeer> onReceive) where T : INetSerializable, new()
        {
            this.NetPacketProcessor.SubscribeNetSerializable(onReceive);
        }

        /// <inheritdoc />
        public virtual void Unregister()
        {
            this.Stop();
        }

        /// <inheritdoc />
        public virtual void Update(float delta)
        {
            this.NetManager?.PollEvents();
        }

        /// <inheritdoc />
        private void Stop()
        {
            this.NetManager?.Stop();
            GDPrint.Print("Shutdown.");
        }
    }
}
