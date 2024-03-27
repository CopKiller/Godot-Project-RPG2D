using Godot;
using System.Net;
using System.Net.Sockets;
using LiteNetLib;
using LiteNetLib.Utils;
using System.Threading;
using System;
using GdProject.Shared.Scripts.Network.Packet;
using GdProject.Shared.Scripts.NodeManager;

public partial class GameClient : Node
{
    private EventBasedNetListener listener = new EventBasedNetListener();
    private NetPacketProcessor processor = new NetPacketProcessor();
    private NetManager client = null;

    public GameClient()
    {
        client = new NetManager(listener);
        client.AutoRecycle = true;

        // Subscribe to recieving packets.        
        processor.SubscribeReusable<JoinGame, NetPeer>(OnJoinGameReceivePacket);
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
        client.Start();
        if (client.IsRunning)
            GD.Print("Client: client started on port: ", client.LocalPort);
        else
            GD.Print("Client: Failed to start client");

        client.Connect(TransportConfigs.ServerAddress, TransportConfigs.ServerPort, TransportConfigs.SecureConnectionKey);
    }

    public override void _Process(double delta)
    {
        client.PollEvents();
        Thread.Sleep(15);
    }

    private void OnPeerConnectedEvent(NetPeer peer)
    {
        GD.Print("Client: OnPeerConnectedEvent");
    }

    private void OnPeerDisconnectedEvent(NetPeer peer, DisconnectInfo disconnectInfo)
    {
        GD.Print("Client: OnPeerDisconnectedEvent");
    }

    private void OnNetworkErrorEvent(IPEndPoint endPoint, SocketError socketError)
    {
        GD.Print("Client: OnNetworkErrorEvent");
    }

    private void OnNetworkReceiveEvent(NetPeer peer, NetPacketReader reader, byte channel, DeliveryMethod deliveryMethod)
    {
        GD.Print("Client: received data. Processing...");
        // Deserializes packet and calls the handler registered in constructor
        processor.ReadAllPackets(reader, peer);
    }

    private void OnNetworkReceiveUnconnectedEvent(IPEndPoint remoteEndPoint, NetPacketReader reader, UnconnectedMessageType messageType)
    {
        GD.Print("Client: OnNetworkReceiveUnconnectedEvent");
    }

    private void OnNetworkLatencyUpdateEvent(NetPeer peer, int latency)
    {
        //GD.Print("Client: OnNetworkLatencyUpdateEvent", latency);
    }

    private void OnConnectionRequestEvent(ConnectionRequest request)
    {
        GD.Print("Client: OnConnectionRequestEvent");
    }

    private void OnDeliveryEvent(NetPeer peer, object userData)
    {
        GD.Print("Client: OnDeliveryEvent");
    }

    private void OnNtpResponseEvent(NtpPacket packet)
    {
        GD.Print("Client: OnNtpResponseEvent");
    }

    private void OnJoinGameReceivePacket(JoinGame packet, NetPeer peer)
    {
        GD.Print("Client: received JoinGame packet");
        GD.Print($"Client: {packet.PlayerName}");

        NodeManager.GetNode<Player>("Player").SetPlayerName(packet.PlayerName);
    }
}
