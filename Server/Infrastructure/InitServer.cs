using LiteNetLib;
using Server.Logger;
using Server.Network;
using SharedLibrary.Extensions;

namespace Server.Infrastructure
{
    public class InitServer
    {
        // TODO: Implement the ServerLoop class

        private NetworkManager _networkManager;

        private LogManager _logger;

        private bool _isRunning = false;

        internal DictionaryWrapper<int, ServerClient> _clients = new DictionaryWrapper<int, ServerClient>();

        internal InitServer(){ }

        public void Start()
        {
            if (_isRunning)
            {
                return;
            }

            // Initialize the logger
            _logger = new LogManager();
            ExternalLogger.Logger = _logger;

            // Initialize the server network service threading
            _networkManager = new NetworkManager();
            _networkManager._serverNetwork.PlayerAccepted += NetworkService_PlayerAccepted;
            _networkManager._serverNetwork.PlayerDisconnected += NetworkService_PlayerDisconnected;
            _networkManager.Start();

            _isRunning = true;
        }
        private void NetworkService_PlayerAccepted(NetPeer peer)
        {
            var client = new ServerClient(peer);
            _clients.AddItem(peer.Id, client);

            _logger.Log($"Player connected: {peer.Id}");
        }

        private void NetworkService_PlayerDisconnected(int peerId)
        {
            _clients.RemoveItem(peerId);

            _logger.Log($"Player disconnected: {peerId}");
        }
    }
}
