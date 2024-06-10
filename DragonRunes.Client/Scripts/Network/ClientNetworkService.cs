using DragonRunes.Network;
using LiteNetLib;
using LiteNetLib.Utils;
using System;
using System.Net;
using System.Net.Sockets;

namespace DragonRunes.Scripts.Network;
public class ClientNetworkService : NetworkService
{
    public event Action<NetPeer> RemotePeerConnectedEvent;

    public event Action<NetPeer> CurrentPeerConnectedEvent;

    public event Action<NetPeer, DisconnectInfo> RemotePeerDisconnectedEvent;

    public event Action<NetPeer> CurrentPeerDisconnectedEvent;

    public event Action<int> LatencyUpdatedEvent;

    public ClientPacketProcessor _clientPacketProcessor { get; private set; }

    /// <inheritdoc />
    public override void Register()
    {
        base.Register();

        // Server is true, client is false
        this.NetManager.UseNativeSockets = false;

        _clientPacketProcessor = new ClientPacketProcessor();

        _clientPacketProcessor.Initialize();

        listener.PeerConnectedEvent += OnPeerConnectedEvent;
        listener.PeerDisconnectedEvent += OnPeerDisconnectedEvent;
        listener.NetworkErrorEvent += OnNetworkErrorEvent;
        listener.NetworkReceiveEvent += OnNetworkReceiveEvent;
        listener.NetworkReceiveUnconnectedEvent += OnNetworkReceiveUnconnectedEvent;
        listener.NetworkLatencyUpdateEvent += OnNetworkLatencyUpdateEvent;
        listener.ConnectionRequestEvent += OnConnectionRequestEvent;
        listener.DeliveryEvent += OnDeliveryEvent;
        listener.NtpResponseEvent += OnNtpResponseEvent;
    }

    /// <inheritdoc />
    public override void Start()
    {
        base.Start();

        //if (NetManager.IsRunning)
        //ExternalLogger.Print("Client: Started client");
        //else
        //ExternalLogger.Print("Client: Failed to start client");

        var CurrentPeer = NetManager.Connect(NetworkAddress.ServerAddress, NetworkAddress.ServerPort, NetworkAddress.SecureConnectionKey);

        if (CurrentPeer == null)
        {
            //ExternalLogger.Print("Client: Failed to connect to server");
            return;
        }
        //else
        //ExternalLogger.Print("Client: Connected to server");

        CurrentPeerConnectedEvent?.Invoke(CurrentPeer);
    }

    public override void Unregister()
    {
        listener.PeerConnectedEvent -= OnPeerConnectedEvent;
        listener.PeerDisconnectedEvent -= OnPeerDisconnectedEvent;
        listener.NetworkErrorEvent -= OnNetworkErrorEvent;
        listener.NetworkReceiveEvent -= OnNetworkReceiveEvent;
        listener.NetworkReceiveUnconnectedEvent -= OnNetworkReceiveUnconnectedEvent;
        listener.NetworkLatencyUpdateEvent -= OnNetworkLatencyUpdateEvent;
        listener.ConnectionRequestEvent -= OnConnectionRequestEvent;
        listener.DeliveryEvent -= OnDeliveryEvent;
        listener.NtpResponseEvent -= OnNtpResponseEvent;

        _clientPacketProcessor.ClearSubscriptions();
        _clientPacketProcessor = null;

        base.Unregister();
    }

    private void OnPeerConnectedEvent(NetPeer peer)
    {
        RemotePeerConnectedEvent?.Invoke(peer);
    }

    private void OnPeerDisconnectedEvent(NetPeer peer, DisconnectInfo disconnectInfo)
    {
        RemotePeerDisconnectedEvent?.Invoke(peer, disconnectInfo);
    }

    private void OnNetworkErrorEvent(IPEndPoint endPoint, SocketError socketError)
    {

    }

    private void OnNetworkReceiveEvent(NetPeer peer, NetPacketReader reader, byte channel, DeliveryMethod deliveryMethod)
    {
        _clientPacketProcessor.ReadAllPackets(reader, peer);
    }

    private void OnNetworkReceiveUnconnectedEvent(IPEndPoint remoteEndPoint, NetPacketReader reader, UnconnectedMessageType messageType)
    {

    }

    private void OnNetworkLatencyUpdateEvent(NetPeer peer, int latency)
    {
        LatencyUpdatedEvent?.Invoke(latency);
    }

    private void OnConnectionRequestEvent(ConnectionRequest request)
    {

    }

    private void OnDeliveryEvent(NetPeer peer, object userData)
    {

    }

    private void OnNtpResponseEvent(NtpPacket packet)
    {

    }
}
