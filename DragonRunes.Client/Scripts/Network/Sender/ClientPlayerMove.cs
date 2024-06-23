
using DragonRunes.Client.Scripts;
using DragonRunes.Network.CustomDataSerializable;
using DragonRunes.Network.Packet.Client;
using LiteNetLib;

namespace DragonRunes.Scripts.Network
{
    public partial class ClientPacketProcessor
    {
        public void ClientPlayerMove(NetPeer netPeer, PlayerMoveModel playerMoveModel)
        {
            var packet = new CPlayerMove
            {
                PlayerMoveModel = playerMoveModel
            };

            SendDataTo(netPeer, packet, DeliveryMethod.ReliableSequenced);
        }
    }
}
