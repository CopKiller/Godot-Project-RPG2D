using Godot;
using System.Net;
using System.Net.Sockets;

using LiteNetLib;
using LiteNetLib.Utils;
using System;

public partial class GameServer : Node
{
    private EventBasedNetListener listener = new EventBasedNetListener();
    private NetPacketProcessor processor = new NetPacketProcessor();
    private NetManager server = null;

    public GameServer()
    {
        server = new NetManager(listener);
        server.AutoRecycle = true;
    }

    public override void _Ready()
    {
        listener.PeerConnectedEvent += OnPeerConnectedEvent;
        listener.PeerDisconnectedEvent += OnPeerDisconnectedEvent;
        listener.NetworkErrorEvent += OnNetworkErrorEvent;
        listener.NetworkReceiveEvent += OnNetworkReceiveEvent;
        listener.NetworkReceiveUnconnectedEvent += OnNetworkReceiveUnconnectedEvent;
        listener.NetworkLatencyUpdateEvent += OnNetworkLatencyUpdateEvent;
        listener.ConnectionRequestEvent += OnConnectionRequestEvent;
        listener.DeliveryEvent += OnDeliveryEvent;
        listener.NtpResponseEvent += OnNtpResponseEvent;

        server.Start(TransportConfigs.ServerPort);
        if (server.IsRunning)
            GD.Print("Server started on port: ", server.LocalPort);
        else
            GD.Print("Failed to start server");
    }

    public override void _Process(double delta)
    {
        server.PollEvents();
    }

    private void OnPeerConnectedEvent(NetPeer peer)
    {
        GD.Print("OnPeerConnectedEvent");
    }

    private void OnPeerDisconnectedEvent(NetPeer peer, DisconnectInfo disconnectInfo)
    {
        GD.Print("OnPeerDisconnectedEvent");
    }

    private void OnNetworkErrorEvent(IPEndPoint endPoint, SocketError socketError)
    {
        GD.Print("OnNetworkErrorEvent");
    }

    private void OnNetworkReceiveEvent(NetPeer peer, NetPacketReader reader, byte channel, DeliveryMethod deliveryMethod)
    {
        GD.Print("OnNetworkReceiveEvent");
    }

    private void OnNetworkReceiveUnconnectedEvent(IPEndPoint remoteEndPoint, NetPacketReader reader, UnconnectedMessageType messageType)
    {
        GD.Print("OnNetworkReceiveUnconnectedEvent");
    }

    private void OnNetworkLatencyUpdateEvent(NetPeer peer, int latency)
    {
        GD.Print("OnNetworkLatencyUpdateEvent");
    }

    private void OnConnectionRequestEvent(ConnectionRequest request)
    {
        GD.Print("OnConnectionRequestEvent");
        GD.Print("Client connected: " , request.RemoteEndPoint);
    }

    private void OnDeliveryEvent(NetPeer peer, object userData)
    {
        GD.Print("OnDeliveryEvent");
    }

    private void OnNtpResponseEvent(NtpPacket packet)
    {
        GD.Print("OnNtpResponseEvent");
    }
}
