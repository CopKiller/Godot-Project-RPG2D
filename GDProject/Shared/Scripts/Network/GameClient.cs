using Godot;
using System.Net;
using System.Net.Sockets;
using LiteNetLib;
using LiteNetLib.Utils;

public partial class GameClient : Node
{
    private EventBasedNetListener listener = new EventBasedNetListener();
    private NetPacketProcessor processor = new NetPacketProcessor();
    private NetManager client = null;

    public GameClient()
    {
        client = new NetManager(listener);
        client.AutoRecycle = true;
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
            GD.Print("client started on port: ", client.LocalPort);
        else
            GD.Print("Failed to start client");

        client.Connect(TransportConfigs.ServerAddress, TransportConfigs.ServerPort, "");
    }

    public override void _Process(double delta)
    {
        client.PollEvents();
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
