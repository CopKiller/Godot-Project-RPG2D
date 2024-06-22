
using DragonRunes.Network.Service;

namespace DragonRunes.Network;

public class NetworkManager : INetworkManager
{
    public INetworkService? _networkService { get; private set; }
    private Thread? _thread;
    public bool _isRunning;

    public NetworkManager(INetworkService networkService)
    {
        _networkService = networkService;
    }
    public void Start()
    {
        _networkService?.Register();
        _networkService?.Start();
        _thread = new Thread(() =>
        {
            while (_isRunning)
            {
                Update();
            }
            Stop();
        });
        _thread.Start();
        _isRunning = true;
    }
    public void Register(INetworkService service)
    {
        _networkService = service;
    }
    public void Stop()
    {
        _isRunning = false;
        _networkService?.Unregister();
    }
    public void Update()
    {
        _networkService?.Update();
        //Thread.Sleep(15); // A Lib ja implementa um delay
    }
}
