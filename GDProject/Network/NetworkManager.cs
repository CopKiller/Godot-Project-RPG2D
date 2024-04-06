

using System.Threading;

namespace GdProject.Network;

internal class NetworkManager
{
    //public Action<NetPeer> PlayerAccepted;

    internal ClientNetworkService _clientNetwork;

    private Thread _thread;

    internal bool _isRunning = true;

    public NetworkManager()
    {
        _clientNetwork = new ClientNetworkService();
        //_serverNetwork.PlayerAccepted += OnPlayerAccepted;
    }

    //private void OnPlayerAccepted(NetPeer peer)
    //{
    //    PlayerAccepted?.Invoke(peer);
    //}

    public void Start()
    {
        _clientNetwork.Register();
        _clientNetwork.Bind(Config.ServerPort);

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
