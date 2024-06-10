
using DragonRunes.Logger;
using DragonRunes.Network;
using DragonRunes.Server.Infrastructure;
using DragonRunes.Shared;
using LiteNetLib;
using LiteNetLib.Utils;
using System.Net;
using System.Net.Sockets;

namespace DragonRunes.Server.Network;
internal class ServerNetworkService : NetworkService
{
    public static DictionaryWrapper<int, ServerClient>? _players;

    public ServerPacketProcessor? _serverPacketProcessor { get; private set; }

    public event Action<NetPeer> PlayerAccepted;

    public override void Register()
    {
        base.Register();

        _serverPacketProcessor = new ServerPacketProcessor();
        _serverPacketProcessor.Initialize();

        _players = new DictionaryWrapper<int, ServerClient>();

        // Server is true, client is false
        this.NetManager.UseNativeSockets = true;

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

    public override void Start()
    {
        var port = NetworkAddress.ServerPort;

        Logg.Logger.Log("Server: Try to start on port " + port);
        var result = this.NetManager.Start(port);
        if (result)
        {
            Logg.Logger.Log("Server: Bind on port " + port);
        }
    }

    public override void Unregister()
    {
        this.listener.PeerConnectedEvent -= OnPeerConnectedEvent;
        this.listener.PeerDisconnectedEvent -= OnPeerDisconnectedEvent;
        this.listener.NetworkErrorEvent -= OnNetworkErrorEvent;
        this.listener.NetworkReceiveEvent -= OnNetworkReceiveEvent;
        this.listener.NetworkReceiveUnconnectedEvent -= OnNetworkReceiveUnconnectedEvent;
        this.listener.NetworkLatencyUpdateEvent -= OnNetworkLatencyUpdateEvent;
        this.listener.ConnectionRequestEvent -= OnConnectionRequestEvent;
        this.listener.DeliveryEvent -= OnDeliveryEvent;
        this.listener.NtpResponseEvent -= OnNtpResponseEvent;

        _serverPacketProcessor?.ClearSubscriptions();
        _serverPacketProcessor = null;

        _players = null;

        base.Unregister();
    }

    private void OnPeerConnectedEvent(NetPeer peer)
    {
        PlayerAccepted?.Invoke(peer);
    }
    private void OnPeerDisconnectedEvent(NetPeer peer, DisconnectInfo disconnectInfo)
    {
        _players.GetItem(peer.Id).Disconnect();
    }
    private void OnNetworkErrorEvent(IPEndPoint endPoint, SocketError socketError)
    {
    }
    private void OnNetworkReceiveEvent(NetPeer peer, NetPacketReader reader, byte channel, DeliveryMethod deliveryMethod)
    {
        _serverPacketProcessor?.ReadAllPackets(reader, peer);
    }
    private void OnNetworkReceiveUnconnectedEvent(IPEndPoint remoteEndPoint, NetPacketReader reader, UnconnectedMessageType messageType)
    {
    }
    private void OnNetworkLatencyUpdateEvent(NetPeer peer, int latency)
    {
    }
    private void OnConnectionRequestEvent(ConnectionRequest request)
    {
        if (NetManager?.ConnectedPeersCount < 10)
        { /* max connections */
            request.AcceptIfKey(NetworkAddress.SecureConnectionKey);
            Logg.Logger.Log("Server: Client connected: " + request.RemoteEndPoint.Address);
        }
        else
        {
            request.Reject();/* reject connection */
        }
    }
    private void OnDeliveryEvent(NetPeer peer, object userData)
    {
    }
    private void OnNtpResponseEvent(NtpPacket packet)
    {
    }
}
