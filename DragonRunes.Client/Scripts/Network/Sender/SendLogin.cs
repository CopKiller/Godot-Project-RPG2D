
using DragonRunes.Network.Packet.Client;
using LiteNetLib;

namespace DragonRunes.Scripts.Network
{
    public partial class ClientPacketProcessor
    {
        public void SendLogin()
        {
            //ClientNetworkService._clientPacketProcessor.SendDataTo(0, new CLogin(), DeliveryMethod.ReliableOrdered);
            //var alertManager = NodeManager.GetNode<AlertMsg>("AlertMsg");
            //alertManager.CallDeferred(nameof(alertManager.ShowAlert), Msg);
        }
    }
}
