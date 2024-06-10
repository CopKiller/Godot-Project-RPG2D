

namespace DragonRunes.Network.Packet.Server
{
    public class SAlertMsg
    {
        public string Msg { get; set; }
        //public void ReadPacket(int peerId)
        //{
        //    var alertManager = NodeManager.GetNode<AlertMsg>("AlertMsg");

        //    alertManager.CallDeferred(nameof(alertManager.ShowAlert), Msg);
        //}
    }
}
