﻿using DragonRunes.Logger;
using DragonRunes.Network.Service.Interface;
using LiteNetLib;

namespace DragonRunes.Network;
public abstract class NetworkService : IService
{
    protected NetManager? NetManager;
    protected EventBasedNetListener? listener;

    /// <inheritdoc />
    public virtual void Register()
    {
        Logg.Logger.Log("Registering Network Service.");

        this.listener = new EventBasedNetListener();
        this.NetManager = new NetManager(this.listener)
        {
            AutoRecycle = true,
        };
    }

    public virtual void Start()
    {
        this.NetManager?.Start();
    }

    /// <inheritdoc />
    public virtual void Unregister()
    {
        this.Stop();
        this.listener = null;
        this.NetManager = null;
    }

    /// <inheritdoc />
    public virtual void Update()
    {
        this.NetManager?.PollEvents();
    }

    /// <inheritdoc />
    public virtual void Stop()
    {
        this.NetManager?.Stop();
        Logg.Logger.Log("Shutdown.");
    }
}