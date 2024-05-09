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

    // Receber todos os jogadores, incluindo o jogador local
    //private void ReceiveAllPlayers(SPeersAll packet, NetPeer peer)
    //{
    //    //GDLog.Print("Client: received PeersAll packet with " + packet.PlayerDataModels.Count + " players");

    //    // Clear the dictionary
    //    Players.Clear();

    //    if (packet.PlayerDataModels.Count == 0) { return; }

    //    //GDLog.Print($"peer.RemoteID: {peer.RemoteId} peer.ID: {peer.Id} | ServerPeer.RemoteID: {ServerPeer.RemoteId} ServerPeer.ID: {ServerPeer.Id}");

    //    // Add all players to game
    //    foreach (var player in packet.PlayerDataModels)
    //    {
    //        AddPlayer(player);
    //    }

    //    NodeManager.GetNode<Client.Client>("Client").InitGame();
    //}

    //private void ReceivePlayerData(SPlayerData packet, NetPeer peer)
    //{
    //    AddPlayer(packet.PlayerDataModel);
    //}

    //private void AddPlayer(PlayerDataModel playerData)
    //{
    //    //GDLog.Print("Client: Adding player: " + playerData.Index);

    //    // Create player
    //    Player player;

    //    if (playerData.Index == ServerPeer.RemoteId)
    //    {
    //        player = NodeManager.GetNode<Player>("Player");
    //        player.IsLocalPlayer = true;
    //        player.GameClient = this;
    //    }
    //    else
    //    {
    //        player = (Player)NodeManager.GetNode<Player>("Player").Duplicate();
    //        NodeManager.GetNode<Control>("Players").AddChild(player);
    //        NodeManager.AddNode(player);

    //        player.IsLocalPlayer = false;
    //        player.GameClient = null;
    //    }

    //    player.PlayerData = playerData;
    //    player.UpdatePlayer();

    //    // Add player to dictionary
    //    Players.AddItem(playerData.Index, player);
    //}

    public void SendPlayerPosition(CPlayerMoveAction playerAction)
    {
        NetPacketProcessor.Send(NetManager.FirstPeer, playerAction, DeliveryMethod.ReliableSequenced);
    }

    // Receber outros jogadores quando eles logarem
    //public void ReceivePlayerPosition(CPlayerAction playerAction, NetPeer netPeer)
    //{

    //ar player = Players.GetItem(playerAction.PlayerId);

    // Sempre que precisar passar valores de uma classe para ela mesma em outro local,
    // é melhor passar seus atributos separadamente para não ter problemas de referência
    //    player.PlayerAction.ActionType = playerAction.ActionType;
    //    player.Position = new Vector2(playerAction.Position.X, playerAction.Position.Y);
    //    player.PlayerAction.Direction = playerAction.Direction;
    //    player.PlayerAction.Speed = playerAction.Speed;
    //    player.PlayerAction.Running = playerAction.Running;
    //}

    //public void Login(string username, string password)
    //{
    //    var loginPacket = new CLogin
    //    {
    //        Login = username,
    //        Password = password
    //    };

    //    NetPacketProcessor.Send(NetManager.FirstPeer, loginPacket, DeliveryMethod.ReliableUnordered);
    //}

    //public void Register(string username, string password)
    //{
    //    var loginPacket = new CNewAccount
    //    {
    //        Login = username,
    //        Password = password
    //    };

    //    NetPacketProcessor.Send(NetManager.FirstPeer, loginPacket, DeliveryMethod.ReliableUnordered);
    //}

    //public void CreateChar(string name)
    //{
    //    var createCharPacket = new CNewChar
    //    {
    //        Name = name
    //    };

    //    NetPacketProcessor.Send(NetManager.FirstPeer, createCharPacket, DeliveryMethod.ReliableUnordered);
    //}

    //public void ReceiveCreateChar(SNewChar newchar, NetPeer netPeer)
    //{
    //    NodeManager.GetNode<Windows>("Windows").CloseAllWindows();
    //    NodeManager.GetNode<Windows>("Windows").AddActiveWindow((IControlWindow)NodeManager.GetNode<CharacterWindow>("CharacterWindow"));
    //}

    //public void PlayerDisconnected(SLeft left, NetPeer netPeer)
    //{
    //    var player = Players.GetItem(left.Index);
    //    player.QueueFree();
    //    Players.RemoveItem(left.Index);
    //}


}
