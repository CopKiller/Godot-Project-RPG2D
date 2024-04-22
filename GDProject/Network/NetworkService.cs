using GdProject.Logger;
using GdProject.Network.Interface;
using LiteNetLib;

namespace GdProject.Network;
public abstract class NetworkService : IService
{
    public readonly PacketProcessor NetPacketProcessor;
    protected NetManager? NetManager;
    protected EventBasedNetListener listener;

    /// <summary>
    /// Initializes a new instance of the <see cref="NetworkService"/> class.
    /// </summary>
    internal NetworkService()
    {
        this.NetPacketProcessor = new PacketProcessor();
    }

    /// <inheritdoc />
    public virtual void Register()
    {
        NetDebug.Logger = new ExternalLogger();

        this.listener = new EventBasedNetListener();
        this.NetManager = new NetManager(this.listener)
        {
            AutoRecycle = true
        };
    }

    public virtual void Unregister()
    {
        this.Stop();
    }

    /// <inheritdoc />
    public virtual void Update()
    {
        this.NetManager?.PollEvents();
    }

    /// <inheritdoc />
    private void Stop()
    {
        this.NetManager?.Stop();
        ExternalLogger.Print("Shutdown.");
    }
}
