using LiteNetLib;
using DragonRunes.Logger;
using DragonRunes.Server.Logger;
using DragonRunes.Network;
using DragonRunes.Shared;
using DragonRunes.Server.Network;

namespace DragonRunes.Server.Infrastructure
{
    public class InitServer
    {
        // TODO: Implement the ServerLoop class

        private NetworkManager _networkManager;

        //private LogManager _loggerManager;

        //internal static InitDatabase _databaseManager;

        private bool _isRunning = false;

        internal InitServer() { }

        public void Start()
        {
            if (_isRunning)
            {
                return;
            }

            // Initialize the logger
            Logg.Logger = new LogManager();
            Logg.Logger.Log("Logs Initialized...");

            // Initialize the server network service threading
            _networkManager = new NetworkManager(new ServerNetworkService());
            Logg.Logger.Log("NetManager Initialized...");

            //_networkManager._networkService.PlayerAccepted += NetworkService_PlayerAccepted;

            _networkManager.Start();
            Logg.Logger.Log("NetManager Started...");

            // Initialize the database
            //_databaseManager = new InitDatabase();
            //_databaseManager.Start();

            _isRunning = true;
        }
        //private void NetworkService_PlayerAccepted(NetPeer peer)
        //{
        //    var client = new ServerClient(peer);
        //    client.OnDisconnect += PlayerDisconnect;
        //    //_clients.AddItem(peer.Id, client);

        //    Logg.Logger.Log($"Player connected: {peer.Id}");
        //}

        //private void PlayerDisconnect(int peerId)
        //{

        //    var peerEntity = _clients.GetItem(peerId);

        //    var sLeft = new SLeft();
        //    sLeft.Index = peerId;
        //    sLeft.WritePacket(_networkManager._serverNetwork.NetPacketProcessor, peerEntity._peer);

        //    var db = _databaseManager._databaseManager.PlayerRepo;
        //    if (db == null) { return; }

        //    db.SavePlayerAsync(peerEntity._playerData, peerEntity._playerPhysic);

        //    _clients.RemoveItem(peerId);

        //    Logg.Logger.Log($"Player disconnected: {peerId}");
        //}
    }
}
