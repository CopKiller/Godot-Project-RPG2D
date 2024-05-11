using GdProject.Logger;
using LiteNetLib;
using LiteNetLib.Utils;
using Network.Packet;
using System;
using System.Net;
using System.Net.Sockets;

namespace GdProject.Network;
public partial class ClientNetworkService : NetworkService
{

    public event Action<NetPeer> RemotePeerConnectedEvent;

    public event Action<NetPeer> CurrentPeerConnectedEvent;

    public event Action<NetPeer, DisconnectInfo> RemotePeerDisconnectedEvent;

    public event Action<NetPeer> CurrentPeerDisconnectedEvent;

    public event Action<int> LatencyUpdatedEvent;

    public ClientNetworkService()
    {

    }

    public override void Register()
    {
        base.Register();

        listener.PeerConnectedEvent += OnPeerConnectedEvent;
        listener.PeerDisconnectedEvent += OnPeerDisconnectedEvent;
        listener.NetworkErrorEvent += OnNetworkErrorEvent;
        listener.NetworkReceiveEvent += OnNetworkReceiveEvent;
        listener.NetworkReceiveUnconnectedEvent += OnNetworkReceiveUnconnectedEvent;
        listener.NetworkLatencyUpdateEvent += OnNetworkLatencyUpdateEvent;
        listener.ConnectionRequestEvent += OnConnectionRequestEvent;
        listener.DeliveryEvent += OnDeliveryEvent;
        listener.NtpResponseEvent += OnNtpResponseEvent;

        this.NetManager.Start();
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

        base.Unregister();
    }

    public void Connect()
    {
        NetManager.Start();
        if (NetManager.IsRunning)
            ExternalLogger.Print("Client: Started client");
        else
            ExternalLogger.Print("Client: Failed to start client");

        var CurrentPeer = NetManager.Connect(Config.ServerAddress, Config.ServerPort, Config.SecureConnectionKey);

        if (CurrentPeer == null)
        {
            ExternalLogger.Print("Client: Failed to connect to server");
            return;
        }
        else
            ExternalLogger.Print("Client: Connected to server");

        CurrentPeerConnectedEvent?.Invoke(CurrentPeer);
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
        NetPacketProcessor.ReadAllPackets(reader, peer);
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

    public void SendPlayerPosition(CPlayerMoveAction playerAction)
    {
        NetPacketProcessor.Send(NetManager.FirstPeer, playerAction, DeliveryMethod.ReliableSequenced);
    }

}
