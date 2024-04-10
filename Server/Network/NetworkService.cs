using LiteNetLib;
using LiteNetLib.Utils;
using Server.Network.Interface;
using Server.Logger;

namespace Server.Network;
internal abstract class NetworkService : IService
{
    public PacketProcessor NetPacketProcessor;
    protected NetManager? NetManager;
    protected EventBasedNetListener listener;

    /// <summary>
    /// Initializes a new instance of the <see cref="NetworkService"/> class.
    /// </summary>
    internal NetworkService(){ }

    /// <inheritdoc />
    public virtual void Register()
    {
        NetDebug.Logger = new ExternalLogger();

        this.listener = new EventBasedNetListener();
        this.NetManager = new NetManager(this.listener)
        {
            AutoRecycle = true,
            UseNativeSockets = true,
        };
    }

    /// <inheritdoc />
    public virtual void Unregister()
    {
        this.Stop();
    }

    /// <inheritdoc />
    public virtual void Update()
    {
        this.NetManager?.PollEvents();
    }

    /// <inheritdoc />
    private void Stop()
    {
        this.NetManager?.Stop();
        ExternalLogger.Print("Shutdown.");
    }

    ///// <summary>
    ///// Send an message to an specific client (non serialized)
    ///// </summary>
    ///// <param name="peerId"></param>
    ///// <param name="obj"></param>
    ///// <param name="method"></param>
    ///// <typeparam name="T"></typeparam>
    //internal void SendMessage<T>(int peerId, T obj, DeliveryMethod method = DeliveryMethod.ReliableOrdered) where T : class, new()
    //{
    //    if (this.NetManager == null)
    //    {
    //        return;
    //    }

    //    var peer = this.NetManager.GetPeerById(peerId);
    //    if (peer != null)
    //    {
    //        this.NetPacketProcessor.Send(peer, obj, method);
    //    }
    //}

    ///// <summary>
    ///// Send an network message to an specific client by id (serialized)
    ///// </summary>
    ///// <param name="peerId"></param>
    ///// <param name="command"></param>
    ///// <param name="method"></param>
    ///// <typeparam name="T"></typeparam>
    //internal void SendMessageSerialisable<T>(int peerId, T command, DeliveryMethod method = DeliveryMethod.ReliableOrdered) where T : INetSerializable, new()
    //{
    //    if (this.NetManager == null)
    //    {
    //        return;
    //    }

    //    var peer = this.NetManager.GetPeerById(peerId);
    //    if (peer != null)
    //    {
    //        this.NetPacketProcessor.SendNetSerializable(peer, ref command, method);
    //    }
    //}

    ///// <summary>
    ///// Send an network message to all clients
    ///// </summary>
    ///// <typeparam name="T"></typeparam>
    //internal void SentMessageToAll<T>(T obj, DeliveryMethod method = DeliveryMethod.ReliableOrdered) where T : class, new()
    //{
    //    if (this.NetManager == null)
    //    {
    //        return;
    //    }

    //    this.NetPacketProcessor.Send(this.NetManager, obj, method);
    //}

    //internal void SentMessageToAll<T>(T obj, DeliveryMethod method,
    //    NetPeer netPeer) where T : class, new()
    //{
    //    if (this.NetManager == null)
    //    {
    //        return;
    //    }

    //    this.NetPacketProcessor.Send(this.NetManager, obj, method, netPeer);
    //}

    ///// <summary>
    ///// Send an network message to all clients
    ///// </summary>
    ///// <typeparam name="T"></typeparam>
    //internal void SentMessageToAllSerialized<T>(T obj, DeliveryMethod method = DeliveryMethod.ReliableOrdered) where T : INetSerializable, new()
    //{
    //    if (this.NetManager == null)
    //    {
    //        return;
    //    }

    //    this.NetPacketProcessor.SendNetSerializable(this.NetManager, ref obj, method);
    //}

    //internal void SentMessageToAllSerialized<T>(T obj, DeliveryMethod method,
    //    NetPeer netPeer) where T : INetSerializable, new()
    //{
    //    if (this.NetManager == null)
    //    {
    //        return;
    //    }
    //    this.NetPacketProcessor.SendNetSerializable(this.NetManager, ref obj, method, netPeer);
    //}

    ///// <summary>
    ///// Subscribe an network command from type class
    ///// </summary>
    ///// <typeparam name="T"></typeparam>
    //internal void Subscribe<T>(Action<T, NetPeer> onReceive) where T : class, new()
    //{
    //    this.NetPacketProcessor.SubscribeReusable(onReceive);
    //}

    ///// <summary>
    ///// Subscribe an network command from type INetSerializable
    ///// </summary>
    ///// <param name="onReceive"></param>
    ///// <typeparam name="T"></typeparam>
    //internal void SubscribeSerialisable<T>(Action<T, NetPeer> onReceive) where T : INetSerializable, new()
    //{
    //    this.NetPacketProcessor.SubscribeNetSerializable(onReceive);
    //}
}
