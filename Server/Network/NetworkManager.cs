

using LiteNetLib;

namespace Server.Network
{
    internal class NetworkManager
    {
        //public Action<NetPeer> PlayerAccepted;

        internal ServerNetworkService _serverNetwork;

        private Thread _thread;

        internal bool _isRunning = true;

        public NetworkManager()
        {
            _serverNetwork = new ServerNetworkService();
            //_serverNetwork.PlayerAccepted += OnPlayerAccepted;
        }

        //private void OnPlayerAccepted(NetPeer peer)
        //{
        //    PlayerAccepted?.Invoke(peer);
        //}

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
