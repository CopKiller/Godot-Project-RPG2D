
namespace GdProject.Network.Interface;

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
    /// Unregister the service
    /// </summary>
    void Unregister();

    /// <summary>
    /// Update the service
    /// </summary>
    void Update();
}