using DragonRunes.Network.Packet.Client;
using LiteNetLib;

namespace DragonRunes.Server.Network
{
    public partial class ServerPacketProcessor
    {
        public void ClientNewChar(CNewChar obj, NetPeer netPeer)
        {
            var player = _players.GetItem(netPeer.Id);

            if (player.GameState != GameState.InLogin)
            {
                ServerAlertMsg(netPeer, "You are not in the menu screen!");
                player.Disconnect();
                return;
            }
            
        }
    }
}
