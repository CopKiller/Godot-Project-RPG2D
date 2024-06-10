using DragonRunes.Network.Packet;
using DragonRunes.Network.Packet.Server;
using LiteNetLib;
using System;
namespace DragonRunes.Scripts.Network
{
    public partial class ClientPacketProcessor
    {
        public void Left(SLeft obj, NetPeer netPeer)
        {

            //var playerNode = NodeManager.GetNode<PlayerController>(Index.ToString());

            //if (playerNode == null) return;

            //playerNode.CallDeferred(nameof(playerNode.RemovePlayer));
        }
    }
}
