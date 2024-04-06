using LiteNetLib;
using LiteNetLib.Utils;
using Server.Logger;
using SharedLibrary.Models;
using System.Net;
using System.Net.Sockets;

namespace Server.Network;
internal class ServerNetworkService : NetworkService
{
    public event Action<NetPeer> PlayerAccepted;
    public event Action<int> PlayerDisconnected;

    public override void Register()
    {
        base.Register();

        // Register to receive packets
        //Subscribe<CPlayerAction>(ProcessPlayerAction);
        //Subscribe<CLogin>(ProcessPlayerLogin);
        //Subscribe<CNewAccount>(ProcessPlayerRegister);
        //Subscribe<CNewChar>(ProcessPlayerCreateChar);

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
        //ExternalLogger.Print("Server: OnPeerConnectedEvent");

        PlayerAccepted?.Invoke(peer);
    }

    private void OnPeerDisconnectedEvent(NetPeer peer, DisconnectInfo disconnectInfo)
    {
        //ExternalLogger.PrintErr($"PeerId: {peer.Id} Disconnected from reason: {Enum.GetName(typeof(DisconnectReason), disconnectInfo.Reason)}");
        
        PlayerDisconnected?.Invoke(peer.Id);
    }

    private void OnNetworkErrorEvent(IPEndPoint endPoint, SocketError socketError)
    {
        ExternalLogger.Print("Server: OnNetworkErrorEvent");
    }

    private void OnNetworkReceiveEvent(NetPeer peer, NetPacketReader reader, byte channel, DeliveryMethod deliveryMethod)
    {
        //GD.Print("Server: received data. Processing...");
        // Deserializes packet and calls the handler registered in constructor
        NetPacketProcessor.ReadAllPackets(reader, peer);
    }

    private void OnNetworkReceiveUnconnectedEvent(IPEndPoint remoteEndPoint, NetPacketReader reader, UnconnectedMessageType messageType)
    {
        ExternalLogger.Print("Server: OnNetworkReceiveUnconnectedEvent");
    }

    private void OnNetworkLatencyUpdateEvent(NetPeer peer, int latency)
    {
        //GD.Print("Server: OnNetworkLatencyUpdateEvent");
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
        ExternalLogger.Print("Server: OnDeliveryEvent");
    }

    private void OnNtpResponseEvent(NtpPacket packet)
    {
        ExternalLogger.Print("Server: OnNtpResponseEvent");
    }

    public void AddPlayer(NetPeer peer)
    {
        var newPlayer = new PlayerDataModel();
        newPlayer.Index = peer.Id;
        //Players.AddItem(peer.Id, newPlayer);
    }

    //public void JoinGameData(PlayerDataModel playerDataModel, NetPeer peer)
    //{
    //    // Add player to the Dictionary of all players
    //    playerDataModel.GameState = GameState.InGame;

    //    // Send all players to the new player -> TO NEW PLAYER
    //    SendAllPlayersTo(peer);

    //    // Send new player to all players -> TO ALL PLAYERS
    //    SendPlayerDataTo(peer, playerDataModel);
    //}

    //private void SendPlayerDataTo(NetPeer peer, PlayerDataModel playerDataModel)
    //{
    //    var playerData = new SPlayerData();
    //    playerData.PlayerDataModel = playerDataModel;
    //    NetPacketProcessor.Send(NetManager, playerData, DeliveryMethod.ReliableUnordered, peer);
    //}

    //private void SendAllPlayersTo(NetPeer peer)
    //{
    //    var playersList = Players.GetItems();

    //    var allPers = new SPeersAll();
    //    allPers.PlayerDataModels = playersList.Values.ToList();
    //    NetPacketProcessor.Send(peer, allPers, DeliveryMethod.ReliableOrdered);
    //}

    //private void ProcessPlayerAction(CPlayerAction playerAction, NetPeer netPeer)
    //{
    //    var player = Players.GetItem(netPeer.Id);
    //    if (player.GameState != GameState.InGame) { return; }

    //    if (Players.ContainsKey(netPeer.Id))
    //    {
    //        switch (playerAction.ActionType)
    //        {
    //            case PlayerActionType.Move:
    //                Players.GetItem(netPeer.Id).Position = playerAction.Position;
    //                playerAction.PlayerId = netPeer.Id;

    //                SentMessageToAll(playerAction, DeliveryMethod.ReliableSequenced, netPeer);
    //                break;
    //            case PlayerActionType.Stop:
    //                Players.GetItem(netPeer.Id).Position = playerAction.Position;
    //                playerAction.PlayerId = netPeer.Id;

    //                SentMessageToAll(playerAction, DeliveryMethod.ReliableSequenced, netPeer);
    //                break;
    //            default:
    //                break;
    //        }
    //    }
    //}

    //private void ProcessPlayerLogin(CLogin playerLogin, NetPeer netPeer)
    //{
    //    var player = Players.GetItem(netPeer.Id);

    //    if (player.GameState != GameState.InMenu) { return; }

    //    var db = NodeManager.GetNode<Database>("Database");

    //    if (db == null) { return; }

    //    var account = db.AuthenticateAsync(playerLogin.Login, playerLogin.Password);

    //    if (account.Result == null) { return; }

    //    GDLog.Print("account logged in: " + account.Result.Login);

    //    player.accountId = account.Result.Id;

    //    if (account.Result.Player.Name == string.Empty)
    //    {
    //        // Create character
    //        var newChar = new SNewChar();
    //        NetPacketProcessor.Send(netPeer, newChar, DeliveryMethod.ReliableUnordered);
    //        Players.GetItem(netPeer.Id).GameState = GameState.InCharacterCreation;
    //        return;
    //    }

    //    GDLog.Print("Player logged in: " + account.Result.Player.Name);

    //    // Create player data
    //    var playerVar = account.Result.Player;

    //    player.Position = new Vector2(playerVar.Position.X, playerVar.Position.Y);
    //    player.PlayerName = playerVar.Name;

    //    player.playerId = playerVar.Id;

    //    JoinGameData(player, netPeer);
    //}

    //private void ProcessPlayerRegister(CNewAccount playerLogin, NetPeer netPeer)
    //{
    //    var player = Players.GetItem(netPeer.Id);
    //    if (player.GameState != GameState.InMenu) { return; }

    //    var db = NodeManager.GetNode<Database>("Database");

    //    if (db == null) { return; }

    //    db.RegisterAccountAsync(playerLogin.Login, playerLogin.Password);

    //}

    //private void ProcessPlayerCreateChar(CNewChar playerCharacter, NetPeer netPeer)
    //{
    //    var player = Players.GetItem(netPeer.Id);
    //    if (player.GameState != GameState.InCharacterCreation) { return; }

    //    var db = NodeManager.GetNode<Database>("Database");

    //    if (db == null) { return; }

    //    var result = db.RegisterPlayerAsync(playerCharacter.Name, player.accountId).Result;

    //    player.PlayerName = playerCharacter.Name;
    //    JoinGameData(player, netPeer);

    //}
}
