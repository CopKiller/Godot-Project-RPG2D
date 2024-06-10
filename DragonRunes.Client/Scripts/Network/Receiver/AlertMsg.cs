using DragonRunes.Network.Packet;
using DragonRunes.Network.Packet.Server;
using LiteNetLib;

namespace DragonRunes.Scripts.Network
{
    public partial class ClientPacketProcessor
    {
        public void AlertMsg(SAlertMsg obj, NetPeer netPeer)
        {

            //var alertManager = NodeManager.GetNode<AlertMsg>("AlertMsg");
            //alertManager.CallDeferred(nameof(alertManager.ShowAlert), Msg);
        }
    }
}
