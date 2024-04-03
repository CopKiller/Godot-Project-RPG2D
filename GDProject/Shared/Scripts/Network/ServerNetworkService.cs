using Godot;
using System.Net;
using System.Net.Sockets;

using LiteNetLib;
using LiteNetLib.Utils;
using GdProject.Shared.Scripts.Network.Packet;
using GdProject.Shared.Scripts.Network.Packet.Client;
using GdProject.Shared.Scripts.Network;
using GdProject.Shared.Scripts.Entities.Player;
using GdProject.Shared.Scripts.Global;
using GdProject.Shared.Scripts.Global.Extensions;
using System.Linq;

public partial class ServerNetworkService : NetworkService
{
    private EventBasedNetListener listener;

    public DictionaryWrapper<int, PlayerDataModel> Players = new(); // This is a dictionary that stores all players in the game.


    public override void _Ready()
    {
        this.Register();
        this.Bind(GlobalConfig.ServerPort);
    }

    public override void _Process(double delta)
    {
        this.Update((float)delta);
    }


    /// <summary>
    /// Bind server on specific port
    /// </summary>
    /// <param name="port"></param>
    public void Bind(int port)
    {
        Register();

        this.listener = new EventBasedNetListener();
        this.NetManager = new NetManager(this.listener)
        {
            AutoRecycle = true,
        };

        InitializePacketHandlers();

        listener.PeerConnectedEvent += OnPeerConnectedEvent;
        listener.PeerDisconnectedEvent += OnPeerDisconnectedEvent;
        listener.NetworkErrorEvent += OnNetworkErrorEvent;
        listener.NetworkReceiveEvent += OnNetworkReceiveEvent;
        listener.NetworkReceiveUnconnectedEvent += OnNetworkReceiveUnconnectedEvent;
        listener.NetworkLatencyUpdateEvent += OnNetworkLatencyUpdateEvent;
        listener.ConnectionRequestEvent += OnConnectionRequestEvent;
        listener.DeliveryEvent += OnDeliveryEvent;
        listener.NtpResponseEvent += OnNtpResponseEvent;

        GDPrint.Print("Server: Try to start on port " + port);

        var result = this.NetManager.Start(port);
        if (result)
        {
            GDPrint.Print("Server: Bind on port " + port);
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

    private void InitializePacketHandlers()
    {
        // Register to receive packets
        Subscribe<CPlayerAction>(ProcessPlayerAction);
    }

    private void OnPeerConnectedEvent(NetPeer peer)
    {
        GD.Print("Server: OnPeerConnectedEvent");

        JoinGameData(peer);
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
        //GD.Print("Server: received data. Processing...");
        // Deserializes packet and calls the handler registered in constructor
        NetPacketProcessor.ReadAllPackets(reader, peer);
    }

    private void OnNetworkReceiveUnconnectedEvent(IPEndPoint remoteEndPoint, NetPacketReader reader, UnconnectedMessageType messageType)
    {
        GD.Print("Server: OnNetworkReceiveUnconnectedEvent");
    }

    private void OnNetworkLatencyUpdateEvent(NetPeer peer, int latency)
    {
        //GD.Print("Server: OnNetworkLatencyUpdateEvent");
    }

    private void OnConnectionRequestEvent(ConnectionRequest request)
    {
        if (NetManager.ConnectedPeersCount < 10) { /* max connections */
            request.AcceptIfKey(GlobalConfig.SecureConnectionKey);
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

    public void JoinGameData(NetPeer peer)
    {
        // Create player data
        var playerDataModel = new PlayerDataModel();
        playerDataModel.Index = peer.Id;
        playerDataModel.Position = new Vector2(200, 200);
        playerDataModel.PlayerName = "Player " + peer.Id;

        // Add player to the Dictionary of all players
        Players.AddItem(playerDataModel.Index, playerDataModel);

        // Send all players to the new player -> TO NEW PLAYER
        SendAllPlayersTo(peer);

        // Send new player to all players -> TO ALL PLAYERS
        SendPlayerDataTo(peer, playerDataModel);
    }

    private void SendPlayerDataTo(NetPeer peer, PlayerDataModel playerDataModel)
    {
        var playerData = new SPlayerData();
        playerData.PlayerDataModel = playerDataModel;
        NetPacketProcessor.Send(NetManager, playerData, DeliveryMethod.ReliableUnordered, peer);
    }

    private void SendAllPlayersTo(NetPeer peer)
    {
        var playersList = Players.GetItems();

        var allPers = new SPeersAll();
        allPers.PlayerDataModels = playersList.Values.ToList();
        NetPacketProcessor.Send(peer, allPers, DeliveryMethod.ReliableOrdered);
    }

    private void ProcessPlayerAction(CPlayerAction playerAction, NetPeer netPeer)
    {
        if (Players.ContainsKey(netPeer.Id))
        {
            switch (playerAction.ActionType)
            {
                case PlayerActionType.Move:
                    Players.GetItem(netPeer.Id).Position = playerAction.Position;
                    playerAction.PlayerId = netPeer.Id;

                    SentMessageToAll(playerAction, DeliveryMethod.ReliableSequenced, netPeer);
                    break;
                case PlayerActionType.Stop:
                    Players.GetItem(netPeer.Id).Position = playerAction.Position;
                    playerAction.PlayerId = netPeer.Id;

                    SentMessageToAll(playerAction, DeliveryMethod.ReliableSequenced, netPeer);
                    break;
                default:
                    break;
            }
        }
    }

    private void ProcessPlayerLogin(CLogin playerLogin, NetPeer netPeer)
    {
        var db = NodeManager.GetNode<Database>("Database");

        if (db == null) { return; }

        //var accountRepo = db.GetRepository<AccountRepository>();

        //if (Players.ContainsKey(netPeer.Id))
        //{
        //    Players.GetItem(netPeer.Id).PlayerName = playerLogin.PlayerName;
        //    Players.GetItem(netPeer.Id).Position = playerLogin.Position;
        //    Players.GetItem(netPeer.Id).Rotation = playerLogin.Rotation;
        //    Players.GetItem(netPeer.Id).Scale = playerLogin.Scale;
        //    Players.GetItem(netPeer.Id).Color = playerLogin.Color;
        //    Players.GetItem(netPeer.Id).IsLocalPlayer = playerLogin.IsLocalPlayer;
        //}
    }
}
