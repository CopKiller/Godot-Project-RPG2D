﻿

using DragonRunes.Network.Service.Interface;

namespace DragonRunes.Network;

public class NetworkManager
{
    public readonly IService? _networkService;

    private Thread? _thread;

    public bool _isRunning;

    public NetworkManager(IService networkService)
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

    public void Stop()
    {
        _isRunning = false;
        _networkService?.Unregister();
    }

    public void Update()
    {
        _networkService?.Update();
        //Thread.Sleep(15);

    }
}
