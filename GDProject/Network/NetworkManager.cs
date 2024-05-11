

using Godot;
using System.Threading;

namespace GdProject.Network;

internal partial class NetworkManager : Node
{

    internal ClientNetworkService _clientNetwork;

    private Thread _thread;

    internal bool _isRunning = true;

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
