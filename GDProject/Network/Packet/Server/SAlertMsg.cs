
namespace Network.Packet
{
    internal class SAlertMsg : IRecv
    {
        public string Msg { get; set; }
        public void ReadPacket(int peerId)
        {
            var alertManager = NodeManager.GetNode<Alert>("Alert");

            alertManager.CallDeferred(nameof(alertManager.ShowAlert), Msg);
        }
    }
}
