

using System.Threading;

namespace GdProject.Network;

internal class NetworkManager
{

    internal ClientNetworkService _clientNetwork;

    private Thread _thread;

    internal bool _isRunning = true;

    public NetworkManager()
    {
        _clientNetwork = new ClientNetworkService();
    }

    public void Start()
    {
        _clientNetwork.Register();
        _clientNetwork.Connect();

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
        _isRunning = false;
        _clientNetwork.Unregister();
    }

    public void Update()
    {
        _clientNetwork.Update();
        Thread.Sleep(15);

    }
}
