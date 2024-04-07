

using LiteNetLib;
using Server.Infrastructure;
using SharedLibrary.Extensions;

namespace Server.Network
{
    internal class NetworkManager
    {
        internal ServerNetworkService _serverNetwork;

        private Thread _thread;

        internal bool _isRunning = true;

        public readonly DictionaryWrapper<int, ServerClient> _players;

        public NetworkManager(DictionaryWrapper<int, ServerClient> players)
        {
            _players = players;
            _serverNetwork = new ServerNetworkService(_players);
        }

        public void Start()
        {
            _serverNetwork.Register();
            _serverNetwork.Bind(Config.ServerPort);

            _thread = new Thread(() =>
            {
                while (_isRunning)
                {
                    Update();
                }

                Stop();
            });
            _thread.Start();
        }

        public void Stop()
        {
            _serverNetwork.Unregister();
        }

        public void Update()
        {
            _serverNetwork.Update();
            Thread.Sleep(15);

        }
    }
}
