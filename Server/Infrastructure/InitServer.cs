using LiteNetLib;
using Server.Database;
using Server.Logger;
using Server.Network;
using SharedLibrary.Extensions;
using SharedLibrary.Network.Packet.Server;

namespace Server.Infrastructure
{
    public class InitServer
    {
        // TODO: Implement the ServerLoop class

        private NetworkManager _networkManager;

        private LogManager _loggerManager;

        internal static InitDatabase _databaseManager;

        private bool _isRunning = false;

        internal DictionaryWrapper<int, ServerClient> _clients = new();

        internal InitServer(){ }

        public void Start()
        {
            if (_isRunning)
            {
                return;
            }

            // Initialize the logger
            _loggerManager = new LogManager();
            ExternalLogger.Logger = _loggerManager;

            // Initialize the server network service threading
            _networkManager = new NetworkManager(_clients);
            _networkManager._serverNetwork.PlayerAccepted += NetworkService_PlayerAccepted;
            _networkManager._serverNetwork.PlayerDisconnected += NetworkService_PlayerDisconnected;
            _networkManager.Start();

            // Initialize the database
            _databaseManager = new InitDatabase();
            _databaseManager.Start();

            _isRunning = true;
        }
        private void NetworkService_PlayerAccepted(NetPeer peer)
        {
            var client = new ServerClient(peer);
            _clients.AddItem(peer.Id, client);

            _loggerManager.Log($"Player connected: {peer.Id}");
        }

        private void NetworkService_PlayerDisconnected(int peerId)
        {
            var sLeft = new SLeft();
            sLeft.Index = peerId;

            _networkManager._serverNetwork.ProcessPackage.SentMessageToAllBut(sLeft, DeliveryMethod.ReliableUnordered, peerId);

            _clients.RemoveItem(peerId);

            _loggerManager.Log($"Player disconnected: {peerId}");
        }
    }
}
