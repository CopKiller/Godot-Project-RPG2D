using LiteNetLib;
using LiteNetLib.Utils;
using Server.Database;
using Server.Infrastructure;
using Server.Logger;
using SharedLibrary.DataType;
using SharedLibrary.Extensions;
using SharedLibrary.Models;
using SharedLibrary.Network.Packet.Client;
using SharedLibrary.Network.Packet.Server;
using System.Net;
using System.Net.Sockets;

namespace Server.Network;
internal class ServerNetworkService : NetworkService
{
    public event Action<NetPeer> PlayerAccepted;

    public event Action<int> PlayerDisconnected;

    public readonly DictionaryWrapper<int, ServerClient> _players;

    public ProcessPackage ProcessPackage { get; set; }

    public ServerNetworkService(DictionaryWrapper<int, ServerClient> players)
    {
        _players = players;
        ProcessPackage = new ProcessPackage(_players, NetPacketProcessor);
    }

    public override void Register()

    {
        base.Register();

        // Register to receive packets
        Subscribe<CPlayerAction>(ProcessPackage.ProcessPlayerAction);
        Subscribe<CLogin>(ProcessPackage.ProcessPlayerLogin);
        Subscribe<CNewAccount>(ProcessPackage.ProcessPlayerRegister);
        Subscribe<CNewChar>(ProcessPackage.ProcessPlayerCreateChar);

        this.listener.PeerConnectedEvent += OnPeerConnectedEvent;
        listener.PeerDisconnectedEvent += OnPeerDisconnectedEvent;
        listener.NetworkErrorEvent += OnNetworkErrorEvent;
        listener.NetworkReceiveEvent += OnNetworkReceiveEvent;
        listener.NetworkReceiveUnconnectedEvent += OnNetworkReceiveUnconnectedEvent;
        listener.NetworkLatencyUpdateEvent += OnNetworkLatencyUpdateEvent;
        listener.ConnectionRequestEvent += OnConnectionRequestEvent;
        listener.DeliveryEvent += OnDeliveryEvent;
        listener.NtpResponseEvent += OnNtpResponseEvent;
    }

    public void Bind(int port)
    {
        ExternalLogger.Print("Server: Try to start on port " + port);
        var result = this.NetManager.Start(port);
        if (result)
        {
            ExternalLogger.Print("Server: Bind on port " + port);
        }
    }

    public override void Unregister()
    {
        if (this.listener is null)
        {
            return;
        }

        this.listener.PeerConnectedEvent -= OnPeerConnectedEvent;
        this.listener.PeerDisconnectedEvent -= OnPeerDisconnectedEvent;
        this.listener.NetworkErrorEvent -= OnNetworkErrorEvent;
        this.listener.NetworkReceiveEvent -= OnNetworkReceiveEvent;
        this.listener.NetworkReceiveUnconnectedEvent -= OnNetworkReceiveUnconnectedEvent;
        this.listener.NetworkLatencyUpdateEvent -= OnNetworkLatencyUpdateEvent;
        this.listener.ConnectionRequestEvent -= OnConnectionRequestEvent;
        this.listener.DeliveryEvent -= OnDeliveryEvent;
        this.listener.NtpResponseEvent -= OnNtpResponseEvent;

        base.Unregister();
    }

    private void OnPeerConnectedEvent(NetPeer peer)
    {
        PlayerAccepted?.Invoke(peer);
    }
    private void OnPeerDisconnectedEvent(NetPeer peer, DisconnectInfo disconnectInfo)
    {
        PlayerDisconnected?.Invoke(peer.Id);
    }
    private void OnNetworkErrorEvent(IPEndPoint endPoint, SocketError socketError)
    {
    }
    private void OnNetworkReceiveEvent(NetPeer peer, NetPacketReader reader, byte channel, DeliveryMethod deliveryMethod)
    {
        NetPacketProcessor.ReadAllPackets(reader, peer);
    }
    private void OnNetworkReceiveUnconnectedEvent(IPEndPoint remoteEndPoint, NetPacketReader reader, UnconnectedMessageType messageType)
    {
    }
    private void OnNetworkLatencyUpdateEvent(NetPeer peer, int latency)
    {
    }
    private void OnConnectionRequestEvent(ConnectionRequest request)
    {
        if (NetManager.ConnectedPeersCount < 10)
        { /* max connections */
            request.AcceptIfKey(Config.SecureConnectionKey);
            ExternalLogger.Print("Server: Client connected: " + request.RemoteEndPoint.Address);
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
