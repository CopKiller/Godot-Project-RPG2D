using DragonRunes.Logger;
using DragonRunes.Network.Service;
using LiteNetLib;

namespace DragonRunes.Network;
public abstract class NetworkService : INetworkService
{
    protected NetManager? NetManager;
    protected EventBasedNetListener? listener;

    /// <inheritdoc />
    public virtual void Register()
    {
        this.listener = new EventBasedNetListener();
        this.NetManager = new NetManager(this.listener)
        {
            AutoRecycle = true,
            EnableStatistics = false,
            UnconnectedMessagesEnabled = true
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
