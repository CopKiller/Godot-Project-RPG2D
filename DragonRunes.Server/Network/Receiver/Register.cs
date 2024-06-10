using DragonRunes.Network.Packet.Client;
using LiteNetLib;

namespace DragonRunes.Server.Network
{
    public partial class ServerPacketProcessor
    {
        public void Register(CRegister obj, NetPeer netPeer)
        {

            //var alertManager = NodeManager.GetNode<AlertMsg>("AlertMsg");
            //alertManager.CallDeferred(nameof(alertManager.ShowAlert), Msg);
        }
    }
}
