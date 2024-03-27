using Godot;
using System.Net;
using System.Net.Sockets;

using LiteNetLib;
using LiteNetLib.Utils;
using System;
using System.Threading;
using GdProject.Shared.Scripts.Network.Packet;

public partial class GameServer : Node
{
    private EventBasedNetListener listener = new EventBasedNetListener();
    private NetPacketProcessor processor = new NetPacketProcessor();
    private NetManager server = null;

    public GameServer()
    {
        server = new NetManager(listener);
        server.AutoRecycle = true;

        processor.RegisterNestedType<JoinGame>(() => new JoinGame());
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
            GD.Print("Server: Server started on port: ", server.LocalPort);
        else
            GD.Print("Server: Failed to start server");
    }

    public override void _Process(double delta)
    {
        server.PollEvents();
        Thread.Sleep(15);
    }

    private void OnPeerConnectedEvent(NetPeer peer)
    {
        GD.Print("Server: OnPeerConnectedEvent");
        SendJoinGameRequest(peer);
    }

    private void OnPeerDisconnectedEvent(NetPeer peer, DisconnectInfo disconnectInfo)
    {
        GD.Print("Server: OnPeerDisconnectedEvent");
    }

    private void OnNetworkErrorEvent(IPEndPoint endPoint, SocketError socketError)
    {
        GD.Print("Server: OnNetworkErrorEvent");
    }

    private void OnNetworkReceiveEvent(NetPeer peer, NetPacketReader reader, byte channel, DeliveryMethod deliveryMethod)
    {
        GD.Print("Server: received data. Processing...");
        // Deserializes packet and calls the handler registered in constructor
        processor.ReadAllPackets(reader, peer);
    }

    private void OnNetworkReceiveUnconnectedEvent(IPEndPoint remoteEndPoint, NetPacketReader reader, UnconnectedMessageType messageType)
    {
        GD.Print("Server: OnNetworkReceiveUnconnectedEvent");
    }

    private void OnNetworkLatencyUpdateEvent(NetPeer peer, int latency)
    {
        //GD.Print("Server: OnNetworkLatencyUpdateEvent");

        //NetDataWriter writer = new NetDataWriter();         // Create writer class
        //writer.Put("Chama na goiaba!");                        // Put some string
        //peer.Send(writer, DeliveryMethod.ReliableOrdered);  // Send with reliability
    }

    private void OnConnectionRequestEvent(ConnectionRequest request)
    {
        if (server.ConnectedPeersCount < 10) { /* max connections */
            request.AcceptIfKey(TransportConfigs.SecureConnectionKey);
            GD.Print("Server: Client connected: ", request.RemoteEndPoint);
        }
        else
        {
            request.Reject();/* reject connection */
        }
    }

    private void OnDeliveryEvent(NetPeer peer, object userData)
    {
        GD.Print("Server: OnDeliveryEvent");
    }

    private void OnNtpResponseEvent(NtpPacket packet)
    {
        GD.Print("Server: OnNtpResponseEvent");
    }



    public void SendJoinGameRequest(NetPeer peer)
    {
        var joinGame = new JoinGame();
        joinGame.PlayerName = "Roludo";

        processor.Send(peer, joinGame, DeliveryMethod.ReliableOrdered);

        //var writer = new NetDataWriter();
        //joinGame.Serialize(writer);
        //peer.Send(writer, DeliveryMethod.ReliableOrdered);
    }


    //public void SendDataToAllPeers(string data)
    //{
    //    NetDataWriter writer = new NetDataWriter();         // Create writer class
    //    writer.Put(data);                        // Put some string
    //    server.SendToAll(writer, DeliveryMethod.ReliableOrdered);  // Send with reliability
    //}
}
