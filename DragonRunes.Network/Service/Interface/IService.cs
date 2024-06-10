namespace DragonRunes.Network.Service.Interface;

/// <summary>
/// Required interface for service classes
/// </summary>
public interface IService
{
    /// <summary>
    /// Register the service
    /// </summary>
    void Register();

    /// <summary>
    /// Start the service
    /// </summary>
    void Start();

    /// <summary>
    /// Unregister the service
    /// </summary>
    void Unregister();

    /// <summary>
    /// Update the service
    /// </summary>
    void Update();

    /// <summary>
    /// Stop the service
    /// </summary>
    void Stop();
}