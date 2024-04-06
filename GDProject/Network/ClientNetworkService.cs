using LiteNetLib;
using LiteNetLib.Utils;
using System.Net;
using System.Net.Sockets;

namespace GdProject.Network;
public partial class ClientNetworkService : NetworkService
{
    private EventBasedNetListener listener;

    //public DictionaryWrapper<int, Player> Players = new();

    //public override void _Ready()
    //{
    //    this.Register();
    //    this.Connect();
    //}

    //public override void _Process(double delta)
    //{
    //    this.Update((float)delta);
    //}

    /// <summary>
    /// Get the current latency between server and client
    /// </summary>
    public int Ping { get; private set; }
    /// <summary>
    /// The network server peer
    /// </summary>
    public NetPeer ServerPeer { get; private set; }
    /// <summary>
    /// The current peer
    /// </summary>
    private NetPeer CurrentPeer;

    public ClientNetworkService() { }

    public override void Register()
    {
        base.Register();
        this.Disconnect();

        this.listener = new EventBasedNetListener();

        this.NetManager = new NetManager(this.listener)
        {
            AutoRecycle = true,
        };

        InitializePacketHandlers();

        // Subscribe to events.
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

    private void InitializePacketHandlers()
    {
        // Register to receive packets   
        //Subscribe<SPeersAll>(ReceiveAllPlayers);
        //Subscribe<SPlayerData>(ReceivePlayerData);
        //Subscribe<CPlayerAction>(ReceivePlayerPosition);
        //Subscribe<SNewChar>(ReceiveCreateChar);
    }

    public void Connect()
    {
        NetManager.Start();
        //if (NetManager.IsRunning)
            //GDLog.Print("Client: Started client");
        //else
            //GDLog.Print("Client: Failed to start client");

        CurrentPeer = NetManager.Connect(Config.ServerAddress, Config.ServerPort, Config.SecureConnectionKey);
    }

    /// <summary>
    /// Disconnect from server
    /// </summary>
    public void Disconnect()
    {
        this.CurrentPeer?.Disconnect();
    }

    private void OnPeerConnectedEvent(NetPeer peer)
    {
        //GDLog.Print("Client: OnPeerConnectedEvent");
        this.ServerPeer = peer;
    }

    private void OnPeerDisconnectedEvent(NetPeer peer, DisconnectInfo disconnectInfo)
    {
        //GDLog.Print("Client: OnPeerDisconnectedEvent");
    }

    private void OnNetworkErrorEvent(IPEndPoint endPoint, SocketError socketError)
    {
        //GDLog.Print("Client: OnNetworkErrorEvent");
    }

    private void OnNetworkReceiveEvent(NetPeer peer, NetPacketReader reader, byte channel, DeliveryMethod deliveryMethod)
    {
        //GDLog.Print("Client: received data. Processing...");
        // Deserializes packet and calls the handler registered in constructor
        NetPacketProcessor.ReadAllPackets(reader, peer);
    }

    private void OnNetworkReceiveUnconnectedEvent(IPEndPoint remoteEndPoint, NetPacketReader reader, UnconnectedMessageType messageType)
    {
        //GDLog.Print("Client: OnNetworkReceiveUnconnectedEvent");
    }

    private void OnNetworkLatencyUpdateEvent(NetPeer peer, int latency)
    {
        //GD.Print("Client: OnNetworkLatencyUpdateEvent", latency);
    }

    private void OnConnectionRequestEvent(ConnectionRequest request)
    {
        //GDLog.Print("Client: OnConnectionRequestEvent");
    }

    private void OnDeliveryEvent(NetPeer peer, object userData)
    {
        //GDLog.Print("Client: OnDeliveryEvent");
    }

    private void OnNtpResponseEvent(NtpPacket packet)
    {
        //GDLog.Print("Client: OnNtpResponseEvent");
    }

    // Receber todos os jogadores, incluindo o jogador local
    //private void ReceiveAllPlayers(SPeersAll packet, NetPeer peer)
    //{
    //    GDLog.Print("Client: received PeersAll packet with " + packet.PlayerDataModels.Count + " players");

    //    // Clear the dictionary
    //    //Players.Clear();

    //    if (packet.PlayerDataModels.Count == 0) { return; }

    //    GDLog.Print($"peer.RemoteID: {peer.RemoteId} peer.ID: {peer.Id} | ServerPeer.RemoteID: {ServerPeer.RemoteId} ServerPeer.ID: {ServerPeer.Id}");

    //    // Add all players to game
    //    foreach (var player in packet.PlayerDataModels)
    //    {
    //        AddPlayer(player);
    //    }

    //    //NodeManager.GetNode<Client>("Client").InitGame();
    //}
    //private void ReceivePlayerData(SPlayerData packet, NetPeer peer)
    //{
    //    AddPlayer(packet.PlayerDataModel);
    //}

    //private void AddPlayer(PlayerDataModel playerData)
    //{
    //    GDLog.Print("Client: Adding player: " + playerData.Index);

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
    //    }

    //    player.PlayerData = playerData;
    //    player.UpdatePlayer();

    //    // Add player to dictionary
    //    Players.AddItem(playerData.Index, player);
    //}

    //public void SendPlayerPosition(CPlayerAction playerAction)
    //{
    //    NetPacketProcessor.Send(NetManager.FirstPeer, playerAction, DeliveryMethod.ReliableSequenced);
    //}

    //// Receber outros jogadores quando eles logarem
    //public void ReceivePlayerPosition(CPlayerAction playerAction, NetPeer netPeer)
    //{

    //    var player = Players.GetItem(playerAction.PlayerId);

    //    // Sempre que precisar passar valores de uma classe para ela mesma em outro local,
    //    // é melhor passar seus atributos separadamente para não ter problemas de referência
    //    player.PlayerAction.ActionType = playerAction.ActionType;
    //    player.Position = playerAction.Position;
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


}
